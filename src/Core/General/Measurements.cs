using System;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// A helper class for simple measurements.
    /// </summary>
    public static class Measurements
    {
        /// <summary>
        /// The minimum width of a node.
        /// </summary>
        public static int NodeMinWidth { get; set; } = 50;
        /// <summary>
        /// Interpolation count. Not yet used. TODO: use.
        /// </summary>
        public const int InterpolationCount = 20;
        private const int NodeWidthMargin = 10;
        private static Random rand = new Random(159159133);

        /// <summary>
        /// Returns an appropriate width for a node with the specified text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int NodeWidth(string text)
        {
            // TODO: Measure actual text with font, don't use hardcoded constants
            int hardcodedNumberChangeASAP = 8; // number that will most likely only work with the default Wpf textblock font
            return (text.Length * hardcodedNumberChangeASAP) + NodeWidthMargin;
        }

        /// <summary>
        /// Returns the amount of x-levels a node with the specified text would require to not overlap another node.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int TextToXLevel(string text)
        {
            // TODO: Finish the TODO in NodeWidth()
            return NodeWidth(text) / NodeMinWidth;
        }

        /// <summary>
        /// Returns a random value between the minimum and maximum value specified.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            // TODO: Fit this somewhere else.
            return rand.Next(minValue, maxValue);
        }
    }
}
