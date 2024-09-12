using System.Security.Claims;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Gis.Net.Aws.AWSCore.Cognito.Policies;
using Gis.Net.Aws.AWSCore.Cognito.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Gis.Net.Aws.AWSCore.Cognito;

/// <summary>
/// Service Manager to Authentication AWS Cognito
/// </summary>
public static class AwsCognitoServiceManager
{
    /// <summary>
    /// Get keys to validate from Aws
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    private static async Task<JsonWebKeySet> GetJsonWebKeys(string uri)
    {
        var response = await new HttpClient().GetAsync(uri);
        var responseString = await response.Content.ReadAsStringAsync();
        return new JsonWebKeySet(responseString);
    }

    /// <summary>
    /// Add Cognito to project
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddCognitoServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonCognitoIdentity>();
        builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();
        builder.Services.AddScoped<IAwsCognitoServiceInterface, AwsCognitoService>();
        builder.Services.AddScoped<IAwsPolicies, AwsPoliciesService>();
        builder.Services.AddScoped<IAwsRoles, AwsRolesService>();
        builder.Services.AddScoped<IAwsAuthService, AwsAuthService>();
            
        // register services to manage policies
        builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandler>();
            
        return builder;
    }
        
    /// <summary>
    /// AWS Cognito Identity Server
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    ///
    public static WebApplicationBuilder AddCognitoAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddCognitoIdentity();
            
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var validIssuer =
                $"https://cognito-idp.{builder.Configuration["AWS_REGION"]!}.amazonaws.com/{builder.Configuration["AWS_USERPOOLID"]!}";
            var validAudience = builder.Configuration["AWS_USERPOOLCLIENTID"]!;
            var tokenValidationParameters = $"{validIssuer}/.well-known/jwks.json";
            var metadataUrl = $"{validIssuer}/.well-known/openid-configuration";

            options.IncludeErrorDetails = true;
            options.SaveToken = true;
            options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
            options.Audience = validAudience;
            options.Authority = validIssuer;
            options.MetadataAddress = metadataUrl;

            var jkwTask = GetJsonWebKeys(tokenValidationParameters);
            jkwTask.Wait();
            var keys = (IEnumerable<SecurityKey>)jkwTask.Result.Keys;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) => keys,
                ValidIssuer = validIssuer,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = validAudience,
                ValidateAudience = false,
                RoleClaimType = "cognito:groups",
                LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow
            };
                
            // Add custom claims
            options.Events = new JwtBearerEvents()
            {
                OnTokenValidated = context =>
                {
                    var userPrincipal = context.Principal;
                    var claims = userPrincipal?.Claims;
                    var enumerable = (claims ?? Array.Empty<Claim>()).ToList();
                    var username = userPrincipal!.HasClaim(c => c.Type == "cognito:username")
                        ? enumerable.ToArray().FirstOrDefault(c => c.Type == "cognito:username")?.Value
                        : string.Empty;

                    if (string.IsNullOrEmpty(username)) return Task.CompletedTask;

                    // get Guid user
                    var authService = context.HttpContext.RequestServices.GetRequiredService<IAwsAuthService>();
                    authService.AddAuthUser(Guid.Parse(username));
                    return Task.CompletedTask;
                }
            };
        });
            
        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        });
            
        return builder;
    }
}