using Microsoft.Extensions.Configuration;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Service for managing AWS roles.
/// </summary>
public class AwsRolesService : IAwsRoles
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AwsRolesService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration settings.</param>
    public AwsRolesService(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Gets the user pool ID from the configuration.
    /// </summary>
    private string UserPoolId => _configuration["AWS_USERPOOLID"]!;

    /// <summary>
    /// Gets the admin role name.
    /// </summary>
    public string RoleAdmin => $"{UserPoolId}_Admin";

    /// <summary>
    /// Gets the users role name.
    /// </summary>
    public string RoleUsers => $"{UserPoolId}_Users";

    /// <summary>
    /// Gets the backoffice role name.
    /// </summary>
    public string RoleBackoffice => $"{UserPoolId}_Backoffice";
}