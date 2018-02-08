using System;

namespace M4Graphs.Core.Elements
{
    /// <summary>
    /// The exception that is thrown when a <see cref="DefaultEdgeElement"/> is not connected to any <see cref="DefaultNodeElement"/>.
    /// </summary>
    public class EdgeNotConnectedException : Exception
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message"></param>
        public EdgeNotConnectedException(string message) : base(message)
        {
        }
    }
}
