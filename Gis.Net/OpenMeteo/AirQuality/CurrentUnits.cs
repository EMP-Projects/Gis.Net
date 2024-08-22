using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents the units for the current air quality data.
/// </summary>
public partial class CurrentUnits
{
    /// <summary>
    /// Represents the value of sulphur dioxide in the current air quality data.
    /// </summary>
    [JsonPropertyName("sulphur_dioxide")]
    public string? SulphurDioxide { get; set; }

    /// <summary>
    /// Represents the PM 2.5 (particulate matter) property in air quality data.
    /// </summary>
    [JsonPropertyName("pm2_5")]
    public string? Pm25 { get; set; }

    /// <summary>
    /// Represents the current units for Nitrogen Dioxide.
    /// </summary>
    [JsonPropertyName("nitrogen_dioxide")]
    public string? NitrogenDioxide { get; set; }

    /// <summary>
    /// Represents the concentration of Particulate Matter with a diameter of 10 micrometers or less (PM10) in the air.
    /// </summary>
    [JsonPropertyName("pm10")]
    public string? Pm10 { get; set; }

    /// <summary>
    /// Represents the interval property of the CurrentUnits class.
    /// </summary>
    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    /// <summary>
    /// Represents the time property of the CurrentUnits class.
    /// </summary>
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    /// <summary>
    /// Represents the current ozone level.
    /// </summary>
    [JsonPropertyName("ozone")]
    public string? Ozone { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) value.
    /// </summary>
    [JsonPropertyName("european_aqi")]
    public string? EuropeanAqi { get; set; }

    /// <summary>
    /// Represents the value of carbon monoxide in the current air quality data.
    /// </summary>
    [JsonPropertyName("carbon_monoxide")]
    public string? CarbonMonoxide { get; set; }
}