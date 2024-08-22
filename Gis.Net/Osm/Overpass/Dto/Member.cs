using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents a member of an element in the Overpass API response.
/// </summary>
public class Member
{
    /// <summary>
    /// Represents the type information of a member in an Overpass response.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type;
    
    [JsonPropertyName("ref")]
    public long? Ref;

    /// <summary>
    /// Represents the role of a member in a relation.
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role;

    /// <summary>
    /// Represents the geometry information of an OSM (OpenStreetMap) object.
    /// </summary>
    [JsonPropertyName("geometry")]
    public List<GeometryOsm>? Geometry;
}