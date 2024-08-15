using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.Weather;

public class GeoCodingResponse : IGeocodingResponse
{
    [JsonPropertyName("elevation")]
    public List<double?> Elevation { get; set; }
}