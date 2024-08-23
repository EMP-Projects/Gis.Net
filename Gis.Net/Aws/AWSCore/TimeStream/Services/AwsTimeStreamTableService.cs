using Amazon.TimestreamWrite;
using Amazon.TimestreamWrite.Model;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.AWSCore.TimeStream.Services;

/// <summary>
/// 
/// </summary>
public interface IAwsTimeStreamTableService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<CreateTableResponse> CreateTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<DescribeTableResponse> DescribeTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<UpdateTableResponse> UpdateTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<DeleteTableResponse> DeleteTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<List<Table>> ListTables(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<WriteRecordsResponse> WriteRecords(AwsTimeStreamRecordsDto options, CancellationToken cancel);
}

/// <summary>
/// 
/// </summary>
public class AwsTimeStreamTableService : IAwsTimeStreamTableService
{
    private readonly ILogger<AwsTimeStreamTableService> _logger;
    private readonly IAmazonTimestreamWrite _timeStreamClient;

    /// <summary>
    /// Interface for AWS TimeStream Table service.
    /// </summary>
    public AwsTimeStreamTableService(ILogger<AwsTimeStreamTableService> logger,
        IAmazonTimestreamWrite timeStreamClient)
    {
        _logger = logger;
        _timeStreamClient = timeStreamClient;
    }

    /// <summary>
    /// create a table that has magnetic store writes disabled,
    /// as a result you can only write data into your memory store retention window.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    public async Task<CreateTableResponse> CreateTable(AwsTimeStreamTableDto options, CancellationToken cancel)
    {
        try
        {
            var createTableRequest = new CreateTableRequest
            {
                DatabaseName = options.DatabaseName,
                TableName = options.TableName,
                RetentionProperties = new RetentionProperties
                {
                    MagneticStoreRetentionPeriodInDays = options.CtTtlDays,
                    MemoryStoreRetentionPeriodInHours = options.HtTtlHours
                }
            };
            
            if (options.EnableMagneticWrite)
                createTableRequest.MagneticStoreWriteProperties = new MagneticStoreWriteProperties
                {
                    EnableMagneticStoreWrites = options.EnableMagneticWrite
                };
            
            if (options.PartitionKeys is { Count: > 0 })
                createTableRequest.Schema = new Schema
                {
                    CompositePartitionKey = options.PartitionKeys
                };
            
            var response = await _timeStreamClient.CreateTableAsync(createTableRequest, cancel);
            _logger.LogInformation("Table {OptionsTableName} created", options.TableName);
            return response;
        }
        catch (ConflictException ex)
        {
            _logger.LogError("Table already exists");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Created table failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }

    }
    
    /// <summary>
    /// get information about the attributes of your table
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<DescribeTableResponse> DescribeTable(AwsTimeStreamTableDto options, CancellationToken cancel)
    {
        try
        {
            var describeTableRequest = new DescribeTableRequest
            {
                DatabaseName = options.DatabaseName,
                TableName = options.TableName
            };
            var response = await _timeStreamClient.DescribeTableAsync(describeTableRequest, cancel);
            _logger.LogInformation("Table {OptionsTableName} has id {Id}", options.TableName, response.Table.Arn);
            return response;
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Table does not exist");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Describe table failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }

    }
    
    /// <summary>
    /// update a table
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<UpdateTableResponse> UpdateTable(AwsTimeStreamTableDto options, CancellationToken cancel)
    {
        try
        {
            var updateTableRequest = new UpdateTableRequest
            {
                DatabaseName = options.DatabaseName,
                TableName = options.TableName,
                RetentionProperties = new RetentionProperties
                {
                    MagneticStoreRetentionPeriodInDays = options.CtTtlDays,
                    MemoryStoreRetentionPeriodInHours = options.HtTtlHours
                }
            };
            var response = await _timeStreamClient.UpdateTableAsync(updateTableRequest, cancel);
            _logger.LogInformation("Table {OptionsTableName} updated", options.TableName);
            return response;
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Table does not exist");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Update table failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
    
    /// <summary>
    /// delete a table
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<DeleteTableResponse> DeleteTable(AwsTimeStreamTableDto options, CancellationToken cancel)
    {
        Console.WriteLine("Deleting table");
        try
        {
            var deleteTableRequest = new DeleteTableRequest
            {
                DatabaseName = options.DatabaseName,
                TableName = options.TableName
            };
            var response = await _timeStreamClient.DeleteTableAsync(deleteTableRequest, cancel);
            _logger.LogInformation("Table {OptionsTableName} delete request status: {Status}", options.TableName, response.HttpStatusCode);
            return response;
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Table does not exist");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception while deleting table: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
    
    /// <summary>
    /// list tables
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<List<Table>> ListTables(AwsTimeStreamTableDto options, CancellationToken cancel)
    {
        try
        {
            var listTablesRequest = new ListTablesRequest
            {
                MaxResults = options.MaxResult,
                DatabaseName = options.DatabaseName
            };
            var response = await _timeStreamClient.ListTablesAsync(listTablesRequest, cancel);
            var listResult = response.Tables;
            var nextToken = response.NextToken;
            while (nextToken != null)
            {
                listTablesRequest.NextToken = nextToken;
                response = await _timeStreamClient.ListTablesAsync(listTablesRequest, cancel);
                listResult.AddRange(response.Tables);
                nextToken = response.NextToken;
            }

            return listResult;
        }
        catch (Exception ex)
        {
            _logger.LogError("List table failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Write data into an Amazon TimeStream table.
    /// Writing data in batches helps to optimize the cost of writes.
    ///
    /// Info: https://docs.aws.amazon.com/timestream/latest/developerguide/metering-and-pricing.writes.html#metering-and-pricing.writes.write-size-multiple-events
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<WriteRecordsResponse> WriteRecords(AwsTimeStreamRecordsDto options, CancellationToken cancel)
    {
        try
        {
            var writeRecordsRequest = new WriteRecordsRequest
            {
                DatabaseName = options.DatabaseName,
                TableName = options.TableName
            };

            if (options.Records is { Count: > 0 })
                writeRecordsRequest.Records = options.Records;
            
            var response = await _timeStreamClient.WriteRecordsAsync(writeRecordsRequest, cancel);
            _logger.LogInformation("Write records status code: {Status}", response.HttpStatusCode.ToString());
            return response;
        }
        catch (RejectedRecordsException ex) {
            _logger.LogError("RejectedRecordsException: {Message}", ex.ToString());
            foreach (var rr in ex.RejectedRecords) 
                _logger.LogError("RecordIndex {Index} : {Reason}", rr.RecordIndex, rr.Reason);
            _logger.LogInformation("Other records were written successfully");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Write records failure: {Failure}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
}