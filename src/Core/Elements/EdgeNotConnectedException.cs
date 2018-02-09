using System;
using System.Runtime.Serialization;

namespace M4Graphs.Core.Elements
{
    /// <summary>
    /// The exception that is thrown when an edge is not connected to a node.
    /// </summary>
    [Serializable]
    public class EdgeNotConnectedException : Exception
    {
        public EdgeNotConnectedException()
        {
        }

        public EdgeNotConnectedException(string message) : base(message)
        {
        }

        public EdgeNotConnectedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EdgeNotConnectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
