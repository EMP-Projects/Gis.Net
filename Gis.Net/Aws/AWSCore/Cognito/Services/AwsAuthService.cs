namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// 
/// </summary>
public class AwsAuthService : IAwsAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public AwsAuthService()
    {
        
    }
    
    public void AddAuthUser(Guid guid)
    {
        var awsAuthUser = new AwsAuthUser(guid);
        AwsAuthUser = awsAuthUser;
    }
    
    public AwsAuthUser? AwsAuthUser { get; set; }
}