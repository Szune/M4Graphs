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
        /// Interpolation count. Not yet used. Use when making more complex edges.
        /// </summary>
        public const int InterpolationCount = 20;
        private const int NodeWidthMargin = 10;
        private static readonly Random Rand = new Random(159159133);

        /// <summary>
        /// Returns an appropriate width for a node with the specified text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int NodeWidthFromText(string text)
        {
            // TODO: Measure actual text with font, don't use hardcoded constants
            const int hardcodedNumberChangeAsap = 8;
            return (text.Length * hardcodedNumberChangeAsap) + NodeWidthMargin;
        }

        /// <summary>
        /// Returns a random value between the minimum and maximum value specified.
        /// </summary>
        public static int GetRandom(int minValue, int maxValue)
        {
            // TODO: Fit this somewhere else.
            return Rand.Next(minValue, maxValue);
        }
    }
}
