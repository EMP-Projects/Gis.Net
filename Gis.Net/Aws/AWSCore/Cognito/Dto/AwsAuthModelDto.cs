using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents the AWS authentication model.
/// </summary>
public class AwsAuthModelDto : AwsUserModelDto
{
    /// <summary>
    /// Represents a password for user authentication.
    /// </summary>
    [JsonPropertyName("password")]
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}