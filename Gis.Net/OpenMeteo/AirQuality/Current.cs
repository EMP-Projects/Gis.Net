using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents the current air quality measurements.
/// </summary>
public partial class Current
{
    /// <summary>
    /// Represents the measurement of Sulphur Dioxide in air quality.
    /// </summary>
    [JsonPropertyName("sulphur_dioxide")]
    public double? SulphurDioxide { get; set; }

    /// <summary>
    /// Represents the measurement of PM2.5 particles in the air.
    /// </summary>
    [JsonPropertyName("pm2_5")]
    public double? Pm25 { get; set; }

    /// <summary>
    /// Represents the value of nitrogen dioxide in the current air quality data.
    /// </summary>
    [JsonPropertyName("nitrogen_dioxide")]
    public double? NitrogenDioxide { get; set; }

    [JsonPropertyName("pm10")]
    public double? Pm10 { get; set; }

    /// <summary>
    /// Represents the interval of air quality data.
    /// </summary>
    [JsonPropertyName("interval")]
    public long? Interval { get; set; }

    /// <summary>
    /// Represents the time when the air quality data was recorded.
    /// </summary>
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    /// <summary>
    /// Represents the ozone property of the Current class.
    /// </summary>
    [JsonPropertyName("ozone")]
    public double? Ozone { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI).
    /// </summary>
    [JsonPropertyName("european_aqi")]
    public long? EuropeanAqi { get; set; }

    /// <summary>
    /// Represents the carbon monoxide level.
    /// </summary>
    [JsonPropertyName("carbon_monoxide")]
    public double? CarbonMonoxide { get; set; }
}