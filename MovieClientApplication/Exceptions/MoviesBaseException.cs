using System;
using System.Runtime.Serialization;

namespace MovieClientApplication.Exceptions
{
    [Serializable]
    public class MoviesBaseException : Exception
    {
        public MoviesBaseException() { }
        public MoviesBaseException(string message) : base(message) { }
        public MoviesBaseException(string message, Exception inner) : base(message, inner) { }
        protected MoviesBaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
