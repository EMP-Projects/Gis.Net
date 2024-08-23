using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents the response status of an AWS Cognito operation.
/// </summary>
public class AwsCognitoResponseStatusDto
{
    /// <summary>
    /// Gets or sets the 'HasConfirmed' flag indicating whether a confirmation action has been performed.
    /// </summary>
    /// <value>
    /// true if the confirmation action has been performed; otherwise, false.
    /// </value>
    [JsonPropertyName("hasConfirmed")]
    public bool? HasConfirmed { get; set; }

    /// <summary>
    /// Represents the response of an AWS Cognito operation.
    /// </summary>
    [JsonPropertyName("response")]
    public string? Response { get; set; }

    /// <summary>
    /// Represents additional details related to the response status of an AWS Cognito operation.
    /// </summary>
    [JsonPropertyName("details")]
    public object? Details { get; set; }

    /// <summary>
    /// Represents the response status of an AWS Cognito operation.
    /// </summary>
    public AwsCognitoResponseStatusDto(string response)
    {
        Response = response;
        HasConfirmed = true;
    }
}