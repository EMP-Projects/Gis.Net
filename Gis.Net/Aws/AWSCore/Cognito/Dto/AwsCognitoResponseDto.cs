using System.Text.Json.Serialization;
using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents a response DTO for AWS Cognito service.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
public class AwsCognitoResponseDto<T> : AwsResponseDto<T> where T : class
{
    /// <summary>
    /// Represents the status of an operation in the AWS Cognito service.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AwsCognitoResponseStatusDto? Status { get; init; }

    /// <summary>
    /// Represents a generic Cognito response data transfer object.
    /// </summary>
    public AwsCognitoResponseDto(T result) : base(result)
    {
    }
}