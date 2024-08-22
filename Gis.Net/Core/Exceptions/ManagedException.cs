namespace Gis.Net.Core.Exceptions;

/// <summary>
/// Represents a managed exception that can be thrown in the application.
/// </summary>
/// <remarks>
/// This class is intended to be subclassed to create custom exceptions for specific scenarios.
/// </remarks>
public class ManagedException : Exception
{
    /// <summary>
    /// Represents the HTTP status code associated with a web request or response.
    /// </summary>
    public int HttpStatus { get; set; } = 400;
    
    /// <summary>
    /// Gets or sets the message to be logged for this exception.
    /// </summary>
    public string? MessageToLog { get; set; }
    
    /// <summary>
    /// Represents an exception with additional details.
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Represents a managed exception.
    /// </summary>
    public ManagedException(string message) : base(message) => this.MessageToLog = message;

    /// <summary>
    /// Represents a managed exception that can be thrown in the application.
    /// </summary>
    /// <remarks>
    /// The ManagedException class inherits from the Exception class and adds additional properties for HTTP status code,
    /// message to log, and details of the exception.
    /// </remarks>
    public ManagedException(string message, int status) : this(message) => this.HttpStatus = status;

    /// <summary>
    /// Represents a managed exception that can be thrown in the application.
    /// </summary>
    public ManagedException(string message, Exception e) : this(message) {
        MessageToLog = e.Message;
        Details = e.Message;
    }
}