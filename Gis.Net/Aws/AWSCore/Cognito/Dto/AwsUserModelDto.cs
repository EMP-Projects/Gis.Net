using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents a data transfer object for AWS user model.
/// </summary>
public class AwsUserModelDto
{
    /// <summary>
    /// Represents the username of a user.
    /// </summary>
    [JsonPropertyName("username")]
    [Required]
    [Display(Name = "UserName")]
    public string? UserName { get; set; }
}

