using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using Gis.Net.Spatial.DTO;

namespace Gis.Net.Vector.DTO;

public class GisRequest : RequestBase, IGisRequest
{
    [JsonPropertyName("geomFilter")] public string? GeomFilter { get; set; }

    [JsonPropertyName("measure")] public bool Measure { get; set; }

    [JsonPropertyName("srCode"), JsonIgnore]
    public int? SrCode { get; set; } = (int)ESrCode.WebMercator;
    
    [JsonPropertyName("buffer")] public double? Buffer { get; set; }
    
    [JsonPropertyName("boundary")] public bool? Boundary { get; set; }
    
    [JsonPropertyName("difference")] public bool? Difference { get; set; }
}