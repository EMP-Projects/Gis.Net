namespace Gis.Net.Nominatim;

/// <summary>
/// Represents exceptions that occur during the execution of the Nominatim service.
/// </summary>
public class NominatimExceptions : Exception
{
    /// <summary>
    /// Exception class for Nominatim service.
    /// </summary>
    public NominatimExceptions(string message) : base(message) {}
}