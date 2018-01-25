using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// Margins used in a model.
    /// </summary>
    public class Margin
    {
        /// <summary>
        /// The x-margin.
        /// </summary>
        public int X { get; }
        /// <summary>
        /// The y-margin.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Margin(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
