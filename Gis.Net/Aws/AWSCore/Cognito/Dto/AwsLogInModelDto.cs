using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents a DTO (Data Transfer Object) for the AWS login model.
/// </summary>
public class AwsLogInModelDto : AwsAuthModelDto
{
    /// <summary>
    /// Gets or sets a value indicating whether the "Remember Me" feature is enabled.
    /// </summary>
    /// <remarks>
    /// This property is used to determine whether the user's authentication session should be remembered
    /// after the user closes the browser or exits the application. If set to true, the user will not
    /// need to log in again when they reopen the browser or restart the application. If set to false,
    /// the user will be required to log in again after closing the browser or exiting the application.
    /// </remarks>
    [JsonPropertyName("rememberMe")]
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
};