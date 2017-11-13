using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using M4Graphs.Core.General;
using M4Graphs.Core.Interfaces;

namespace M4Graphs.Wpf.Components
{
    /// <summary>
    /// Interaction logic for Edge.xaml
    /// </summary>
    public partial class Edge : UserControl, IDynamicModelElement
    {
        private Brush ColorNormal = Brushes.Black;
        private Brush ColorReset = Brushes.Black;

        public string Id { get; private set; }
        public ElementState State { get; private set; } = ElementState.Normal;
        public bool IsVisited { get; private set; }

        public bool HasErrors => Errors.Any();

        private enum Direction
        {
            Left = 1,
            Right = 2
        }

        public List<ExecutingElementMethodError> Errors = new List<ExecutingElementMethodError>();

        private Direction ArrowDirection = Direction.Left;

        public static DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(PathPoint), typeof(Edge), new FrameworkPropertyMetadata(PathPoint.Zero, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
        public PathPoint Position
        {
            get { return (PathPoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        private double _lastHeat;

        public Edge()
        {
            InitializeComponent();
            var dir = (Direction)(Measurements.GetRandom(1000, 2999) / 1000);
            ArrowDirection = dir;
        }

        public Edge(string id, string text, PointCollection points) : this()
        {
            Id = id;
            SetPoints(points);
            var x = points.Min(p => p.X);
            var y = points.Min(p => p.Y);
            EdgeText.Text = text;
            EdgeText.Margin = new Thickness(x, y, 0, 0);
        }


        public Edge(string id, string text, PointCollection points, Point labelPoint) : this()
        {
            Id = id;
            SetPoints(points);
            EdgeText.Text = text;
            EdgeText.Margin = new Thickness(labelPoint.X, labelPoint.Y, 0, 0);
        }

        public Edge(string id, SolidColorBrush color, PointCollection points) : this()
        {
            Id = id;
            EdgeArrow.Stroke = color;
            SetPoints(points);
        }

        public Edge(string id, string text, SolidColorBrush color, PointCollection points) : this(id, color, points)
        {
            var x = points?.Min(p => p.X) ?? 0;
            var y = points?.Min(p => p.Y) ?? 0;
            EdgeText.Text = text;
            EdgeText.Margin = new Thickness(x, y, 0, 0);
        }


        private void SetPoints(PointCollection points)
        {
            EdgeArrow.Points = points;
            EdgeMouseOver.Points = points;
            var x = points?.Max(p => p.X) ?? 0;
            var y = points?.Max(p => p.Y) ?? 0;
            Position = new PathPoint(x, y);
        }

        private PointCollection BendEdge(PointCollection points)
        {
            var pointCount = Measurements.InterpolationCount * points.Count;
            var roundedPoints = new PointCollection();
            var start = points.First();
            var end = points.Last();
            var xStep = (end.X - start.X) / pointCount;
            var yStep = (end.Y - start.Y) / pointCount;

            for(int i = 0; i < pointCount; i++)
            {
                if (ArrowDirection == Direction.Right)
                {
                    if (i < (pointCount / 2))
                        roundedPoints.Add(new Point(start.X + (xStep * i) + i, start.Y + (yStep * i)));
                    else
                        roundedPoints.Add(new Point(start.X + (xStep * i) + (pointCount - i), start.Y + (yStep * i)));
                }
                else if (ArrowDirection == Direction.Left)
                {
                    if (i < (pointCount / 2))
                        roundedPoints.Add(new Point(start.X + (xStep * i) - i, start.Y + (yStep * i)));
                    else
                        roundedPoints.Add(new Point(start.X + (xStep * i) - (pointCount - i), start.Y + (yStep * i)));
                }
            }
            return roundedPoints;
        }

        public void Activate()
        {
            IsVisited = true;
            State = State.AddFlag(ElementState.Activated);
            UpdateColor();
        }

        public void Deactivate()
        {
            State = State.RemoveFlag(ElementState.Activated);
            UpdateColor();
        }

        private void EdgeArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show($@"Id: {Id}
Text: {EdgeText.Text}
Position of last point: {EdgeArrow.Points.Last().X},{EdgeArrow.Points.Last().Y}");
        }

        private void EdgeText_MouseEnter(object sender, MouseEventArgs e)
        {
            SetColor(ColorManager.HoverColor);
        }

        private void EdgeText_MouseLeave(object sender, MouseEventArgs e)
        {
            // reset color
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (State == ElementState.Normal) // just use normal color
                SetColor(ColorNormal);
            else
            {
                if (State.HasFlag(ElementState.Activated)) // force activated color
                    SetColor(ColorManager.ActivatedColor);
                else if (State.HasFlag(ElementState.Filtered)) // use filtered color
                    SetColor(ColorManager.FilteredColor);
            }
        }

        private void SetColor(Brush color)
        {
            EdgeArrow.Dispatcher.InvokeOnUIThread(() =>
                EdgeArrow.Stroke = color);
        }

        public void UpdateHeat(double heat)
        {
            _lastHeat = heat;
            ColorNormal = GetColor(_lastHeat);
            UpdateColor();
        }

        private Brush GetColor(double heat)
        {
            if (HasErrors)
                return ColorManager.GetRedBrush(heat);
            return ColorManager.GetGreenBrush(heat);
        }

        public void AddError(ExecutingElementMethodError error)
        {
            Errors.Add(error);
            ColorNormal = GetColor(_lastHeat);
            UpdateColor();
        }

        public void Filter(List<Predicate<IDynamicModelElement>> filter)
        {
            if (filter.Any(f => f(this)))
            {
                EdgeGrid.Visibility = Visibility.Hidden;
                State = State.AddFlag(ElementState.Filtered);
            }
            else
            {
                EdgeGrid.Visibility = Visibility.Visible;
                State = State.RemoveFlag(ElementState.Filtered);
            }
            UpdateColor(); // keeping this in case of only wanting to change the color of filtered elements
        }
    }
}
