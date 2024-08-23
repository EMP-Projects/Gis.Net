using Gis.Net.Aws.AWSCore.Cognito.Dto;
using Gis.Net.Aws.AWSCore.Cognito.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Aws.Controllers;

/// <summary>
/// Base class for AWS Cognito Controllers.
/// </summary>
public abstract class AwsCognitoController : ControllerBase
{
    private readonly IAwsCognitoServiceInterface _awsCognitoAuth;
        
    /// <summary>
    /// Aws Cognito Controllers
    /// </summary>
    protected AwsCognitoController(IAwsCognitoServiceInterface awsCognitoAuth)
    {
        _awsCognitoAuth = awsCognitoAuth;
    }

    /// <summary>
    /// Login Aws Cognito
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("signIn")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> SignIn([FromBody] AwsLogInModelDto user)
    {
        try
        {
            var result = await _awsCognitoAuth.LogIn(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Logout User
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("logOut")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult> LogOut()
    {
        try
        {
            var result = await _awsCognitoAuth.LogOut();
            return Ok(result);
        }
        catch (Exception ex)
        {   
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Send code to registration
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("sendCode")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> SendCode([FromBody] AwsAuthCodeModelDto user)
    {
        try
        {
            var result = await this._awsCognitoAuth.ResendConfirmationCodeAsync(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Reset password
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("resetPassword")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> ResetPassword([FromBody] AwsAuthCodeModelDto user)
    {
        try
        {
            var result = await this._awsCognitoAuth.ResetPassword(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Forgot Password
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("forgotPassword")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> ForgotPassword([FromBody] AwsUserModelDto user)
    {
        try
        {
            var result = await this._awsCognitoAuth.ForgotPassword(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Confirm Code to register user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("confirm")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> Confirm([FromBody] AwsAuthCodeModelDto user)
    {
        try
        {
            var result = await this._awsCognitoAuth.ConfirmSignupAsync(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Register User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("signUp")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> SignUp([FromBody] AwsSignUpModelDto user)
    {
        try
        {
            var result = await _awsCognitoAuth.SignUpAsync(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Add group user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("group/add")]
    [Authorize(Roles = AwsRoles.Level0)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> AddUserGroup([FromBody] AwsUserGroupModelDto user)
    {
        try
        {
            var result = await _awsCognitoAuth.AddUserGroup(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Delete group
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("group/delete")]
    [Authorize(Roles = AwsRoles.Level0)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> DeleteUserGroup([FromBody] AwsUserGroupModelDto user)
    {
        try
        {
            var result = await _awsCognitoAuth.DeleteUserGroup(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// List group users
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("group/list")]
    [Authorize(Roles = AwsRoles.Level2)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult> ListUserGroup([FromQuery] AwsUserModelDto user)
    {
        try
        {
            var result = await _awsCognitoAuth.ListUserGroup(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// get list users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("users")]
    [Authorize(Roles = AwsRoles.Level2)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult> Users()
    {
        try
        {
            return Ok(await _awsCognitoAuth.ListUsersAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }
        
    /// <summary>
    ///  Get User information by username
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("user/info")]
    [Authorize(Roles = AwsRoles.Level2)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult> UserInfo([FromQuery] AwsUserModelDto query)
    {
        try
        {
            var user = await _awsCognitoAuth.GetUserInfo(query);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }
        
    /// <summary>
    /// Get user information by access Token
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("user/token")]
    [Authorize(Roles = AwsRoles.Level2)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult> UserInfoByToken([FromHeader] string accessToken)
    {
        try
        {
            var user = await _awsCognitoAuth.GetUserInfoByToken(accessToken);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// List groups
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("groups")]
    [Authorize(Roles = AwsRoles.Level2)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult> Groups()
    {
        try
        {
            return Ok(await _awsCognitoAuth.ListGroupsAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(new AwsCognitoResponseErrorDto(ex.Message));
        }
    }
}