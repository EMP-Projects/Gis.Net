using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

public class AirQualityData: IAirQualityData
{
    [JsonPropertyName("time")]
    public DateTime? Time { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("value")]
    public double? Value { get; set; }
    
    [JsonPropertyName("unit")]
    public string? Unit { get; set; }
    
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    
    [JsonPropertyName("hexColor")]
    public string? HexColor { get; set; }
}