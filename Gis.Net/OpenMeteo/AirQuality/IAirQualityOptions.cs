namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents the options for retrieving air quality data.
/// </summary>
public interface IAirQualityOptions
{
    /// <summary>
    /// Represents the options for retrieving air quality data.
    /// </summary>
    AirQualityLatLng[] Points { get; set; }
    
    /// <summary>
    /// Represents the time zone for retrieving air quality data.
    /// </summary>
    string? TimeZone { get; set; }

    /// <summary>
    /// Represents the number of forecast days for retrieving air quality data.
    /// </summary>
    int? ForecastDays { get; set; }

    /// <summary>
    /// Represents the number of forecast hours for retrieving air quality data.
    /// </summary>
    int? ForecastHours { get; set; }
}