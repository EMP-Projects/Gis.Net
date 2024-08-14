using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

public class Member
{
    [JsonPropertyName("type")]
    public string? Type;
    
    [JsonPropertyName("ref")]
    public long? Ref;

    [JsonPropertyName("role")]
    public string? Role;

    [JsonPropertyName("geometry")]
    public List<GeometryOsm>? Geometry;
}