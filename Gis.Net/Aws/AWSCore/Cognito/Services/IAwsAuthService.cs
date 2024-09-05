namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Interface for AWS authentication services.
/// </summary>
public interface IAwsAuthService
{
    /// <summary>
    /// Gets or sets the AWS authenticated user.
    /// </summary>
    AwsAuthUser? AwsAuthUser { get; set; }
    
    /// <summary>
    /// Adds an authenticated user by their GUID.
    /// </summary>
    /// <param name="guid">The GUID of the user to add.</param>
    void AddAuthUser(Guid guid);
}