using System;

namespace M4Graphs.Parsers.Graphml
{
    /// <summary>
    /// The exception that is thrown when the <see cref="GraphmlStringParser"/> converter is unable to
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
