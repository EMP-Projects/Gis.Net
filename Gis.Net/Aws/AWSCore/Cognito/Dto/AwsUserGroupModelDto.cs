using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents a DTO class for AWS User Group information.
/// </summary>
public class AwsUserGroupModelDto : AwsUserModelDto
{
    /// <summary>
    /// Represents a user group.
    /// </summary>
    [JsonPropertyName("group")]
    [Required]
    [Display(Name = "Group")]
    public string? Group { get; set; }
}