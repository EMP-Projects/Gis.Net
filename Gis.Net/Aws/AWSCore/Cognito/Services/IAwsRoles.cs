namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Interface for managing AWS roles.
/// </summary>
public interface IAwsRoles
{
    /// <summary>
    /// Gets the admin role name.
    /// </summary>
    public string RoleAdmin { get; }
    
    /// <summary>
    /// Gets the users role name.
    /// </summary>
    public string RoleUsers { get; }
    
    /// <summary>
    /// Gets the backoffice role name.
    /// </summary>
    public string RoleBackoffice { get; }
}