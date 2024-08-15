using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <inheritdoc />
public class OsmPropertiesDto : DtoBase, IOsmProperties
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    
    [JsonPropertyName("tags")]
    public required string[] Tags { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}