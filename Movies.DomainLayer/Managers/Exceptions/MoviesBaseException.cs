using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Movies.DomainLayer.Managers.Exceptions
{
    [Serializable]
    public abstract class MoviesBaseException : Exception
    {
        protected readonly List<Notice> notices = new List<Notice>();
        private string Id { get { return GetType().Name; } }
        public string Reason { get { return ToWords(); } }
        public IEnumerable<Notice> Notices { get { return notices; } }

        public override string Message
        {
            get
            {
                var sb = new StringBuilder();

                foreach (var notice in Notices)
                {
                    if (notice.Property != null)
                        sb.AppendLine("Property: " + notice.Property + ". Message: " + notice.Message);
                    else
                        sb.AppendLine("Message: " + notice.Message);
                }
                return sb.ToString();
            }
        }

        protected MoviesBaseException() { }
        protected MoviesBaseException(string message) : base(message) { }
        protected MoviesBaseException(string message, Exception inner) : base(message, inner) { }
        protected MoviesBaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        protected MoviesBaseException(params ExceptionMessageDetail[] exceptionMessageDetails)
        {
            Source = "Movies Service";

            foreach (var exceptionMessageDetail in exceptionMessageDetails)
            {
                notices.Add(new Notice(exceptionMessageDetail.Message, exceptionMessageDetail.ParameterName, Severity.Error, Source));
            }
        }

        private string ToWords()
        {
            return Regex.Replace(GetType().Name, "[a-z][A-Z]", m => m.Value[0] + " " + m.Value[1]);
        }
    }

    public sealed class ExceptionMessageDetail
    {
        private readonly string message;
        public string Message { get { return message; } }

        private readonly string parameterName;
        public string ParameterName { get { return parameterName; } }

        public ExceptionMessageDetail(string message, string parameterName)
        {
            this.message = message;
            this.parameterName = parameterName;
        }
        public ExceptionMessageDetail(string message)
            :this(message, null)
        {
        }
    }

    public sealed class Notice
    {        
        public string Message { get; }

        public string Property { get; }

        public Severity Severity { get; }

        public string Source { get; }

        public Notice(string message, string property, Severity severity, string source)
        {
            Message = message;
            Property = property;
            Severity = severity;
            Source = source;
        }
    }

    public sealed class Parameter
    {
        public string Name { get; }

        public string Value { get; }

        public Parameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public enum Severity { Critical, Error, Warning, Informational }

}
