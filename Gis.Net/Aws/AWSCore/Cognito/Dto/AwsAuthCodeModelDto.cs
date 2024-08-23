using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents the AWS authentication code model.
/// </summary>
public class AwsAuthCodeModelDto : AwsAuthModelDto
{
    /// <summary>
    /// Represents the AWS authentication code model.
    /// </summary>
    [JsonPropertyName("code")]
    [Display(Name = "Code")]
    public string? Code { get; set; }
}