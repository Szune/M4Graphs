using System;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// A wrapper for an exception thrown during execution of a method associated with an element in the model.
    /// </summary>
    public class ExecutingElementMethodError
    {
        /// <summary>
        /// Returns the element's identifier.
        /// </summary>
        public string ElementId;
        /// <summary>
        /// Returns the error message.
        /// </summary>
        public string Message;
        /// <summary>
        /// Returns the associated <see cref="Exception"/>, if any.
        /// </summary>
        public Exception ThrownException;
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="elementId"></param>
        /// <param name="message"></param>
        /// <param name="thrownException"></param>
        public ExecutingElementMethodError(string elementId, string message, Exception thrownException)
        {
            ElementId = elementId;
            Message = message;
            ThrownException = thrownException;
        }
    }

    /// <summary>
    /// An <see cref="ExecutingElementMethodError"/> error not caused by a thrown exception.
    /// </summary>
    public class GenericExecutingElementMethodError : ExecutingElementMethodError
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="elementId"></param>
        public GenericExecutingElementMethodError(string elementId) : base(elementId, "GenericError", null)
        {
        }
    }
}
