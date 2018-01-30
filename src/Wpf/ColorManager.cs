using System.Windows.Media;

namespace M4Graphs.Wpf
{
    /// <summary>
    /// A helper class for managing colors of elements.
    /// </summary>
    public static class ColorManager
    {
        /// <summary>
        /// Returns the color used for elements the user hovers over.
        /// </summary>
        public static Brush HoverColor = Brushes.CadetBlue;
        /// <summary>
        /// Returns the color used for activated elements.
        /// </summary>
        public static Brush ActivatedColor = Brushes.Lavender;

        /// <summary>
        /// Returns the color used for filtered elements.
        /// </summary>
        public static Brush FilteredColor = Brushes.LightSteelBlue;

        private const int lowestColor = 40;

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
            return new Color { A = 255, R = 0, G = (byte)(lowestColor + ((250 - lowestColor) * heat)), B = 0 };
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
            return new Color { A = 255, R = (byte)(lowestColor + ((250 - lowestColor) * heat)), G = 0, B = 0 };
        }
    }
}
