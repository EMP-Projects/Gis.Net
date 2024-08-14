using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

public class RequestBase : IRequestBase
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
}