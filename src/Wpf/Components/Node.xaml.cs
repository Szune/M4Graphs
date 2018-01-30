using M4Graphs.Core.General;
using M4Graphs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace M4Graphs.Wpf.Components
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl, IDynamicModelElement
    {
        private Brush ColorNormal = Brushes.DarkGoldenrod;
        private Brush ColorReset = Brushes.DarkGoldenrod;

        public string Id { get; private set; }
        public ElementState State { get; private set; } = ElementState.Normal;
        public bool IsVisited { get; private set; }

        public bool HasErrors => Errors.Any();

        public List<ExecutingElementMethodError> Errors = new List<ExecutingElementMethodError>();

        private double _lastHeat;

        public static DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(PathPoint), typeof(Node), new FrameworkPropertyMetadata(PathPoint.Zero, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public PathPoint Position
        {
            get { return (PathPoint)GetValue(PositionProperty); }
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
            Position = new PathPoint(maxX, maxY);
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
            NodeBackground.Dispatcher.InvokeOnUIThread(() =>
                NodeBackground.Background = color);
        }

        public void UpdateHeat(double heat)
        {
            _lastHeat = heat;
            ColorNormal = GetColor(_lastHeat);
            UpdateColor();
        }

        private Brush GetColor(double heat)
        {
            if(HasErrors)
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
                NodeGrid.Visibility = Visibility.Hidden;
                State = State.AddFlag(ElementState.Filtered);
            }
            else
            {
                NodeGrid.Visibility = Visibility.Visible;
                State = State.RemoveFlag(ElementState.Filtered);
            }
            UpdateColor(); // keeping this in case of only wanting to change the color of filtered elements
        }
    }
}
