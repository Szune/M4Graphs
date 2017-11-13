using System;
using System.Collections.Generic;
using System.Text;

namespace M4Graphs.Core
{
    /// <summary>
    /// The exception that is thrown when the <see cref="ModelGenerator"/> is trying to expand by adding an unknown element.
    /// </summary>
    public class UnknownElementException : Exception
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message"></param>
        public UnknownElementException(string message) : base(message)
        {
        }
    }
}
