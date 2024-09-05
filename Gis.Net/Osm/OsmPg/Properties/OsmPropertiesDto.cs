using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <summary>
/// Represents the properties of an OpenStreetMap entity.
/// </summary>
public class OsmPropertiesDto : DtoBase, IOsmProperties
{
    /// <summary>
    /// Represents the properties of an OpenStreetMap entity.
    /// </summary>
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// Represents the properties of an OpenStreetMap entity.
    /// </summary>
    [JsonPropertyName("tags"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Tags { get; set; }

    /// <summary>
    /// Gets or sets the name of an OpenStreetMap entity.
    /// </summary>
    [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
}