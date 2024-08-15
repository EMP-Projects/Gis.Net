namespace Gis.Net.Core.Exceptions
{
    public class ManagedException : Exception
    {
        public int HttpStatus { get; set; } = 400;
        public string? MessageToLog { get; set; }
        public string? Details { get; set; }
        public ManagedException(string message) : base(message) => this.MessageToLog = message;
        public ManagedException(string message, int status) : this(message) => this.HttpStatus = status;
        public ManagedException(string message, Exception e) : this(message) {
            this.MessageToLog = e.Message;
            this.Details = e.Message;
        }
    }
}

