﻿using System;

namespace MovieClientApplication.Exceptions
{

    [Serializable]
    public sealed class InvalidGenreException : MoviesDomainException
    {
        public InvalidGenreException() { }
        public InvalidGenreException(string message) : base(message) { }
        public InvalidGenreException(string message, Exception inner) : base(message, inner) { }
        private InvalidGenreException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
