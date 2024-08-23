using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.S3.Dto;
using Gis.Net.Aws.AWSCore.S3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.Controllers;

/// <summary>
/// Base controller for interacting with AWS S3.
/// </summary>
public abstract class AwsS3Controller : ControllerBase
{
    private readonly IAwsBucketService _bucketService;
    private readonly ILogger<AwsS3Controller> _logger;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Base abstract class for AWS S3 controllers.
    /// </summary>
    /// <remarks>
    /// This class provides common functionality and actions for working with AWS S3 buckets and files.
    /// </remarks>
    protected AwsS3Controller(IAwsBucketService bucketService,
        ILogger<AwsS3Controller> logger,
        IConfiguration configuration)
    {
        _bucketService = bucketService;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Create Bucket S3
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("bucket/create")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<IActionResult> CreateBucket([FromBody] AwsS3BucketRootDto request, CancellationToken cancel)
    {
        try
        {
            var response = await _bucketService.CreateBucket(request, cancel);
            if (response)
                return Ok(new AwsS3ResponseDto<string>($"Bucket {request.BucketName} created successfully."));
            return BadRequest(new AwsS3ResponseErrorDto($"Bucket {request.BucketName} created failed."));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
        
    }
    
    /// <summary>
    /// List Buckets
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("bucket/list")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> GetAll(CancellationToken cancel)
    {
        try
        {
            var result = (await _bucketService.GetAllBuckets(cancel)).ToList();
            return Ok(new AwsS3ResponseDto<List<string>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
        
    }
    
    /// <summary>
    /// Delete Bucket
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("bucket/delete")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> DeleteBucket([FromQuery] AwsS3BucketRootDto request, CancellationToken cancel)
    {
        try
        {
            await _bucketService.DeleteBucket(request, cancel);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("files/exist")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> CheckIfExists([FromQuery] AwsS3BucketFileDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bucketService.IsExistFile(request, cancellationToken);
            return Ok(new AwsS3ResponseDto<string>(result.ToString()));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// List files
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("files/list")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> GetAllFiles([FromQuery] AwsS3BucketFileDto query, CancellationToken cancel)
    {
        try
        {
            query.BucketName ??= _configuration["AWS_BUCKET"]!;

            if (query.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            
            var result = (await _bucketService.GetAllFiles(query, cancel) ?? Array.Empty<AwsS3ObjectDto?>()).ToList();
            return Ok(new AwsS3ResponseDto<List<AwsS3ObjectDto?>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Delete files
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("files/delete")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> DeleteFile([FromQuery] AwsS3BucketDto query, CancellationToken cancel)
    {
        try
        {
            query.BucketName ??= _configuration["AWS_BUCKET"]!;

            if (query.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            
            await _bucketService.DeleteFile(query, cancel);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("files/move")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> MoveFile(AwsS3MoveFileDto request, CancellationToken cancellationToken)
    {
        try
        {
            await _bucketService.MoveFile(request, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Upload file to Bucket
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost]
    [DisableRequestSizeLimit]
    [Route("files/upload")]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> UploadFile([FromForm] AwsS3BucketUploadDto query, CancellationToken cancel)
    {
        try
        {
            query.BucketName ??= _configuration["AWS_BUCKET"]!;

            if (query.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            
            if (query.File is null)
                throw new AwsExceptions("You need the file to upload in the Bucket");
            
            var result = await _bucketService.Upload(query, cancel);
            return Ok(new AwsS3ResponseDto<AwsS3ObjectDto>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Get url to share file
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("files/share")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> SharingFile([FromForm] AwsS3BucketUploadDto query, CancellationToken cancel)
    {
        try
        {
            query.BucketName ??= _configuration["AWS_BUCKET"]!;

            if (query.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            
            if (query.File is null)
                throw new AwsExceptions("You need the file to upload in the Bucket");
            
            var result = await _bucketService.GetSharingUrl(query, cancel);
            return Ok(new AwsS3ResponseDto<string>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }

    /// <summary>
    /// Download file from bucket
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("files/download")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<IActionResult> DownloadFile([FromBody] AwsS3BucketDto request, CancellationToken cancel)
    {
        try
        {
            request.BucketName ??= _configuration["AWS_BUCKET"]!;

            if (request.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            
            var result = await _bucketService.DownloadFile(request, cancel);
            return File(result.FileStream!, result.FileContentType);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Create Folder
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("folder/create")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<IActionResult> CreateFolder([FromBody] AwsS3BucketDto request, CancellationToken cancel)
    {
        try
        {
            if (request.Key == null) return BadRequest();
            request.BucketName ??= _configuration["AWS_BUCKET"]!;
            if (request.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            var response = await _bucketService.CreateFolder(request, cancel);
            if (response)
                return Ok(new AwsS3ResponseDto<string>($"Folder {request.Key} created successfully."));
            return BadRequest(new AwsS3ResponseErrorDto($"Folder {request.Key} created failed."));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
        
    }
    
    /// <summary>
    /// Delete Folder
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("folder/delete")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> DeleteFolder([FromQuery] AwsS3BucketDto request, CancellationToken cancel)
    {
        try
        {
            request.BucketName ??= _configuration["AWS_BUCKET"]!;

            if (request.BucketName is null)
                throw new AwsExceptions("You need the bucket name or the AWS_BUCKET variable");
            
            await _bucketService.DeleteFolder(request, cancel);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error -> {Message}", ex.Message);
            return BadRequest(new AwsS3ResponseErrorDto(ex.Message));
        }
    }
}