using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

/// <summary>
/// Base class for Data Transfer Objects (DTOs).
/// </summary>
public abstract class DtoBase : IDtoBase
{
    /// <summary>
    /// Base class for data transfer objects (DTOs).
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Represents the key property of a DTO object.
    /// </summary>
    [JsonPropertyName("entityKey")]
    public required string EntityKey { get; set; }

    /// <summary>
    /// Represents the timestamp of a data object.
    /// </summary>
    [JsonPropertyName("timestamp"), JsonIgnore]
    public DateTime TimeStamp { get; set; }
}