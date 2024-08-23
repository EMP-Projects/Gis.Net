using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents an attribute of a user in the AWS Cognito service.
/// </summary>
public class AwsCognitoResponseUsersAttribute
{
    /// <summary>
    /// Represents an attribute of a user in the AWS Cognito service.
    /// </summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>
    /// Represents the value of an attribute for a user in the AWS Cognito service.
    /// </summary>
    [JsonPropertyName("value")] public object? Value { get; set; }

    /// <summary>
    /// Briefly describe the class, method, or property.
    /// </summary>
    /// <remarks>
    /// Provide additional details or important notes about the class, method, or property.
    /// </remarks>
    /// <example>
    /// Provide an example usage of the class, method, or property.
    /// <code>
    /// // Code example goes here
    /// </code>
    /// </example>
    public AwsCognitoResponseUsersAttribute(string name, object value)
    {
        Name = name;
        Value = value;
    }
}