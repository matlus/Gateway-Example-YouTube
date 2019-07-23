using System;
using System.Runtime.Serialization;

namespace MovieClientApplication.Exceptions
{

    [Serializable]
    public class MoviesDomainException : MoviesBaseException
    {
        public MoviesDomainException() { }
        public MoviesDomainException(string message) : base(message) { }
        public MoviesDomainException(string message, Exception inner) : base(message, inner) { }
        protected MoviesDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
