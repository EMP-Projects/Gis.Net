using Microsoft.AspNetCore.Authorization;

namespace Gis.Net.Aws.AWSCore.Cognito.Policies;

/// <summary>
/// class to config Policy
/// </summary>
public class AuthorizationPolicy : IAuthorizationRequirement 
{
    /// <summary>
    /// Represents a policy attribute.
    /// </summary>
    public AttributePolicy Attribute { get; set; }

    /// <summary>
    /// Represents the User Pool ID for the Cognito authorization policy.
    /// </summary>
    public string UserPoolId { get; set; }

    /// <summary>
    /// Represents a class for configuring a policy.
    /// </summary>
    public AuthorizationPolicy(string userPoolId, AttributePolicy attribute)
    {
        UserPoolId = userPoolId;
        Attribute = attribute;
    }
}