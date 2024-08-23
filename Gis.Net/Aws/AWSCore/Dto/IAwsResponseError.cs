using Microsoft.AspNetCore.Identity;

namespace Gis.Net.Aws.AWSCore.Dto;

/// <summary>
/// Represents a response error from AWS service.
/// </summary>
public interface IAwsResponseError
{
    /// <summary>
    /// Represents an AWS response error.
    /// </summary>
    bool HasError { get; set; }

    /// <summary>
    /// Represents a response error message from AWS service.
    /// </summary>
    string? Message { get; set; }

    /// <summary>
    /// Represents additional details about an AWS response error.
    /// </summary>
    /// <value>
    /// The collection of identity errors associated with the response error.
    /// </value>
    IEnumerable<IdentityError>? Details { get; set; }
}