using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents the hourly air quality units.
/// </summary>
public partial class HourlyUnits
{
    /// <summary>
    /// Represents the hourly units for sulphur dioxide in air quality data.
    /// </summary>
    [JsonPropertyName("sulphur_dioxide")]
    public string? SulphurDioxide { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) for PM2.5 particulate matter.
    /// </summary>
    [JsonPropertyName("european_aqi_pm2_5")]
    public string? EuropeanAqiPm25 { get; set; }

    /// <summary>
    /// Represents the Nitrogen Dioxide property in the HourlyUnits class.
    /// </summary>
    [JsonPropertyName("nitrogen_dioxide")]
    public string? NitrogenDioxide { get; set; }

    /// <summary>
    /// Represents the PM10 property in the HourlyUnits class.
    /// </summary>
    [JsonPropertyName("pm10")]
    public string? Pm10 { get; set; }

    /// <summary>
    /// Represents the ozone property.
    /// </summary>
    [JsonPropertyName("ozone")]
    public string? Ozone { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) value for ozone.
    /// </summary>
    [JsonPropertyName("european_aqi_ozone")]
    public string? EuropeanAqiOzone { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index for Nitrogen Dioxide.
    /// </summary>
    [JsonPropertyName("european_aqi_nitrogen_dioxide")]
    public string? EuropeanAqiNitrogenDioxide { get; set; }

    /// <summary>
    /// Represents the European AQI (Air Quality Index) value for PM10 particles.
    /// </summary>
    [JsonPropertyName("european_aqi_pm10")]
    public string? EuropeanAqiPm10 { get; set; }

    /// <summary>
    /// Represents the value of PM2.5 (particulate matter) in the air.
    /// </summary>
    [JsonPropertyName("pm2_5")]
    public string? Pm25 { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) value for Sulphur Dioxide (SO2).
    /// </summary>
    [JsonPropertyName("european_aqi_sulphur_dioxide")]
    public string? EuropeanAqiSulphurDioxide { get; set; }

    /// <summary>
    /// Represents the time property of the HourlyUnits class.
    /// </summary>
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    /// Represents the property for Carbon Monoxide.
    /// /
    [JsonPropertyName("carbon_monoxide")]
    public string? CarbonMonoxide { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) for a specific time.
    /// </summary>
    [JsonPropertyName("european_aqi")]
    public string? EuropeanAqi { get; set; }
}