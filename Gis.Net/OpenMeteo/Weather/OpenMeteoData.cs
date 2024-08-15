using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.Weather;

/// <inheritdoc />
public class OpenMeteoData : IOpenMeteoData
{
    /// <inheritdoc />
    [JsonPropertyName("lat")] public double? Lat { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("lng")] public double? Lng { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("elevation")] public double? Elevation { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("timezone")] public string? Timezone { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("description")] public string? Description { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("date")] public DateTime? Date { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("value")] public double? Value { get; set; }
}