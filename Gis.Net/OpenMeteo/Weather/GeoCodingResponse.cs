using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.Weather;

/// <inheritdoc />
public class GeoCodingResponse : IGeocodingResponse
{
    /// <inheritdoc />
    [JsonPropertyName("elevation")]
    public List<double?> Elevation { get; set; }
}