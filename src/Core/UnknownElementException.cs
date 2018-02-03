using System;

namespace M4Graphs.Core
{
    /// <summary>
    /// The exception that is thrown when the generator is trying to expand by adding an unknown element.
    /// </summary>
    public class UnknownElementException : Exception
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UnknownElementException(string message) : base(message)
        {
        }
    }
}
