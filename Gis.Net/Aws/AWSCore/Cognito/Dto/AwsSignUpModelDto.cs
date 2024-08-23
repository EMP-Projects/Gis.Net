using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents the data transfer object for signing up a user in AWS Cognito.
/// </summary>
public class AwsSignUpModelDto : AwsAuthModelDto
{
    /// <summary>
    /// Represents an email property for an AWS sign-up model.
    /// </summary>
    [JsonPropertyName("email")]
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
};