using Microsoft.Extensions.Configuration;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// 
/// </summary>
public interface IAwsRoles
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleAdmin { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public string RoleUsers { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public string RoleBackoffice { get; }
}

/// <summary>
/// 
/// </summary>
public class AwsRolesService : IAwsRoles
{
    private readonly IConfiguration _configuration;
    
    public AwsRolesService(IConfiguration configuration) => _configuration = configuration;

    private string UserPoolId => _configuration["AWS_USERPOOLID"]!;
    
    public string RoleAdmin => $"{UserPoolId}_Admin";
    
    public string RoleUsers => $"{UserPoolId}_Users";
    
    public string RoleBackoffice => $"{UserPoolId}_Backoffice";
}