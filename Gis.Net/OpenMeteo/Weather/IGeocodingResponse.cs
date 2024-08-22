namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Represents a response from a geocoding service.
/// </summary>
public interface IGeocodingResponse
{
    /// <summary>
    /// Gets or sets the elevation.
    /// </summary>
    /// <value>
    /// The elevation.
    /// </value>
    List<double?> Elevation { get; set; }
}