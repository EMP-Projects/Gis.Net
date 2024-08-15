namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Interface representing the data structure for OpenMeteo weather information.
/// </summary>
public interface IOpenMeteoData
{
    /// <summary>
    /// Gets or sets the latitude coordinate of the location.
    /// </summary>
    double? Lat { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude coordinate of the location.
    /// </summary>
    double? Lng { get; set; }
    
    /// <summary>
    /// Gets or sets the elevation of the location in meters.
    /// </summary>
    double? Elevation { get; set; }
    
    /// <summary>
    /// Gets or sets the timezone identifier for the location.
    /// </summary>
    string? Timezone { get; set; }
    
    /// <summary>
    /// Gets or sets the description of the weather condition.
    /// </summary>
    string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time for the weather data.
    /// </summary>
    DateTime? Date { get; set; }
    
    /// <summary>
    /// Gets or sets the value representing a specific weather parameter (e.g., temperature, humidity).
    /// </summary>
    double? Value { get; set; }
}