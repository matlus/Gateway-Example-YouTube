using System;
using System.Runtime.Serialization;

namespace MovieClientApplication.Exceptions
{

    [Serializable]
    public sealed class InternalRemoteServerException : MoviesBaseException
    {
        public InternalRemoteServerException() { }
        public InternalRemoteServerException(string message) : base(message) { }
        public InternalRemoteServerException(string message, Exception inner) : base(message, inner) { }
        private InternalRemoteServerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
