using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Gis.Net.Aws.AWSCore.Dto;

/// <summary>
/// Represents a response error from AWS service.
/// </summary>
public class AwsResponseErrorDto : IAwsResponseError
{
    /// <summary>
    /// Gets or sets a value indicating whether there is an error in the response from the AWS service.
    /// </summary>
    [JsonPropertyName("hasError")] public bool HasError { get; set; } = true;

    /// <summary>
    /// Gets or sets the message associated with the response error from the AWS service.
    /// </summary>
    /// <remarks>
    /// This property represents the detailed error message from the AWS service in response to a request.
    /// </remarks>
    [JsonPropertyName("message")] public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the details of the response error from the AWS service.
    /// </summary>
    /// <remarks>
    /// The details provide additional information about the response error.
    /// </remarks>
    [JsonPropertyName("details")] public IEnumerable<IdentityError>? Details { get; set; } = null;

    /// <summary>
    /// Represents a response error from AWS service.
    /// </summary>
    protected AwsResponseErrorDto(string message) => Message = message;
}