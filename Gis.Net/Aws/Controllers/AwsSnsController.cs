using Amazon.SimpleNotificationService.Model;
using Gis.Net.Aws.AWSCore.SNS.Dto;
using Gis.Net.Aws.AWSCore.SNS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.Controllers;

/// <summary>
/// Abstract base class for AWS SNS controller.
/// </summary>
public abstract class AwsSnsController : ControllerBase
{
    private readonly IAwsSnsService _awsSnsService;
    private readonly ILogger<AwsSnsController> _logger;

    /// <summary>
    /// Controller AWS Sns Service
    /// </summary>
    /// <param name="awsSnsService"></param>
    /// <param name="logger"></param>
    protected AwsSnsController(IAwsSnsService awsSnsService, ILogger<AwsSnsController> logger)
    {
        _awsSnsService = awsSnsService;
        _logger = logger;
    }

    /// <summary>
    /// Confirm Subscription Topic
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/confirm")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ConfirmSubscribe([FromBody] AwsConfirmDto payload, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.ConfirmSubscriptionAsync(payload, cancel);
            return Ok(new AwsSnsResponseDto<string>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Subscribe Topic
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/subscribe")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Subscribe([FromBody] AwsSubscribeDto payload, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.SubscribeTopic(payload, cancel);
            return Ok(new AwsSnsResponseDto<string>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// List topic
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("topics")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTopics(CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.ListTopics(cancel);
            return Ok(new AwsSnsResponseDto<List<string>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Check phone number
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("check/phone")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTopics(AwsSubscribeCheckDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.CheckIfOptedOut(request, cancel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Create Topic
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/create")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateTopic([FromBody] AwsTopicRequestDto payload, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.CreateTopic(payload, cancel);
            return Ok(new AwsSnsResponseDto<string>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Unsubscribe Topic
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/unsubscribe")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UnsubscribeTopic([FromBody] AwsUnSubscribeDto payload, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.Unsubscribe(payload, cancel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Delete Topic
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/delete")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> DeleteTopic([FromBody] AwsSnsDto? payload, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.DeleteTopic(payload, cancel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Attributes Topic
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/attributes")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAttributes([FromBody] AwsSnsDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.GetTopicAttributes(request, cancel);
            return Ok(new AwsSnsResponseDto<Dictionary<string, string>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// List subscriptions Topic
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("topic/subscriptions")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> GetSubscription([FromBody] AwsSnsDto request)
    {
        try
        {
            var result = await _awsSnsService.GetSubscriptions(request);
            return Ok(new AwsSnsResponseDto<List<Subscription>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Publish message ti topic
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("topic/publish")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> GetSubscription([FromBody] AwsPublishDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsSnsService.Publish(request, cancel);
            return Ok(new AwsSnsResponseDto<string>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsSnsResponseErrorDto(ex.Message));
        }
    }
}