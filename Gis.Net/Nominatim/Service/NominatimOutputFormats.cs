namespace Gis.Net.Nominatim.Service;

/// <summary>
/// Represents the available output formats for Nominatim.
/// </summary>
public static class NominatimOutputFormats
{
    /// <summary>
    /// Represents the XML output format for the Nominatim service.
    /// </summary>
    public const string Xml = "xml";

    /// <summary>
    /// Represents the JSON format for Nominatim output.
    /// </summary>
    public const string Json = "json";

    /// <summary>
    /// Represents the GeoJSON format used by the Nominatim service.
    /// </summary>
    public const string GeoJson = "geojson";
}