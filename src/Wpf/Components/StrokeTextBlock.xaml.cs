using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace M4Graphs.Wpf.Components
{
    /// <summary>
    /// Interaction logic for StrokeTextBlock.xaml
    /// </summary>
    public partial class StrokeTextBlock : UserControl
    {
        public static readonly DependencyProperty TextBrushProperty = DependencyProperty.Register("TextBrush", typeof(Brush), typeof(StrokeTextBlock), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));
        public Brush TextBrush {
        get { return (Brush)GetValue(TextBrushProperty); }
        set { SetValue(TextBrushProperty, value); }
        }

        public static readonly DependencyProperty StrokeBrushProperty = DependencyProperty.Register("StrokeBrush", typeof(Brush), typeof(StrokeTextBlock), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush StrokeBrush {
            get { return (Brush)GetValue(StrokeBrushProperty); }
            set { SetValue(StrokeBrushProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(StrokeTextBlock), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(StrokeTextBlock), new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set
            {
                SetValue(TextAlignmentProperty, value);
            }
        }
    
        public StrokeTextBlock()
        {
            InitializeComponent();
        }
    }
}
