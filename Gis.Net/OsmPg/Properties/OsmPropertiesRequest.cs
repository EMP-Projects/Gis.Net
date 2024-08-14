using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.OsmPg.Properties;

public class OsmPropertiesRequest : RequestBase, IOsmProperties
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    
    [JsonPropertyName("tags")]
    public required string[] Tags { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}