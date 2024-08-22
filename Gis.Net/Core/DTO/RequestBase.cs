using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents the base class for request objects.
/// </summary>
public class RequestBase : IRequestBase
{
    /// <summary>
    /// Gets or sets the Id for the request object.
    /// </summary>
    /// <remarks>
    /// This property represents the unique identifier for the request object.
    /// </remarks>
    [JsonPropertyName("id")]
    public int Id { get; set; }
}