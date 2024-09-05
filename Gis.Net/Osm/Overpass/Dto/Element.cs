using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents an element in the Overpass response.
/// </summary>
public class Element
{

    /// Represents a generic element in the Overpass API response.
    /// /
    [JsonPropertyName("type")]
    public string? Type;

    /// <summary>
    /// Represents the unique identifier of an element in the Overpass API response.
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id;

    /// <summary>
    /// Represents the bounds of a geographical area.
    /// </summary>
    [JsonPropertyName("bounds")]
    public Bounds? Bounds;

    /// <summary>
    /// Represents a collection of nodes in the Overpass API response.
    /// </summary>
    [JsonPropertyName("nodes")]
    public List<object>? Nodes;

    /// <summary>
    /// Represents the geometry of an element.
    /// </summary>
    [JsonPropertyName("geometry")]
    public List<GeometryOsm>? Geometry;

    /// <summary>
    /// Represents the tags associated with an <see cref="Element"/> in the Overpass API response.
    /// </summary>
    [JsonPropertyName("tags")]
    public object? Tags;

    /// <summary>
    /// Represents a member of an element.
    /// </summary>
    [JsonPropertyName("members")]
    public List<Member>? Members;

    /// <summary>
    /// The latitude coordinate of the element.
    /// </summary>
    [JsonPropertyName("lat")]
    public double Lat;

    /// <summary>
    /// Represents the longitude of a geographic point.
    /// </summary>
    [JsonPropertyName("lon")]
    public double Lon;
}