namespace Gis.Net.Aws.AWSCore.Cognito.Services;

public interface IAwsAuthService
{

    AwsAuthUser? AwsAuthUser { get; set; }
    
    void AddAuthUser(Guid guid);
}