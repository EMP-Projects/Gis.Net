using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents the response DTO for AWS Cognito users.
/// </summary>
public class AwsCognitoResponseUsersDto
{
    /// <summary>
    /// Gets or sets a value indicating whether the user is enabled.
    /// </summary>
    /// <value><c>true</c> if the user is enabled; otherwise, <c>false</c>.</value>
    [JsonPropertyName("enabled")] public bool Enabled { get; set; }

    /// <summary>
    /// Represents the status of a user in AWS Cognito.
    /// </summary>
    [JsonPropertyName("userStatus")] public string? UserStatus { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    /// <value>The user identifier.</value>
    [JsonPropertyName("id")] public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time the user was created.
    /// </summary>
    /// <value>The date and time the user was created.</value>
    [JsonPropertyName("created")] public DateTime? Created { get; set; }

    /// <summary>
    /// Gets or sets the list of attributes for a user in the AWS Cognito service.
    /// </summary>
    /// <value>The list of attributes.</value>
    [JsonPropertyName("attributes")] public List<AwsCognitoResponseUsersAttribute>? Attributes { get; set; } = new();

    /// Gets or sets the list of groups associated with the user.
    /// </summary>
    /// <value>The list of groups associated with the user.</value>
    [JsonPropertyName("groups")] public List<AwsCognitoResponseGroupDto>? Groups { get; set; } = new();
}