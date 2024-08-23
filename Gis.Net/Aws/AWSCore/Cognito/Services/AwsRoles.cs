namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// 
/// </summary>
public static class AwsRoles
{
    /// <summary>
    /// 
    /// </summary>
    public const string Admin = "Admin";
    
    /// <summary>
    /// 
    /// </summary>
    public const string Users = "Users";
    
    /// <summary>
    /// 
    /// </summary>
    public const string Backoffice = "Backoffice";

    /// <summary>
    /// 
    /// </summary>
    public const string Level0 = Admin;
    
    /// <summary>
    /// 
    /// </summary>
    public const string Level1 = $"{Admin},{Backoffice}";
    
    /// <summary>
    /// 
    /// </summary>
    public const string Level2 = $"{Admin},{Backoffice},{Users}";
}