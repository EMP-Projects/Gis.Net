using Amazon.TimestreamQuery.Model;
using Amazon.TimestreamWrite.Model;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;
using Gis.Net.Aws.AWSCore.TimeStream.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.Controllers;

/// <summary>
/// Controller for managing AWS TimeStream databases and tables.
/// </summary>
public abstract class AwsTimeStreamController : ControllerBase
{
    private readonly ILogger<AwsTimeStreamController> _logger;
    private readonly AwsTimeStreamDatabaseService _awsTimeStreamDatabaseService;
    private readonly AwsTimeStreamQueryService _awsTimeStreamQueryService;
    private readonly AwsTimeStreamTableService _awsTimeStreamTableService;
    
    /// <summary>
    /// Controller Aws TimeStream
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="awsTimeStreamDatabaseService"></param>
    /// <param name="awsTimeStreamQueryService"></param>
    /// <param name="awsTimeStreamTableService"></param>
    protected AwsTimeStreamController(ILogger<AwsTimeStreamController> logger, 
                                   AwsTimeStreamDatabaseService awsTimeStreamDatabaseService, 
                                   AwsTimeStreamQueryService awsTimeStreamQueryService, 
                                   AwsTimeStreamTableService awsTimeStreamTableService)
    {
        _logger = logger;
        _awsTimeStreamDatabaseService = awsTimeStreamDatabaseService;
        _awsTimeStreamQueryService = awsTimeStreamQueryService;
        _awsTimeStreamTableService = awsTimeStreamTableService;
    }
    
    /// <summary>
    /// Create database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("database")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDatabase([FromBody] AwsTimeStreamDatabaseDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamDatabaseService.CreateDatabase(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<CreateDatabaseResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Update database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPut("database")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateDatabase([FromBody] AwsTimeStreamDatabaseDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamDatabaseService.UpdateDatabase(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<UpdateDatabaseResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Delete Database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete("database")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> DeleteDatabase([FromBody] AwsTimeStreamDatabaseDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamDatabaseService.DeleteDatabase(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<DeleteDatabaseResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// List Database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("database")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListDatabase([FromQuery] AwsTimeStreamDatabaseDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamDatabaseService.ListDatabases(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<List<Database>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Describe Database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("database/describe")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DescribeDatabase([FromQuery] AwsTimeStreamDatabaseDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamDatabaseService.DescribeDatabase(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<DescribeDatabaseResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Create Table
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("table")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTable([FromBody] AwsTimeStreamTableDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamTableService.CreateTable(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<CreateTableResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Describe Table
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("table/describe")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DescribeTable([FromQuery] AwsTimeStreamTableDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamTableService.DescribeTable(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<DescribeTableResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Update Table
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPut("table")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateTable([FromBody] AwsTimeStreamTableDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamTableService.UpdateTable(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<UpdateTableResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Delete Table
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete("table")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> DeleteTable([FromBody] AwsTimeStreamTableDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamTableService.DeleteTable(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<DeleteTableResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// List Tables
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("table")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTable([FromQuery] AwsTimeStreamTableDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamTableService.ListTables(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<List<Table>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Write tables
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("table/record")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> WriteTable([FromBody] AwsTimeStreamRecordsDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamTableService.WriteRecords(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<WriteRecordsResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Run query
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("query")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RunQuery([FromBody] AwsTimeStreamSqlDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.RunQueryAsync(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<List<string>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Delete Query
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete("query")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> DeleteQuery([FromBody] AwsTimeStreamSqlDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.CancelQuery(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<CancelQueryResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// List scheduled query
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("query/scheduled")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListScheduledQueries(CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.ListScheduledQueries(cancel);
            return Ok(new AwsTimeStreamResponseDto<List<ScheduledQuery>>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Describe scheduled query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpGet("query/scheduled/describe")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DescribeScheduledQueries([FromQuery] AwsTimeStreamQueryDto options, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.DescribeScheduledQuery(options, cancel);
            return Ok(new AwsTimeStreamResponseDto<string>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Run scheduled query
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("query/scheduled/run")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RunScheduledQuery([FromBody] AwsTimeStreamQueryDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.ExecuteScheduledQuery(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<ExecuteScheduledQueryResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Update query scheduled
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPut("query/scheduled")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateScheduledQuery([FromBody] AwsTimeStreamQueryDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.UpdateScheduledQuery(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<UpdateScheduledQueryResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Delete query scheduled
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpDelete("query/scheduled")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> DeleteScheduledQuery([FromBody] AwsTimeStreamQueryDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.DeleteScheduledQuery(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<DeleteScheduledQueryResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
    
    /// <summary>
    /// Create scheduled query
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    [HttpPost("query/scheduled")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateScheduledQuery([FromBody] AwsTimeStreamScheduledQueryDto request, CancellationToken cancel)
    {
        try
        {
            var result = await _awsTimeStreamQueryService.CreateScheduledQuery(request, cancel);
            return Ok(new AwsTimeStreamResponseDto<CreateScheduledQueryResponse>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
            return BadRequest(new AwsTimeStreamResponseErrorDto(ex.Message));
        }
    }
}
