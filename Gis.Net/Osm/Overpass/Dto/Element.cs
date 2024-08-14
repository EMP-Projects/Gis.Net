using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

public class Element
{

    [JsonPropertyName("type")]
    public string? Type;

    [JsonPropertyName("id")]
    public long? Id;
    
    [JsonPropertyName("bounds")]
    public Bounds? Bounds;

    [JsonPropertyName("nodes")]
    public List<object>? Nodes;
    
    [JsonPropertyName("geometry")]
    public List<GeometryOsm>? Geometry;
    
    [JsonPropertyName("tags")]
    public object? Tags;

    [JsonPropertyName("members")]
    public List<Member>? Members;
    
    [JsonPropertyName("lat")]
    public double Lat;

    [JsonPropertyName("lon")]
    public double Lon;
}