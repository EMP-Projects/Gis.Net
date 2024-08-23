using System.Net;
using System.Security.Cryptography;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Gis.Net.Aws.AWSCore.Cognito.Dto;
using Gis.Net.Aws.AWSCore.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// 
/// </summary>
public class AwsCognitoService : IAwsCognitoServiceInterface
{
    private readonly SignInManager<CognitoUser> _signInManager;
    private readonly UserManager<CognitoUser> _userManager;
    private readonly IAmazonCognitoIdentityProvider _awsIdentityProvider;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="awsIdentityProvider"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public AwsCognitoService(UserManager<CognitoUser> userManager,
        SignInManager<CognitoUser> signInManager,
        IAmazonCognitoIdentityProvider awsIdentityProvider,
        IConfiguration configuration,
        ILogger<AwsCognitoService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _awsIdentityProvider = awsIdentityProvider;
        _configuration = configuration;
        _logger = logger;
    }

    private string UserPoolId => _configuration["AWS_USERPOOLID"]!;
    private string ClientId => _configuration["AWS_USERPOOLCLIENTID"]!;

    private string? ClientSecret =>
        !string.IsNullOrEmpty(_configuration["AWS_USERPOOLCLIENTSECRET"]) ? 
            _configuration["AWS_USERPOOLCLIENTSECRET"] : 
            "";   

    /// <summary>
    /// Create token
    /// </summary>
    /// <param name="message"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    private static string CreateToken(string message, string? secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        var keyByte = encoding.GetBytes(secret);
        var messageBytes = encoding.GetBytes(message);
        using var hash256 = new HMACSHA256(keyByte);
        var hashMessage = hash256.ComputeHash(messageBytes);
        return Convert.ToBase64String(hashMessage);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private Dictionary<string, string> GetAuthParameters(string userName, string password)
    {
        try
        {
            Dictionary<string, string> authParameters = new()
            {
                { "USERNAME", userName },
                { "PASSWORD", password }
            };
                
            if (!string.IsNullOrEmpty(ClientSecret))
                authParameters.Add("SECRET_HASH", CreateToken(userName + ClientId, ClientSecret));

            return authParameters;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// authentication with Amazon Cognito and administrator credentials
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task<AdminInitiateAuthResponse> AdminInitiateAuthAsync(string userName, string password)
    {
        try
        {
            var authParameters = GetAuthParameters(userName, password);

            var request = new AdminInitiateAuthRequest
            {
                ClientId = ClientId,
                UserPoolId = UserPoolId,
                AuthParameters = authParameters,
                AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH,
            };

            var response = await _awsIdentityProvider.AdminInitiateAuthAsync(request);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
            
    }

    /// <summary>
    /// Get User information
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<AwsCognitoResponseDto<List<AttributeType>>> GetUserInfoByToken(string accessToken)
    {
        try
        {
            var request = new GetUserRequest
            {
                AccessToken = accessToken
            };

            var response = await _awsIdentityProvider.GetUserAsync(request);
            _logger.LogInformation("User info {UserStatus}", response.Username);
            return new AwsCognitoResponseDto<List<AttributeType>>(response.UserAttributes);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Get the specified user from an Amazon Cognito user pool with administrator access.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Async task.</returns>
    public async Task<AwsCognitoResponseDto<List<AttributeType>>> GetUserInfo(AwsUserModelDto user)
    {
        try
        {
                
            var userRequest = new AdminGetUserRequest
            {
                Username = user.UserName,
                UserPoolId = UserPoolId,
            };

            var response = await _awsIdentityProvider.AdminGetUserAsync(userRequest);
            _logger.LogInformation("User status {UserStatus}", response.UserStatus);
                
            return new AwsCognitoResponseDto<List<AttributeType>>(response.UserAttributes)
            {
                Status = new AwsCognitoResponseStatusDto($"User status {response.UserStatus}")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Create an AmazonCognitoIdentityProviderClient using AnonymousAWSCredentials, which do not require signed requests.
    /// You do not need to supply a region, the underlying code calls FallbackRegionFactory.GetRegionEndpoint() if a region
    /// is not provided. Create CognitoUserPool and CognitoUser objects. Call the StartWithSrpAuthAsync method with an
    /// InitiateSrpAuthRequest that contains the user password.
    ///
    /// Info: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/cognito-authentication-extension.html
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<AuthenticationResultType>> LogIn(AwsLogInModelDto user)
    {
        try
        {
            var provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials());
            var userPool = new CognitoUserPool(UserPoolId, ClientId, provider);
                
            // get cognitoUser
            var cognitoUser = new CognitoUser(user.UserName, ClientId, userPool, provider, ClientSecret);
                
            InitiateSrpAuthRequest authRequest = new()
            {
                Password = user.Password,
            };
                
            var authResponse = await cognitoUser.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            return new AwsCognitoResponseDto<AuthenticationResultType>(authResponse.AuthenticationResult)
            {
                Status = new AwsCognitoResponseStatusDto($"User logged in {cognitoUser.Username}")
            };
        }
        catch (UserNotConfirmedException ex)
        {
            var msg = $"{ex.Message} - User not confirmed";
            // Occurs if the User has signed up 
            // but has not confirmed his EmailAddress
            // In this block we try sending 
            // the Confirmation Code again and ask user to confirm
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
        }
        catch (UserNotFoundException ex)
        {
            var msg = $"{ex.Message} - User not found";
            // Occurs if the provided emailAddress 
            // doesn't exist in the UserPool
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
        }
        catch (NotAuthorizedException ex)
        {
            var msg = $"{ex.Message} - User not authorized";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
        }
    }
        
    /// <summary>
    /// LogOut
    /// </summary>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<string>> LogOut()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return new AwsCognitoResponseDto<string>(string.Empty)
            {
                Status = new AwsCognitoResponseStatusDto("User logged out.")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<AwsCognitoResponseDto<string>> ChangePassword(AwsChangePasswordModelDto user)
    {
        try
        {
            var cognitoUser = await _userManager.FindByEmailAsync(user.UserName!) ??
                              throw new InvalidOperationException($"Unable to retrieve user.");

            var result = await _userManager.ChangePasswordAsync(cognitoUser, user.Password!, user.NewPassword!);

            var msg = $"Changed password for user with ID {cognitoUser.UserID}";

            if (result.Succeeded)
                return new AwsCognitoResponseDto<string>(string.Empty)
                {
                    Status = new AwsCognitoResponseStatusDto(msg)
                };

            msg = "Failed to change password.";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }
        
    /// <summary>
    /// Forgot Password
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<AwsCognitoResponseDto<string>> ForgotPassword(AwsUserModelDto user)
    {
        try
        {
            var request = new ForgotPasswordRequest
            {
                ClientId = ClientId,
                Username = user.UserName,
                SecretHash = CreateToken(user.UserName + ClientId, ClientSecret)
            };

            var response = await _awsIdentityProvider.ForgotPasswordAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
                return new AwsCognitoResponseDto<string>(string.Empty)
                {
                    Status = new AwsCognitoResponseStatusDto(
                        $"Forgot password for user {user.UserName}")
                };
                
            const string msg = "The password could not be changed.";
            _logger.LogError(msg);
            throw new AwsExceptions(msg);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }
        
    /// <summary>
    /// Reset Password
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<AwsCognitoResponseDto<string>> ResetPassword(AwsAuthCodeModelDto user)
    {
        try
        {
            var cognitoUser = await _userManager.FindByEmailAsync(user.UserName!) ??
                              throw new AwsExceptions($"Unable to retrieve user.");

            var result = await _userManager.ResetPasswordAsync(cognitoUser, user.Code!, user.Password!);

            if (result.Succeeded)
                return new AwsCognitoResponseDto<string>(string.Empty)
                {
                    Status = new AwsCognitoResponseStatusDto(
                        $"Password reset for user with ID '{cognitoUser.UserID}'")
                };

            var msg = $"Unable to rest password for user with ID {cognitoUser.UserID}.";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
                
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// List the Amazon Cognito user pools for an account.
    /// A list of UserPoolDescriptionType objects
    /// </summary>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<List<UserPoolDescriptionType>>> ListUserPoolsAsync()
    {
        try
        {
            List<UserPoolDescriptionType> userPools = new();
            var userPoolsPaginator = _awsIdentityProvider.Paginators.ListUserPools(new ListUserPoolsRequest());

            await foreach (var response in userPoolsPaginator.Responses)
            {
                userPools.AddRange(response.UserPools);
            }
                
            return new AwsCognitoResponseDto<List<UserPoolDescriptionType>>(userPools)
            {
                Status = new AwsCognitoResponseStatusDto("successfully")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Get a list of users for the Amazon Cognito user pool.
    /// </summary>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<List<AwsCognitoResponseUsersDto>>> ListUsersAsync()
    {
        try
        {
            ListUsersRequest request = new()
            {
                UserPoolId = UserPoolId
            };

            List<UserType> users = [];

            var usersPaginator = _awsIdentityProvider.Paginators.ListUsers(request);
            await foreach (var response in usersPaginator.Responses)
            {
                users.AddRange(response.Users);
            }

            var listUsers = users.Select(async u =>
            {
                AwsCognitoResponseUsersDto lu = new()
                {
                    Enabled = u.Enabled,
                    UserStatus = u.UserStatus,
                    Id = u.Username,
                    Created = u.UserCreateDate,
                    Attributes = u.Attributes.Select(a => new AwsCognitoResponseUsersAttribute(a.Name, a.Value))
                        .ToList(),
                    Groups = await GetGroupsFromUser(u.Username!)
                };

                return lu;
            }).Select(u => u.Result).ToList();

            return new AwsCognitoResponseDto<List<AwsCognitoResponseUsersDto>>(listUsers)
            {
                Status = new AwsCognitoResponseStatusDto("successfully")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }
        
    /// <summary>
    /// Get list groups
    /// </summary>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>> ListGroupsAsync()
    {
        try
        {
            ListGroupsRequest request = new()
            {
                UserPoolId = UserPoolId
            };

            List<GroupType> groups = new();

            var groupsPaginator = _awsIdentityProvider.Paginators.ListGroups(request);
            await foreach (var response in groupsPaginator.Responses)
            {
                groups.AddRange(response.Groups);
            }

            var listGroups = groups.Select(g => new AwsCognitoResponseGroupDto()
            {
                Created = g.CreationDate,
                Name = g.GroupName,
                Description = g.Description,
                UserPoolId = UserPoolId
            }).ToList();

            return new AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>(listGroups)
            {
                Status = new AwsCognitoResponseStatusDto("successfully")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Initiates and confirms tracking of the device.
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deviceKey"></param>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<string>> ConfirmDeviceAsync(string accessToken, string deviceKey, string deviceName)
    {
        try
        {
            ConfirmDeviceRequest request = new()
            {
                AccessToken = accessToken,
                DeviceKey = deviceKey,
                DeviceName = deviceName
            };

            var response = await _awsIdentityProvider.ConfirmDeviceAsync(request);

            if (response.UserConfirmationNecessary)
                return new AwsCognitoResponseDto<string>(string.Empty)
                {
                    Status = new AwsCognitoResponseStatusDto($"{deviceName} was confirmed")
                };

            var msg = $"{deviceName} was not confirmed";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);   
        }
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<string>> AddUserGroup(AwsUserGroupModelDto user)
    {
        try
        {
            AdminAddUserToGroupRequest request = new()
            {
                GroupName = $"{UserPoolId}_{user.Group}",
                UserPoolId = UserPoolId,
                Username = user.UserName,
            };

            var response = await _awsIdentityProvider.AdminAddUserToGroupAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
                return new AwsCognitoResponseDto<string>(string.Empty)
                {
                    Status = new AwsCognitoResponseStatusDto($"Added group to {user.Group} successfully.")
                };
                
            var msg = $"{user.UserName} Error to added group to {user.Group}";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg); 
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message); 
        }
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<string>> DeleteUserGroup(AwsUserGroupModelDto user)
    {
        try
        {
            AdminRemoveUserFromGroupRequest request = new()
            {
                GroupName = $"{UserPoolId}_{user.Group}",
                UserPoolId = UserPoolId,
                Username = user.UserName,
            };

            var response = await _awsIdentityProvider.AdminRemoveUserFromGroupAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.OK)
                return new AwsCognitoResponseDto<string>(string.Empty)
                {
                    Status = new AwsCognitoResponseStatusDto($"Deleted {user.UserName} from group {user.Group} successfully.")
                };

            var msg = $"{user.UserName} Error to delete {user.UserName} from group {user.Group}";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message); 
        }
    }

    /// <summary>
    /// Get list user's group
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<List<AwsCognitoResponseGroupDto>?> GetGroupsFromUser(string username)
    {
        try
        {
            AdminListGroupsForUserRequest request = new()
            {
                UserPoolId = UserPoolId,
                Username = username,
            };

            var response = await _awsIdentityProvider.AdminListGroupsForUserAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK) return null;
                
            return response.Groups.Select(g =>
            {
                AwsCognitoResponseGroupDto gr = new()
                {
                    Created = g.CreationDate,
                    Description = g.Description,
                    Name = g.GroupName,
                    UserPoolId = UserPoolId
                };
                return gr;
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message); 
        }
    }   
        
    /// <summary>
    /// Get user list
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>?> ListUserGroup(AwsUserModelDto user)
    {
        try
        {
            if (user.UserName == null) return null;
            var listGroups = await GetGroupsFromUser(user.UserName);

            if (listGroups is not null)
                return new AwsCognitoResponseDto<List<AwsCognitoResponseGroupDto>>(listGroups);

            var msg = $"{user.UserName} to read groups";
            _logger.LogError("Error: {Message}", msg);
            throw new AwsExceptions(msg); 
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message); 
        }
    }
        
    /// <summary>
    /// Confirm that the user has signed up.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<AuthenticationResultType>> ConfirmSignupAsync(AwsAuthCodeModelDto user)
    {
        try
        {
            ConfirmSignUpRequest signUpRequest = new()
            {
                ClientId = ClientId,
                ConfirmationCode = user.Code,
                Username = user.UserName,
                SecretHash = CreateToken(user.UserName + ClientId, ClientSecret)
            };
                
            var response = await this._awsIdentityProvider.ConfirmSignUpAsync(signUpRequest);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                var msg = $"{user.UserName} was not confirmed";
                _logger.LogError("Error: {Message}", msg);
                throw new AwsExceptions(msg); 
            }

            var status = new AwsCognitoResponseStatusDto($"{user.UserName} was confirmed");

            if (user.Password is null) 
                return new AwsCognitoResponseDto<AuthenticationResultType>(new AuthenticationResultType())
                {
                    Status = status
                };
                
            var authResponse = await AdminInitiateAuthAsync(user.UserName!, user.Password!);
            return new AwsCognitoResponseDto<AuthenticationResultType>(authResponse.AuthenticationResult)
            {
                Status = status
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);  
        }
    }

    /// <summary>
    /// Send a new confirmation code to a user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<string>> ResendConfirmationCodeAsync(AwsAuthCodeModelDto user)
    {
        try
        {
            ResendConfirmationCodeRequest codeRequest = new()
            {
                ClientId = ClientId,
                Username = user.UserName,
                SecretHash = CreateToken(user.UserName + ClientId, ClientSecret)
            };

            var response = await _awsIdentityProvider.ResendConfirmationCodeAsync(codeRequest);
            _logger.LogInformation("Method of delivery is {DeliveryMedium}", response.CodeDeliveryDetails.DeliveryMedium);

            return new AwsCognitoResponseDto<string>(string.Empty)
            {
                Status = new AwsCognitoResponseStatusDto($"Send new code")
                {
                    Details = response.CodeDeliveryDetails
                }
            };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
            throw new AwsExceptions(ex.Message);  
        }
    }

    /// <summary>
    /// Sign up a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<AwsCognitoResponseDto<string>> SignUpAsync(AwsSignUpModelDto user)
    {
        try
        {
            AttributeType userAttrs = new()
            {
                Name = "email",
                Value = user.Email,
            };

            List<AttributeType> userAttrsList = [userAttrs];

            var signUpRequest = new SignUpRequest
            {
                UserAttributes = userAttrsList,
                Username = user.UserName,
                ClientId = ClientId,
                Password = user.Password,
                SecretHash = CreateToken(user.UserName + ClientId, ClientSecret)
            };
            var response = await _awsIdentityProvider.SignUpAsync(signUpRequest);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                var msgError = $"User {user.UserName} was not logged.";
                _logger.LogError("Error: {Message}", msgError);
                throw new AwsExceptions(msgError); 
            }

            return new AwsCognitoResponseDto<string>(string.Empty)
            {
                Status = new AwsCognitoResponseStatusDto($"User {user.UserName} was logged successfully")
            };
        }
        catch (UsernameExistsException ex)
        {
            var msgError = "User Already Exists: " + ex.Message;
            _logger.LogError("Error: {Message}", msgError);
            throw new AwsExceptions(msgError);  
        }
    }
}