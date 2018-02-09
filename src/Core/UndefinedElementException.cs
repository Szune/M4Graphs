using System;
using System.Runtime.Serialization;

namespace M4Graphs.Core
{
    /// <summary>
    /// The exception that is thrown when an undefined element appears.
    /// </summary>
    [Serializable]
    public class UndefinedElementException : Exception
    {
        public UndefinedElementException()
        {
        }

        public UndefinedElementException(string message) : base(message)
        {
        }

        public UndefinedElementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UndefinedElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
