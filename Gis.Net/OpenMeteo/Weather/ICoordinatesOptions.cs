namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Represents the options for specifying geographic coordinates.
/// </summary>
public interface ICoordinatesOptions
{
    /// <summary>
    /// Gets or sets the latitude coordinate for weather data retrieval.
    /// </summary>
    /// <value>
    /// The latitude as a double, or null if not set.
    /// </value>
    double? Lat { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate for weather data retrieval.
    /// </summary>
    /// <value>
    /// The longitude as a double, or null if not set.
    /// </value>
    double? Lng { get; set; }
}