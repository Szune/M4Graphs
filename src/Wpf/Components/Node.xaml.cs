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
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node
    {
        private Brush ColorNormal = Brushes.DarkGoldenrod;

        public override string Id { get; }
        public override ElementStates States { get; protected set; } = ElementStates.Normal;
        public override bool IsVisited { get; protected set; }

        public override bool HasErrors => Errors.Any();

        public List<ExecutingElementMethodError> Errors { get; } = new List<ExecutingElementMethodError>();

        private double _lastHeat;

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position",
            typeof(Coordinate), typeof(Node),
            new FrameworkPropertyMetadata(Coordinate.Zero,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public override Coordinate Position
        {
            get { return (Coordinate)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public Node()
        {
            InitializeComponent();
        }

        public Node(string id, string text, double x, double y, double width, double height) : this()
        {
            Id = id;
            NodeText.Text = text;
            NodeBackground.Width = width;
            NodeBackground.Height = height;
            NodeBackground.Margin = new Thickness(x, y, 0, 0);
            TextBorder.Margin = new Thickness(x, y, 0, 0);
            SetPosition(x + width, y + height);
        }

        private void SetPosition(double maxX, double maxY)
        {
            Position = new Coordinate(maxX, maxY);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show($@"Id: {Id}
Text: {NodeText.Text}
Width: {NodeBackground.Width}
Height: {NodeBackground.Height}
Position: {NodeBackground.Margin.Left},{NodeBackground.Margin.Top}");
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

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            SetColor(ColorManager.HoverColor);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            // reset color
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (States == ElementStates.Normal) // just use normal color
                SetColor(ColorNormal);
            else
            {
                if (States.HasFlag(ElementStates.Activated)) // force activated color
                    SetColor(ColorManager.ActivatedColor);
                else if (States.HasFlag(ElementStates.Filtered)) // use filtered color
                    SetColor(ColorManager.FilteredColor);
            }
        }

        private void SetColor(Brush color)
        {
            NodeBackground.Dispatcher.InvokeOnUIThread(() =>
                NodeBackground.Background = color);
        }

        public override void UpdateHeat(double heat)
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

        public override void AddError(ExecutingElementMethodError error)
        {
            Errors.Add(error);
            ColorNormal = GetColor(_lastHeat);
            UpdateColor();
        }

        public override void Filter(List<Predicate<IModelElement>> filter)
        {
            if (filter.Any(f => f(this)))
            {
                NodeGrid.Visibility = Visibility.Hidden;
                States = States.AddFlag(ElementStates.Filtered);
            }
            else
            {
                NodeGrid.Visibility = Visibility.Visible;
                States = States.RemoveFlag(ElementStates.Filtered);
            }

            UpdateColor(); // keeping this in case of only wanting to change the color of filtered elements
        }
    }
}
