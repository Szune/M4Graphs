using M4Graphs.Core.General;
using M4Graphs.Core.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace M4Graphs.Wpf.Components
{
    /// <summary>
    /// Interaction logic for Edge.xaml
    /// </summary>
    public partial class Edge
    {
        private Brush _colorNormal;
        private readonly ColorScheme _colorScheme;

        public override string Id { get; }
        public override ElementStates States { get; protected set; } = ElementStates.Normal;
        public override bool IsVisited { get; protected set; }

        public override bool HasErrors => Errors.Any();

        public List<ExecutingElementMethodError> Errors { get; } = new List<ExecutingElementMethodError>();


        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(Coordinate), typeof(Edge), new FrameworkPropertyMetadata(Coordinate.Zero, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
        public override Coordinate Position
        {
            get => (Coordinate)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        private double _lastHeat;

        public Edge(string id, string text, PointCollection points)
        {
            InitializeComponent();
            Id = id;
            SetPoints(points);
            var x = points.Min(p => p.X);
            var y = points.Min(p => p.Y);
            EdgeText.Text = text;
            EdgeText.Margin = new Thickness(x, y, 0, 0);
            _colorScheme = ColorScheme.Default;
            _colorNormal = _colorScheme.EdgeColor;
        }


        public Edge(string id, string text, PointCollection points, Point labelPoint) : this(id, text, points)
        {
            EdgeText.Margin = new Thickness(labelPoint.X, labelPoint.Y, 0, 0);
        }

        public Edge(string id, string text, PointCollection points, ColorScheme colorScheme) : this(id, text, points)
        {
            _colorScheme = colorScheme;
            _colorNormal = colorScheme.EdgeColor;
        }

        public Edge(string id, string text, PointCollection points, Point labelPoint, ColorScheme colorScheme) : this(id, text, points, labelPoint)
        {
            _colorScheme = colorScheme;
            _colorNormal = colorScheme.EdgeColor;
        }

        private void SetPoints(PointCollection points)
        {
            EdgeArrow.Points = points;
            EdgeMouseOver.Points = points;
            var x = points?.Max(p => p.X) ?? 0;
            var y = points?.Max(p => p.Y) ?? 0;
            Position = new Coordinate(x, y);
        }

        public override void Activate()
        {
            IsVisited = true;
            States = States.AddFlag(ElementStates.Activated);
            UpdateColor();
        }

        public override void Deactivate()
        {
            States = States.RemoveFlag(ElementStates.Activated);
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
            SetColor(_colorScheme.HoverColor);
        }

        private void EdgeText_MouseLeave(object sender, MouseEventArgs e)
        {
            // reset color
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (States == ElementStates.Normal) // just use normal color
                SetColor(_colorNormal);
            else
            {
                if (States.HasFlag(ElementStates.Activated)) // force activated color
                    SetColor(_colorScheme.ActivatedColor);
                else if (States.HasFlag(ElementStates.Filtered)) // use filtered color
                    SetColor(_colorScheme.FilteredColor);
            }
        }

        private void SetColor(Brush color)
        {
            EdgeArrow.Dispatcher.InvokeOnUIThread(() =>
                EdgeArrow.Stroke = color);
        }

        public override void UpdateHeat(double heat)
        {
            _lastHeat = heat;
            _colorNormal = GetColor(_lastHeat);
            UpdateColor();
        }

        private Brush GetColor(double heat)
        {
            if (HasErrors)
                return ColorScheme.GetRedBrush(heat);
            return ColorScheme.GetGreenBrush(heat);
        }

        public override void AddError(ExecutingElementMethodError error)
        {
            Errors.Add(error);
            _colorNormal = GetColor(_lastHeat);
            UpdateColor();
        }

        public override void Filter(List<Predicate<IModelElement>> filter)
        {
            if (filter.Any(f => f(this)))
            {
                EdgeGrid.Visibility = Visibility.Hidden;
                States = States.AddFlag(ElementStates.Filtered);
            }
            else
            {
                EdgeGrid.Visibility = Visibility.Visible;
                States = States.RemoveFlag(ElementStates.Filtered);
            }
            UpdateColor(); // keeping this in case of only wanting to change the color of filtered elements
        }
    }
}
