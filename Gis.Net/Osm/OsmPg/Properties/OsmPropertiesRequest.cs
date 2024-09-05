using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <inheritdoc cref="Gis.Net.Osm.OsmPg.IOsmProperties" />
public class OsmPropertiesRequest : RequestBase, IOsmProperties
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    public required string? Type { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("tags")]
    public required string[]? Tags { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}