namespace Gis.Net.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a specific object is not found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Represents an exception that is thrown when a specific object is not found.
    /// </summary>
    public NotFoundException(string model, int id)
        : base($"Id {id} not found for type {model}") { }

    /// <summary>
    /// Represents an exception that is thrown when a specific object is not found.
    /// </summary>
    public NotFoundException(string model, string key)
        : base($"Value {key} not found for type {model}") { }

    /// <summary>
    /// Represents an exception that is thrown when a specified model or key is not found.
    /// </summary>
    public NotFoundException(string message)
        : base(message) { }
}