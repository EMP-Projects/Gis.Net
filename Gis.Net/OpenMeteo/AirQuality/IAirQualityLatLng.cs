namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents a location with latitude and longitude for air quality data.
/// </summary>
public interface IAirQualityLatLng
{
    /// <summary>
    /// Gets or sets the name of the location for air quality data.
    /// </summary>
    /// <value>
    /// The name of the location.
    /// </value>
    string? Name { get; set; }
    
    /// <summary>
    /// Gets or sets the latitude of a location for air quality data.
    /// </summary>
    double Lat { get; set; }
    
    /// <summary>
    /// Represents a location with latitude and longitude for air quality data.
    /// </summary>
    double Lng { get; set; }
}