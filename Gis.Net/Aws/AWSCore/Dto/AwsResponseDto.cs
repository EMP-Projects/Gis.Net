using System.Collections;
using System.Text.Json.Serialization;
using AutoMapper.Internal;

namespace Gis.Net.Aws.AWSCore.Dto;

/// <summary>
/// Represents a generic response DTO for the AWS service.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
public class AwsResponseDto<T> where T : class
{
    /// <summary>
    /// Gets the count of the response data.
    /// </summary>
    /// <remarks>
    /// The Count property returns the number of elements in the response data. If the response data is not a collection, the Count property will always return 0.
    /// </remarks>
    /// <value>
    /// The number of elements in the response data.
    /// </value>
    [JsonPropertyName("count")]
    public long Count { 
        get {
            if (!typeof(T).IsCollection()) return 0;
            var c = Response as ICollection;
            return c?.Count ?? 0;
        }
    }

    /// <summary>
    /// Gets the count of the response data.
    /// </summary>
    /// <remarks>
    /// The Count property returns the number of elements in the response data. If the response data is not a collection, the Count property will always return 0.
    /// </remarks>
    /// <value>
    /// The number of elements in the response data.
    /// </value>
    [JsonPropertyName("response")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Response { get; set; }

    /// <summary>
    /// Represents a generic response DTO for the AWS service.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    protected AwsResponseDto(T result)
    {
        Response = result;
    }
}