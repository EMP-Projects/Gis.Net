using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Represents daily weather data.
/// </summary>
public class Daily
{
    /// <summary>
    /// Gets or sets the list of times for the daily weather data.
    /// </summary>
    /// <remarks>
    /// Each string in the list represents a date or time point.
    /// </remarks>
    [JsonPropertyName("time")]
    public List<string>? Time { get; set; }
    
    /// <summary>
    /// Gets or sets the list of weather codes for the daily weather data.
    /// </summary>
    /// <remarks>
    /// Each integer in the list corresponds to a specific weather condition code.
    /// </remarks>
    [JsonPropertyName("weather_code")]
    public List<int?>? WeatherCode { get; set; }

    /// <summary>
    /// Gets or sets the list of total rainfall amounts for each day.
    /// </summary>
    /// <remarks>
    /// Each value represents the sum of rainfall in millimeters over a day.
    /// </remarks>
    [JsonPropertyName("rain_sum")]
    public List<double?>? RainSum { get; set; }
}