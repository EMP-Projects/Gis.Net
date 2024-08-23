using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents a group in AWS Cognito.
/// </summary>
public class AwsCognitoResponseGroupDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("created")] public DateTime? Created { get; set; } = null;

    /// <summary>
    /// Represents the description of a group in AWS Cognito.
    /// </summary>
    [JsonPropertyName("description")] public string? Description { get; set; } = null;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")] public string? Name { get; set; } = null;

    /// <summary>
    /// Represents the user pool ID in AWS Cognito.
    /// </summary>
    [JsonPropertyName("userPoolId")] [JsonIgnore] public string? UserPoolId { get; init; }

    /// <summary>
    /// Represents the list of policies associated with a group in AWS Cognito.
    /// </summary>
    [JsonPropertyName("policies")] public List<string>? Policies { get; init; }
}