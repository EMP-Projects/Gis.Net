using System.Text.Json.Serialization;

namespace Gis.Net.Vector.DTO;

public class MeasureResponse
{
    [JsonPropertyName("area")]
    public double? Area = 0;
        
    [JsonPropertyName("percentage")]
    public double? Percentage = 0;
        
    [JsonPropertyName("lenght")]
    public double? Lenght  = 0;
}