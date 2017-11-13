using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Core.Converters.Graphml
{
    /// <summary>
    /// The exception that is thrown when the <see cref="Graphml"/> converter is unable to
    /// discern an element's type.
    /// </summary>
    public class GraphmlElementFormatException : Exception
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message"></param>
        public GraphmlElementFormatException(string message) : base(message) { }
    }
}
