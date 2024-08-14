using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

public class ResultBase
{
    [JsonPropertyName("error"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Error { get; set; }

    [JsonPropertyName("hasSuccess")]
    public bool HasSuccess => Error is null;
}


