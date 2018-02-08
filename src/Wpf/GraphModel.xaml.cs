using M4Graphs.Core;
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
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace M4Graphs.Wpf
{
    /// <summary>
    /// Interaction logic for DynamicGraphModel.xaml
    /// </summary>
    public partial class GraphModel : IDynamicGraphModel<GraphModel>
    {
        private int NodeWidth { get; } = 50;
        private int NodeHeight { get; } = 20;

        private readonly Dictionary<string, IModelElement> _elements = new Dictionary<string, IModelElement>();

        private IModelElement _currentlyActivated;

        private readonly HeatMap _heatMap = new HeatMap();

        private readonly Filters _filters = new Filters();

        public static readonly DependencyProperty ModelBackgroundProperty = DependencyProperty.Register("ModelBackground", typeof(Brush), typeof(GraphModel), new FrameworkPropertyMetadata(Brushes.LightSteelBlue, FrameworkPropertyMetadataOptions.AffectsRender));
        public Brush ModelBackground
        {
            get => (Brush)GetValue(ModelBackgroundProperty);
            set { SetValue(ModelBackgroundProperty, value);
                ColorManager.FilteredColor = value;
            }
        }

        /// <summary>
        /// 0: 25%, 1: 50%, 2: 75%, 3: 100%, 4: 150%, 5: 200%
        /// </summary>
        private readonly LevelList<double> _zoomStages = new LevelList<double>(3) {0.25, 0.5, 0.75, 1, 1.5, 2 }; // 1 = 100%, 2 = 200%, 0.5 = 50%, etc
        private readonly MatrixTransform _originalTransform;
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
        public GraphModel(int nodeWidth, int nodeHeight)
        {
            NodeWidth = nodeWidth;
            NodeHeight = nodeHeight;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GraphModel(Brush background, int nodeWidth, int nodeHeight) : this(nodeWidth, nodeHeight)
        {
            Main.Background = background;
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

        public void Draw(IRenderer<GraphModel> renderer)
        {
            renderer.RenderElements(this);
        }

        internal void AddElement(ModelElementBase element)
        {
            _elements.Add(element.Id, element);
            ModelBoard.Children.Add(element);
        }

        public void Refresh()
        {
            // TODO: implement functionality for cases where it'd be unnecessary to redraw the entire model
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
            var elements = _heatMap.GetElements();
            foreach (var id in elements)
            {
                if (id.Equals(idToExempt, StringComparison.InvariantCultureIgnoreCase)) continue;
                var heat = _heatMap.GetHeat(id);
                _elements[id].UpdateHeat(heat);
            }
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
