using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents the data transfer object for changing a password in AWS Cognito.
/// Inherits from AwsAuthModelDto class.
/// </summary>
public class AwsChangePasswordModelDto : AwsAuthModelDto
{
    /// <summary>
    /// Represents the new password for changing a password in AWS Cognito.
    /// </summary>
    /// <value>
    /// The new password value.
    /// </value>
    [JsonPropertyName("newPassword")]
    [Required]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
}