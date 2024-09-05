using Amazon.CognitoIdentityProvider.Model;
using Gis.Net.Aws.AWSCore.Cognito.Dto;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// Interface for AWS Cognito service operations.
/// </summary>
public interface IAwsCognitoServiceInterface
{
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="user">The login model containing user credentials.</param>
    /// <returns>A response containing the authentication result.</returns>
    Task<AwsCognitoResponseDto<AuthenticationResultType>> LogIn(AwsLogInModelDto user);
    
    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>A response indicating the result of the logout operation.</returns>
    Task<AwsCognitoResponseDto<string>> LogOut();
    
    /// <summary>
    /// Changes the password for a user.
    /// </summary>
    /// <param name="user">The model containing the user's current and new passwords.</param>
    /// <returns>A response indicating the result of the password change operation.</returns>
    Task<AwsCognitoResponseDto<string>> ChangePassword(AwsChangePasswordModelDto user);

    /// <summary>
    /// Initiates the forgot password process for a user.
    /// </summary>
    /// <param name="user">The model containing the user's information.</param>
    /// <returns>A response indicating the result of the forgot password operation.</returns>
    Task<AwsCognitoResponseDto<string>> ForgotPassword(AwsUserModelDto user);
    
    /// <summary>
    /// Resets the password for a user.
    /// </summary>
    /// <param name="user">The model containing the user's authentication code and new password.</param>
    /// <returns>A response indicating the result of the password reset operation.</returns>
    Task<AwsCognitoResponseDto<string>> ResetPassword(AwsAuthCodeModelDto user);

    /// <summary>
    /// Lists all user pools.
    /// </summary>
    /// <returns>A response containing a list of user pool descriptions.</returns>
    Task<AwsCognitoResponseDto<List<UserPoolDescriptionType>>> ListUserPoolsAsync();

    /// <summary>
    /// Lists all users.
    /// </summary>
    /// <returns>A response containing a list of users.</returns>
    Task<AwsCognitoResponseDto<List<AwsCognitoResponseUsersDto>>> ListUsersAsync();
    
    /// <summary>
    /// Lists all groups.
    /// </summary>
    /// <returns>A response containing a list of groups.</returns>
    Task<AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>> ListGroupsAsync();
    
    /// <summary>
    /// Confirms a device.
    /// </summary>
    /// <param name="accessToken">The access token of the user.</param>
    /// <param name="deviceKey">The key of the device to confirm.</param>
    /// <param name="deviceName">The name of the device to confirm.</param>
    /// <returns>A response indicating the result of the device confirmation operation.</returns>
    Task<AwsCognitoResponseDto<string>> ConfirmDeviceAsync(string accessToken, string deviceKey, string deviceName);
    
    /// <summary>
    /// Adds a user to a group.
    /// </summary>
    /// <param name="user">The model containing the user's information and the group to add the user to.</param>
    /// <returns>A response indicating the result of the add user to group operation.</returns>
    Task<AwsCognitoResponseDto<string>> AddUserGroup(AwsUserGroupModelDto user);
    
    /// <summary>
    /// Deletes a user from a group.
    /// </summary>
    /// <param name="user">The model containing the user's information and the group to delete the user from.</param>
    /// <returns>A response indicating the result of the delete user from group operation.</returns>
    Task<AwsCognitoResponseDto<string>> DeleteUserGroup(AwsUserGroupModelDto user);
    
    /// <summary>
    /// Lists the groups a user belongs to.
    /// </summary>
    /// <param name="user">The model containing the user's information.</param>
    /// <returns>A response containing a list of groups the user belongs to.</returns>
    Task<AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>?> ListUserGroup(AwsUserModelDto user);
    
    /// <summary>
    /// Confirms a user's signup.
    /// </summary>
    /// <param name="user">The model containing the user's authentication code.</param>
    /// <returns>A response containing the authentication result.</returns>
    Task<AwsCognitoResponseDto<AuthenticationResultType>> ConfirmSignupAsync(AwsAuthCodeModelDto user);

    /// <summary>
    /// Signs up a new user.
    /// </summary>
    /// <param name="user">The model containing the user's signup information.</param>
    /// <returns>A response indicating the result of the signup operation.</returns>
    Task<AwsCognitoResponseDto<string>> SignUpAsync(AwsSignUpModelDto user);

    /// <summary>
    /// Resends the confirmation code to a user.
    /// </summary>
    /// <param name="user">The model containing the user's information.</param>
    /// <returns>A response indicating the result of the resend confirmation code operation.</returns>
    Task<AwsCognitoResponseDto<string>> ResendConfirmationCodeAsync(AwsAuthCodeModelDto user);
    
    /// <summary>
    /// Retrieves user information.
    /// </summary>
    /// <param name="user">The model containing the user's information.</param>
    /// <returns>A response containing a list of user attributes.</returns>
    Task<AwsCognitoResponseDto<List<AttributeType>>> GetUserInfo(AwsUserModelDto user);

    /// <summary>
    /// Retrieves user information by access token.
    /// </summary>
    /// <param name="accessToken">The access token of the user.</param>
    /// <returns>A response containing a list of user attributes.</returns>
    Task<AwsCognitoResponseDto<List<AttributeType>>> GetUserInfoByToken(string accessToken);
}