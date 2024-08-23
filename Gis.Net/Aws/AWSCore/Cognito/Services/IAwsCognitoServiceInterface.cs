using Amazon.CognitoIdentityProvider.Model;
using Gis.Net.Aws.AWSCore.Cognito.Dto;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

public interface IAwsCognitoServiceInterface
{

    Task<AwsCognitoResponseDto<AuthenticationResultType>> LogIn(AwsLogInModelDto user);
    
    Task<AwsCognitoResponseDto<string>> LogOut();
    
    Task<AwsCognitoResponseDto<string>> ChangePassword(AwsChangePasswordModelDto user);

    Task<AwsCognitoResponseDto<string>> ForgotPassword(AwsUserModelDto user);
    
    Task<AwsCognitoResponseDto<string>> ResetPassword(AwsAuthCodeModelDto user);

    Task<AwsCognitoResponseDto<List<UserPoolDescriptionType>>> ListUserPoolsAsync();

    Task<AwsCognitoResponseDto<List<AwsCognitoResponseUsersDto>>> ListUsersAsync();
    
    Task<AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>> ListGroupsAsync();
    
    Task<AwsCognitoResponseDto<string>> ConfirmDeviceAsync(string accessToken, string deviceKey, string deviceName);
    
    Task<AwsCognitoResponseDto<string>> AddUserGroup(AwsUserGroupModelDto user);
    
    Task<AwsCognitoResponseDto<string>> DeleteUserGroup(AwsUserGroupModelDto user);
    
    Task<AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>?> ListUserGroup(AwsUserModelDto user);
    
    Task<AwsCognitoResponseDto<AuthenticationResultType>> ConfirmSignupAsync(AwsAuthCodeModelDto user);

    Task<AwsCognitoResponseDto<string>> SignUpAsync(AwsSignUpModelDto user);

    Task<AwsCognitoResponseDto<string>> ResendConfirmationCodeAsync(AwsAuthCodeModelDto user);
    
    Task<AwsCognitoResponseDto<List<AttributeType>>> GetUserInfo(AwsUserModelDto user);

    Task<AwsCognitoResponseDto<List<AttributeType>>> GetUserInfoByToken(string accessToken);
}