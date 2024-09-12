namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Provides constants for AWS roles and role levels.
/// </summary>
public static class AwsRoles
{
    /// <summary>
    /// The Admin role.
    /// </summary>
    public const string Admin = "Admin";
    
    /// <summary>
    /// The Users role.
    /// </summary>
    public const string Users = "Users";
    
    /// <summary>
    /// The Backoffice role.
    /// </summary>
    public const string Backoffice = "Backoffice";

    /// <summary>
    /// Level 0 role, which includes only the Admin role.
    /// </summary>
    public const string Level0 = Admin;
    
    /// <summary>
    /// Level 1 role, which includes the Admin and Backoffice roles.
    /// </summary>
    public const string Level1 = $"{Admin},{Backoffice}";
    
    /// <summary>
    /// Level 2 role, which includes the Admin, Backoffice, and Users roles.
    /// </summary>
    public const string Level2 = $"{Admin},{Backoffice},{Users}";
}