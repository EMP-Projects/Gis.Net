namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Interface representing the response structure for OpenMeteo API calls.
/// </summary>
public interface IOpenMeteoResponse
{
    /// <summary>
    /// Gets or sets the latitude of the location for which the weather data is provided.
    /// </summary>
    double? Latitude { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude of the location for which the weather data is provided.
    /// </summary>
    double? Longitude { get; set; }
    
    /// <summary>
    /// Gets or sets the generation time of the weather data in milliseconds.
    /// </summary>
    double? GenerationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the UTC offset in seconds for the location's timezone.
    /// </summary>
    int? UtcOffsetSeconds { get; set; }
    
    /// <summary>
    /// Gets or sets the timezone identifier for the location.
    /// </summary>
    string? Timezone { get; set; }
    
    /// <summary>
    /// Gets or sets the abbreviated form of the timezone.
    /// </summary>
    string? TimezoneAbbreviation { get; set; }
    
    /// <summary>
    /// Gets or sets the elevation of the location in meters.
    /// </summary>
    double? Elevation { get; set; }
    
    /// <summary>
    /// Gets or sets the units of measurement for daily values.
    /// </summary>
    DailyUnits? DailyUnits { get; set; }
    
    /// <summary>
    /// Gets or sets the daily weather data.
    /// </summary>
    Daily? Daily { get; set; }
}