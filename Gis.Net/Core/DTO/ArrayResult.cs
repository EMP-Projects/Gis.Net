using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

public class ArrayResult<T>: ResultBase where T : IDtoBase
{
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<T>? Data { get; init; }
    
    [JsonPropertyName("count")]
    public int Count => Data?.Count() ?? 0;

    public ArrayResult(string error) => Error = error;
    public ArrayResult(IEnumerable<T> data) => Data = data;
}