using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents air quality data.
/// </summary>
/// <remarks>
/// This class is used to store information about air quality, such as time, name, value, unit, text, and hex color.
/// It is typically used in an air quality response object.
/// </remarks>
public class AirQualityData: IAirQualityData
{
    /// <summary>
    /// Represents the time of air quality data.
    /// </summary>
    [JsonPropertyName("time")]
    public DateTime? Time { get; set; }

    /// <summary>
    /// Represents the name of an air quality data.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents a property value associated with air quality data.
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// Represents an air quality data unit.
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; set; }

    /// <summary>
    /// Represents the text associated with air quality data.
    /// </summary>
    /// <remarks>
    /// This property is used in the <see cref="AirQualityData"/> class to store the textual representation
    /// of the air quality measurement. It provides additional information about the air quality value, such
    /// as the pollution level or the specific condition.
    /// </remarks>
    /// <seealso cref="AirQualityData"/>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Represents the hexadecimal color value associated with air quality data.
    /// </summary>
    [JsonPropertyName("hexColor")]
    public string? HexColor { get; set; }
}