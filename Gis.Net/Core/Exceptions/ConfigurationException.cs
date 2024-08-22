namespace Gis.Net.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a configuration key is missing or invalid.
/// </summary>
public class ConfigurationException : Exception
{
    /// <summary>
    /// Represents an error message.
    /// </summary>
    private const string ErrorMessage = "Missing or invalid configuration key: {0}";

    /// <summary>
    /// Represents an exception that is thrown when there is a missing or invalid configuration key.
    /// </summary>
    public ConfigurationException(string key) : base(string.Format(ErrorMessage, key)) { }
}