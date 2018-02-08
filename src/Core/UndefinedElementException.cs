using System;

namespace M4Graphs.Core
{
    /// <summary>
    /// The exception that is thrown when an undefined element appears.
    /// </summary>
    public class UndefinedElementException : Exception
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UndefinedElementException(string message) : base(message)
        {
        }
    }
}
