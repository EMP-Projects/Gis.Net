using Gis.Net.Aws.AWSCore.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <inheritdoc />
public class AwsPoliciesService : IAwsPolicies
{
    private readonly IConfiguration _configuration;

    /// <inheritdoc />
    public List<string> Policies { get; } = [];
    
    private string UserPoolId => _configuration["AWS_USERPOOLID"]!;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AwsPoliciesService"/> class with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration to be used by the service.</param>
    public AwsPoliciesService(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Gets the role for the admin users in the AWS User Pool.
    /// </summary>
    public string RoleAdmin => $"{UserPoolId}_Admin";

    /// <summary>
    /// Gets the role for the regular users in the AWS User Pool.
    /// </summary>
    public string RoleUsers => $"{UserPoolId}_Users";
    
    /// <summary>
    /// Gets the role for the backoffice users in the AWS User Pool.
    /// </summary>
    public string RoleBackoffice => $"{UserPoolId}_Backoffice";
    
    /// <summary>
    /// Retrieves the AWS policies associated with a specified Cognito group.
    /// </summary>
    /// <param name="cognitoGroup">The Cognito group for which to retrieve policies.</param>
    /// <returns>A collection of AWS policies associated with the specified Cognito group.</returns>
    /// <exception cref="AwsExceptions">
    /// Thrown when the required environment variables are not set or the specified groups do not exist.
    /// </exception>
    public IEnumerable<string> GetAwsPolicies(string cognitoGroup)
    {
        List<string> result = [];
    
        if (string.IsNullOrEmpty(_configuration["AWS_ADMIN_GROUPS"]))
            throw new AwsExceptions("AWS_ADMIN_GROUPS environment variable is required");
    
        if (string.IsNullOrEmpty(_configuration["AWS_USERS_GROUPS"]))
            throw new AwsExceptions("AWS_USERS_GROUPS environment variable is required");
    
        var admin = _configuration["AWS_ADMIN_GROUPS"]?.Split(":").ToList();
        var users = _configuration["AWS_USERS_GROUPS"]?.Split(":").ToList();
    
        if (admin is null)
            throw new AwsExceptions("Admin Group doesn't exist.");
    
        if (users is null)
            throw new AwsExceptions("User Group doesn't exist.");
    
        if (admin.Exists(c => $"{UserPoolId}_{c}".ToUpper().Equals(cognitoGroup.ToUpper())))
            if (!result.Exists(p => p.Equals(AwsPolicies.Admin))) result.Add(AwsPolicies.Admin);

        if (users.Exists(u => $"{UserPoolId}_{u}".ToUpper().Equals(cognitoGroup.ToUpper()))) return result;
        if (!result.Exists(p => p.Equals(AwsPolicies.Users))) result.Add(AwsPolicies.Users);

        return result;
    }

    /// <summary>
    /// Sets the AWS policies for the specified Cognito groups.
    /// </summary>
    /// <param name="cognitoGroups">The Cognito groups for which to set policies.</param>
    public void SetAwsPolicies(IEnumerable<string> cognitoGroups)
    {
        foreach (var cognitoGroup in cognitoGroups)
        {
            var policy = GetAwsPolicies(cognitoGroup);
            Policies.AddRange(policy);
        }
    }
}