using System.Text.Json.Serialization;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents the response of a measure calculation.
/// </summary>
public class MeasureResponse
{
    /// <summary>
    /// Represents the area measurement in a <see cref="MeasureResponse"/>.
    /// </summary>
    [JsonPropertyName("area")]
    public double? Area = 0;

    /// <summary>
    /// Represents the percentage value of an area compared to a reference geometry.
    /// </summary>
    [JsonPropertyName("percentage")]
    public double? Percentage = 0;

    /// <summary>
    /// Converts a length value from geometric units to meters.
    /// </summary>
    /// <param name="length">The length value in geometric units.</param>
    /// <returns>The length value in meters.</returns>
    [JsonPropertyName("lenght")]
    public double? Lenght  = 0;
}