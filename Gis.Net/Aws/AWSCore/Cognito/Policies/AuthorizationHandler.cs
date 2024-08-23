using Microsoft.AspNetCore.Authorization;

namespace Gis.Net.Aws.AWSCore.Cognito.Policies;

/// <summary>
/// Check Policy by User Attributes
/// </summary>
public class AuthorizationHandler : AuthorizationHandler<AuthorizationPolicy>
{
    /// <summary>
    /// Handles the requirement of an authorization policy by checking user attributes.
    /// </summary>
    /// <param name="context">The authorization context.</param>
    /// <param name="requirement">The requirement of the authorization policy.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationPolicy requirement)
    {
        var attributes = context.User.FindAll(c => c.Type == requirement.Attribute.Name).ToList();
        
        if (attributes.Count != 0)
        {
            var hasAll = attributes.All(group => requirement.Attribute.Value.Equals(group.Value));

            if (!hasAll)
                context.Fail();
            else
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}