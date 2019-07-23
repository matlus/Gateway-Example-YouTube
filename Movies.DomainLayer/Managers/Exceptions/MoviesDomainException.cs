using System;
using System.Runtime.Serialization;

namespace Movies.DomainLayer.Managers.Exceptions
{

    [Serializable]
    public abstract class MoviesDomainException : MoviesBaseException
    {
        protected MoviesDomainException() { }
        protected MoviesDomainException(string message) : base(message) { }
        protected MoviesDomainException(string message, Exception inner) : base(message, inner) { }
        protected MoviesDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected MoviesDomainException(params ExceptionMessageDetail[] exceptionMessageDetails) : base(exceptionMessageDetails) { }
    }
}
