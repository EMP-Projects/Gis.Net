namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Provides authentication services for AWS.
/// </summary>
public class AwsAuthService : IAwsAuthService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AwsAuthService"/> class.
    /// </summary>
    public AwsAuthService()
    {
    }

    /// <summary>
    /// Adds an authenticated user with the specified GUID.
    /// </summary>
    /// <param name="guid">The unique identifier for the user.</param>
    public void AddAuthUser(Guid guid)
    {
        var awsAuthUser = new AwsAuthUser(guid);
        AwsAuthUser = awsAuthUser;
    }

    /// <summary>
    /// Gets or sets the authenticated AWS user.
    /// </summary>
    public AwsAuthUser? AwsAuthUser { get; set; }
}