using System.Text;

namespace MovieClientApplication.Gateways.Models
{
    public sealed class Notice
    {
        private readonly string message;
        public string Message { get { return message; } }

        private readonly string property;
        public string Property { get { return property; } }

        private readonly string severity;
        public string Severity { get { return severity; } }

        private readonly string source;
        public string Source { get { return source; } }

        public Notice(string message, string property, string severity, string source)
        {
            this.message = message;
            this.property = property;
            this.severity = severity;
            this.source = source;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Property != null)
                sb.AppendLine("Property: " + Property + ". Message: " + Message);
            else
                sb.AppendLine("Message: " + Message);
            return sb.ToString();
        }
    }
}
