using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Represents the units of measurement for the daily weather data.
/// </summary>
public class DailyUnits
{
    /// <summary>
    /// Gets or sets the unit of time measurement.
    /// </summary>
    /// <remarks>
    /// This is typically a string representation of the date format.
    /// </remarks>
    [JsonPropertyName("time")]
    public string? Time { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of measurement for weather codes.
    /// </summary>
    /// <remarks>
    /// This could be a string indicating the coding system used for weather conditions (e.g., "metar").
    /// </remarks>
    [JsonPropertyName("weather_code")]
    public string? WeatherCode { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of measurement for the sum of rain.
    /// </summary>
    /// <remarks>
    /// This is typically a string indicating the unit used for measuring rainfall, such as "mm" for millimeters.
    /// </remarks>
    [JsonPropertyName("rain_sum")]
    public string? RainSum { get; set; }
}
