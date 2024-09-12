namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Interface for managing AWS policies related to Cognito.
/// </summary>
public interface IAwsPolicies
{
    /// <summary>
    /// Gets the list of AWS policies.
    /// </summary>
    List<string> Policies { get; }
    
    /// <summary>
    /// Sets the AWS policies based on the provided Cognito groups.
    /// </summary>
    /// <param name="cognitoGroups">The Cognito groups to set the policies for.</param>
    void SetAwsPolicies(IEnumerable<string> cognitoGroups);
    
    /// <summary>
    /// Gets the AWS policies for a specific Cognito group.
    /// </summary>
    /// <param name="cognitoGroup">The Cognito group to get the policies for.</param>
    /// <returns>An enumerable collection of AWS policies.</returns>
    IEnumerable<string> GetAwsPolicies(string cognitoGroup);
}