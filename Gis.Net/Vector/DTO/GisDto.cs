using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.DTO;

public class GisDto : DtoBase, IGisDto
{
    [JsonPropertyName("guid")]
    public Guid Guid { get; set; }
    
    [JsonPropertyName("geom"), JsonIgnore]
    public Geometry? Geom { get; set; }
}