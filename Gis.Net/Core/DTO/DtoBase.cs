using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

public abstract class DtoBase : IDtoBase
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    
    [JsonPropertyName("timestamp")]
    public DateTime TimeStamp { get; set; }
}