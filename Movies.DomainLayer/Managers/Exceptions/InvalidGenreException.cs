using System;
using System.Runtime.Serialization;

namespace Movies.DomainLayer.Managers.Exceptions
{

    [Serializable]
    public sealed class InvalidGenreException : MoviesDomainException
    {
        public InvalidGenreException() { }
        public InvalidGenreException(string message) : base(message) { }
        public InvalidGenreException(string message, Exception inner) : base(message, inner) { }
        public InvalidGenreException(params ExceptionMessageDetail[] exceptionMessageDetails)
            :base(exceptionMessageDetails)
        {
        }

        private InvalidGenreException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
