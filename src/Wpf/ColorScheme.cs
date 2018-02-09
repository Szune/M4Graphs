using System.Windows.Media;

namespace M4Graphs.Wpf
{
    /// <summary>
    /// A helper class for managing colors of elements.
    /// </summary>
    public class ColorScheme
    {
        /// <summary>
        /// Returns the color used for elements the user hovers over.
        /// </summary>
        public Brush HoverColor { get; private set; } = Brushes.CadetBlue;
        /// <summary>
        /// Returns the color used for activated elements.
        /// </summary>
        public Brush ActivatedColor { get; private set; } = Brushes.Lavender;

        /// <summary>
        /// Returns the color used for filtered elements.
        /// </summary>
        public Brush FilteredColor { get; private set; } = Brushes.LightSteelBlue;

        public Brush EdgeColor { get; private set; } = Brushes.Black;

        public Brush NodeColor { get; private set; } = Brushes.DarkGoldenrod;

        private const int LowestColor = 40;

        public static ColorScheme Default => new ColorScheme();

        public ColorScheme()
        {
        }

        public ColorScheme(Brush hoverColor, Brush activatedColor, Brush filteredColor, Brush edgeColor, Brush nodeColor)
        {
            HoverColor = hoverColor;
            ActivatedColor = activatedColor;
            FilteredColor = filteredColor;
            EdgeColor = edgeColor;
            NodeColor = nodeColor;
        }


        public void SetFilteredColor(Brush color)
        {
            FilteredColor = color;
        }

        public void SetActivatedColor(Brush color)
        {
            ActivatedColor = color;
        }

        public void SetHoverColor(Brush color)
        {
            HoverColor = color;
        }

        public void SetEdgeColor(Brush color)
        {
            EdgeColor = color;
        }

        public void SetNodeColor(Brush color)
        {
            NodeColor = color;
        }

        /// <summary>
        /// Gets a green <see cref="Brush"/> based on the specified heat.
        /// </summary>
        /// <param name="heat"></param>
        /// <returns></returns>
        public static Brush GetGreenBrush(double heat)
        {
            return new SolidColorBrush(GetGreenColor(heat));
        }

        /// <summary>
        /// Gets a green <see cref="Color"/> based on the specified heat.
        /// </summary>
        /// <param name="heat"></param>
        /// <returns></returns>
        public static Color GetGreenColor(double heat)
        {
            return new Color { A = 255, R = 0, G = (byte)(LowestColor + ((250 - LowestColor) * heat)), B = 0 };
        }

        /// <summary>
        /// Gets a red <see cref="Brush"/> based on the specified heat.
        /// </summary>
        /// <param name="heat"></param>
        /// <returns></returns>
        public static Brush GetRedBrush(double heat)
        {
            return new SolidColorBrush(GetRedColor(heat));
        }

        /// <summary>
        /// Gets a red <see cref="Color"/> based on the specified heat.
        /// </summary>
        /// <param name="heat"></param>
        /// <returns></returns>
        public static Color GetRedColor(double heat)
        {
            return new Color { A = 255, R = (byte)(LowestColor + ((250 - LowestColor) * heat)), G = 0, B = 0 };
        }
    }
}
