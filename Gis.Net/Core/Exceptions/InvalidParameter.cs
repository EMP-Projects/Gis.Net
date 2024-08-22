namespace Gis.Net.Core.Exceptions;

/// <summary>
/// Exception that is thrown when an invalid parameter is encountered.
/// </summary>
public class InvalidParameter : Exception
{
    /// <summary>
    /// Exception thrown when a parameter is absent or invalid.
    /// </summary>
    public InvalidParameter(string paramName) : base($"{paramName} parameter absent or invalid") { }
}