using M4Graphs.Core;
using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.General;
using M4Graphs.Core.Interfaces;
using M4Graphs.Wpf.Components;
using M4Graphs.Wpf.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace M4Graphs.Wpf
{
    /// <summary>
    /// Interaction logic for DynamicGraphModel.xaml
    /// </summary>
    public partial class GraphModel : UserControl, IDynamicGraphModel
    {
        //private Dictionary<int, SolidColorBrush> brushForEdgeId = new Dictionary<int, SolidColorBrush>();
        public IModel Model; // TODO: make private? TODO: more thinking
        private int NodeWidth = 50;
        private int NodeHeight = 20;
        private int _yDistance = 110;
        private int _xDistance = 60;
        
        private int RandVal => Measurements.GetRandom(1, 10);

        private Dictionary<string, IDynamicModelElement> _elements = new Dictionary<string, IDynamicModelElement>();

        private IDynamicModelElement _currentlyActivated;

        private HeatMap _heatMap = new HeatMap();
        private Filters _filters = new Filters();

        public static DependencyProperty ModelBackgroundProperty = DependencyProperty.Register("ModelBackground", typeof(Brush), typeof(GraphModel), new FrameworkPropertyMetadata(Brushes.LightSteelBlue, FrameworkPropertyMetadataOptions.AffectsRender));
        public Brush ModelBackground
        {
            get { return (Brush)GetValue(ModelBackgroundProperty); }
            set { SetValue(ModelBackgroundProperty, value);
                ColorManager.FilteredColor = value;
            }
        }

        /// <summary>
        /// 0: 25%, 1: 50%, 2: 75%, 3: 100%, 4: 150%, 5: 200%
        /// </summary>
        private LevelList<double> _zoomStages = new LevelList<double>(3) {0.25, 0.5, 0.75, 1, 1.5, 2 }; // 1 = 100%, 2 = 200%, 0.5 = 50%, etc
        private MatrixTransform _originalTransform;
        private MatrixTransform _currentTransform;

        private double _gridActualWidth;
        private double _gridActualHeight;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GraphModel()
        {
            InitializeComponent();
            Measurements.NodeMinWidth = NodeWidth;
            _originalTransform = ModelBoard.RenderTransform as MatrixTransform;
            _currentTransform = ModelBoard.RenderTransform as MatrixTransform;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xDistance"></param>
        /// <param name="yDistance"></param>
        public GraphModel(int xDistance, int yDistance) : this()
        {
            _xDistance = xDistance;
            _yDistance = yDistance;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="nodeWidth"></param>
        /// <param name="nodeHeight"></param>
        /// <param name="xDistance"></param>
        /// <param name="yDistance"></param>
        public GraphModel(int nodeWidth, int nodeHeight, int xDistance, int yDistance) : this(xDistance, yDistance)
        {
            NodeWidth = nodeWidth;
            NodeHeight = nodeHeight;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="background"></param>
        /// <param name="nodeWidth"></param>
        /// <param name="nodeHeight"></param>
        /// <param name="xDistance"></param>
        /// <param name="yDistance"></param>
        public GraphModel(Brush background, int nodeWidth, int nodeHeight, int xDistance, int yDistance) : this(nodeWidth, nodeHeight, xDistance, yDistance)
        {
            Main.Background = background;
        }

        /// <summary>
        /// Sets the associated <see cref="Model"/> used for drawing.
        /// </summary>
        /// <param name="model"></param>
        public void Set(IModel model)
        {
            Model = model;
        }

        /// <summary>
        /// Resets the dynamic graph model.
        /// </summary>
        public void Reset()
        {
            _heatMap.Reset();
            _elements.Clear();
            ModelBoard.Children.Clear();
        }

        /// <summary>
        /// Gets the elements from the model before drawing.
        /// </summary>
        public void Draw()
        {
            // reset board before drawing
            Reset();
            //var elements = Model.GetElements(_xDistance, _yDistance);
            var elements = Model.GetElements();
            Draw(elements);
        }

        /// <summary>
        /// Draws a loaded model.
        /// </summary>
        /// <param name="loadedModel"></param>
        public void Draw(DrawableElementCollection loadedModel)
        {
            // reset board before drawing
            Reset();
            foreach (var edge in loadedModel.Edges)
                DrawEdge(edge.Value, loadedModel);

            foreach (var node in loadedModel.Nodes)
                DrawNode(node.Value, loadedModel);
        }

        private void DrawNode(IDrawableNode node, DrawableElementCollection collection)
        {
            // TODO: refactor this method
            if(node.IsLoaded)
            {
                var newNode = new Node(node.Id, node.Text, node.X, node.Y, node.Width, node.Height);
                AddNode(newNode);
                // no fun allowed -> newNode.BeginAnimation(HeightProperty, new DoubleAnimation(node.Y, node.Height + node.Y, TimeSpan.FromSeconds(2)));
                return;
            }
            var x = node.X + RandVal;
            var y = node.Y + RandVal;
            var calculatedWidth = Measurements.NodeWidth(node.Text);
            var width = calculatedWidth > Measurements.NodeMinWidth ? calculatedWidth : Measurements.NodeMinWidth;

            var control = new Node(node.Id, node.Text, x, y, width, NodeHeight);
            AddNode(control);
        }

        private void AddNode(Node newNode)
        {
            _elements.Add(newNode.Id, newNode);
            ModelBoard.Children.Add(newNode);
        }

        private void DrawEdge(IDrawableEdge edge, DrawableElementCollection collection)
        {
            // TODO: refactor this method into classes
            // and let them figure this stuff out


            if (edge.IsLoaded)
            {
                var loadedPoints = PathPointsToPointCollection(edge.Points);
                var source = collection.Nodes[edge.SourceId];
                var target = collection.Nodes[edge.TargetId];
                loadedPoints.Insert(0, new Point(source.CenterX, source.CenterY));
                PathPoint outside;
                if (loadedPoints.Count > 1)
                {
                    var nextLast = loadedPoints.Last();
                    outside = target.Collide(new PathPoint(nextLast.X, nextLast.Y));
                }
                else
                {
                    outside = target.Collide(new PathPoint(source.CenterX, source.CenterY));
                }
                loadedPoints.Add(outside.ToPoint());
                 
                Edge newLine;
                if (edge.Label != null)
                {
                    var secondPoint = loadedPoints[1];
                    var firstPoint = source.Collide(new PathPoint(secondPoint.X, secondPoint.Y));
                    // get first x and y coordinates outside of source node
                    var labelPoint = edge.Label.GetActualPosition(firstPoint.X, firstPoint.Y);
                    newLine = new Edge(edge.Id, edge.Text, loadedPoints, labelPoint.ToPoint());
                }
                else
                    newLine = new Edge(edge.Id, edge.Text, loadedPoints);

                // add edge to board
                AddEdge(newLine);
                return;
            }

            var points = new PointCollection();
            if (edge.SourceNode == null) return; // can't draw if there's no starting point
            var start = edge.SourceNode;
            points.Add(new Point(start.X + (NodeWidth / 2) + RandVal, start.Y + NodeHeight));

            if (edge.TargetNode != null)
            {
                var end = edge.TargetNode;

                if (end.Y < start.Y)
                {
                    points.Add(new Point(end.X + (NodeWidth / 2) + RandVal, end.Y + NodeHeight + 10));
                }
                else
                {
                    points.Add(new Point(end.X + (NodeWidth / 2) + RandVal, end.Y));
                }
            }
            else
            {
                // not attached - just draw a straight line or whatever
                points.Add(new Point(start.X + (NodeWidth / 2) + (edge.X * NodeWidth) + RandVal, start.Y + _yDistance));
            }

            var line = new Edge(edge.Id, edge.Text, points);

            // add line to board
            AddEdge(line);
        }

        private void AddEdge(Edge newEdge)
        {
            _elements.Add(newEdge.Id, newEdge);
            ModelBoard.Children.Add(newEdge);
        }

        private Color GetColor()
        {
            //var r = Convert.ToByte(rand.Next(1, 250));
            //var g = Convert.ToByte(rand.Next(1, 250));
            //var b = Convert.ToByte(rand.Next(1, 250));
            byte r = 1;
            byte g = 1;
            byte b = 1;
            return Color.FromRgb(r, g, b);
        }

        //private SolidColorBrush AssignOrGetColor(int id)
        //{
        //    SolidColorBrush brush;
        //    if(brushForEdgeId.TryGetValue(id, out brush))
        //    {
        //        return brush;
        //    }
        //    var color = new SolidColorBrush(GetColor());
        //    brushForEdgeId.Add(id, color);
        //    return color;
        //}

        public void Refresh()
        {
            // TODO: add/remove changed elements from tree, but don't touch any other
            // for efficiency, handle changes inside Tree.cs 
            // and do a Tree.ClearChanges() - thingy here after having done the refreshing
        }

        /// <summary>
        /// Sets the background of the control.
        /// </summary>
        /// <param name="color"></param>
        public void SetBackground(Brush color)
        {
            ModelBackground = color;
        }

        /// <summary>
        /// Activates the specified element, deactivating the last activated one.
        /// </summary>
        /// <param name="id"></param>
        public void ActivateElement(string id)
        {
            Dispatcher.InvokeOnUIThread(() =>
            {
                var old = _currentlyActivated;
                // deactivate last active one
                old?.Deactivate();
                _currentlyActivated = _elements[id];
                // add heat to current active one
                _heatMap.AddHeat(_currentlyActivated.Id);
                // update heat for all elements except current one
                UpdateHeat(id);
                _currentlyActivated.Activate();
            });
            FilterElements();
        }

        /// <summary>
        /// Adds a generic error to the specified element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        public void AddElementError(string id)
        {
            AddElementError(id, new GenericExecutingElementMethodError(id));
        }

        /// <summary>
        /// Adds an error to the specified element element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        /// <param name="error">The associated error.</param>
        public void AddElementError(string id, ExecutingElementMethodError error)
        {
            Dispatcher.InvokeOnUIThread(() =>
            {
                _elements[id].AddError(error);
            });
            FilterElements();
        }

        private void UpdateHeat(string idToExempt)
        {
            var elements = _heatMap.Elements;
            foreach (var id in elements)
            {
                if (id.Equals(idToExempt, StringComparison.InvariantCultureIgnoreCase)) continue;
                var heat = _heatMap.GetHeat(id);
                _elements[id].UpdateHeat(heat);
            }
        }

        private PointCollection PathPointsToPointCollection(List<PathPoint> points)
        {
            return new PointCollection(points.Select(point => new Point(point.X, point.Y)));
        }

        private void Main_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var position = e.GetPosition(Main); // the position at which the user scrolled up/down
            if (e.Delta > 0)
                ZoomIn(position);
            else
                ZoomOut(position);
            e.Handled = true;
        }

        /// <summary>
        /// Zooms in on the graphical model
        /// </summary>
        /// <param name="point">The central point of the zooming</param>
        private void ZoomIn(Point point)
        {
            _zoomStages.Up();
            ZoomModelBoard(_zoomStages.Current, point);
        }

        /// <summary>
        /// Zooms out of the graphical model
        /// </summary>
        /// <param name="point">The central point of the zooming</param>
        private void ZoomOut(Point point)
        {
            _zoomStages.Down();
            ZoomModelBoard(_zoomStages.Current, point);
        }

        private void ZoomModelBoard(double percent, Point point)
        {
            Dispatcher.InvokeOnUIThread(() =>
            {
                var matrix = _originalTransform.Matrix;

                matrix.ScaleAt(percent, percent, 0, 0);// point.X, point.Y);
                _currentTransform = new MatrixTransform(matrix);
                ModelBoard.RenderTransform = _currentTransform;
                UpdateScrollBar();
            });
        }

        private void MoveModelBoard(Point point)
        {
            //const double OutOfBoundsLimit = 400;
            // Reduce move speed by half
            //point.X *= 0.5;
            //point.Y *= 0.5;
            // Move model by adjusting margins
            var margin = ModelBoard.Margin;
            var left = margin.Left + point.X;
            var top = margin.Top + point.Y;
            var divideBy = 2;
            ModelBoard.Margin = new Thickness(
                left.Clamp(-_gridActualWidth / divideBy,
                    _gridActualWidth / divideBy),
                top.Clamp(-_gridActualHeight / divideBy,
                    _gridActualHeight / divideBy),
                margin.Right, margin.Bottom);
            
            //ModelBoard.Margin = new Thickness(
            //    left.Clamp(-ModelBoard.ActualWidth + OutOfBoundsLimit,
            //        ModelBoard.ActualWidth - OutOfBoundsLimit),
            //    top.Clamp(-ModelBoard.ActualHeight + OutOfBoundsLimit,
            //        ModelBoard.ActualHeight - OutOfBoundsLimit),
            //    margin.Right, margin.Bottom);

            // Make scroll thingy workyworky

            UpdateScrollBar();
        }

        private void UpdateScrollBar()
        {
            var currentZoom = _zoomStages.Current;
            var maxX = _elements.Values.Max(_ => _.Position.X) * currentZoom;
            var maxY = _elements.Values.Max(_ => _.Position.Y) * currentZoom;
            var leftMargin = Math.Abs(ModelBoard.Margin.Left);
            var topMargin = Math.Abs(ModelBoard.Margin.Top);
            //var leftMargin = GetMarginSize(ModelBoard.Margin.Left);
            //var topMargin = GetMarginSize(ModelBoard.Margin.Top);
            ModelBoard.Width = maxX + leftMargin;
            ModelBoard.Height = maxY + topMargin;
        }
        
        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // start drag here
            MouseDrag.Begin(sender, e);
        }


        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            var mouse = MouseDrag.Move(sender, e);
            // check if moving
            if (!mouse.IsMoving) return;
            // move around
            MoveModelBoard(mouse.Change);
        }

        private void Main_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // finish drag here (may have to update scrollbar afterwards)
            MouseDrag.End(sender, e);
        }

        private void Root_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _gridActualHeight = Root.ActualHeight;
            _gridActualWidth = Root.ActualWidth;
        }

        private void Filters_HeatMap_Show_Green_Click(object sender, RoutedEventArgs e)
        {
            var item = ((MenuItem)e.Source);
            item.IsChecked = !item.IsChecked;
            _filters.HeatMap.ToggleGreen();
            FilterElements();
        }

        private void Filters_HeatMap_Show_Red_Click(object sender, RoutedEventArgs e)
        {
            var item = ((MenuItem)e.Source);
            item.IsChecked = !item.IsChecked;
            _filters.HeatMap.ToggleRed();
            FilterElements();
        }

        private void FilterElements()
        {
            Dispatcher.InvokeOnUIThread(() =>
            {
                foreach (var el in _elements.Values)
                {
                    el.Filter(_filters.Current);
                }
            });
        }

        private void Filters_Show_Unvisited_Click(object sender, RoutedEventArgs e)
        {
            var item = ((MenuItem)e.Source);
            item.IsChecked = !item.IsChecked;
            _filters.ToggleVisited();
            FilterElements();

        }
    }
}
