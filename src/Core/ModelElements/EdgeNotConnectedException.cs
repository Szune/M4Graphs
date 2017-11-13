using System;

namespace M4Graphs.Core.ModelElements
{
    /// <summary>
    /// The exception that is thrown when a <see cref="ModelEdge"/> is not connected to any <see cref="ModelNode"/>.
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
