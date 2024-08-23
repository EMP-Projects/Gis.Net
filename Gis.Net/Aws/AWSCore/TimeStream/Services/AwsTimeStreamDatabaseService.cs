using Amazon.TimestreamWrite;
using Amazon.TimestreamWrite.Model;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.AWSCore.TimeStream.Services;

/// <summary>
/// Interface for interacting with the AWS TimeStream database service.
/// </summary>
public class AwsTimeStreamDatabaseService : IAwsTimeStreamDatabaseService
{
    private readonly ILogger<AwsTimeStreamDatabaseService> _logger;
    private readonly IAmazonTimestreamWrite _timeStreamClient;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <param name="timeStreamClient"></param>
    public AwsTimeStreamDatabaseService(ILogger<AwsTimeStreamDatabaseService> logger, 
                                IAmazonTimestreamWrite timeStreamClient)
    {
        _logger = logger;
        _timeStreamClient = timeStreamClient;
    }

    /// <summary>
    /// Create a database.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<CreateDatabaseResponse> CreateDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel)
    {
        Console.WriteLine("Creating Database");

        try
        {
            var createDatabaseRequest = new CreateDatabaseRequest
            {
                DatabaseName = options.DatabaseName
            };
            _logger.LogInformation("Database {DatabaseName} created", options.DatabaseName);
            return await _timeStreamClient.CreateDatabaseAsync(createDatabaseRequest, cancel);
        }
        catch (ConflictException ex)
        {
            _logger.LogError("Database already exists");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Create database failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }

    }
    
    /// <summary>
    /// get information about the attributes of your newly created database.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<DescribeDatabaseResponse> DescribeDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel)
    {
        try
        {
            var describeDatabaseRequest = new DescribeDatabaseRequest
            {
                DatabaseName = options.DatabaseName
            };
            var response = await _timeStreamClient.DescribeDatabaseAsync(describeDatabaseRequest, cancel);
            _logger.LogInformation("Database {DatabaseName} has id: {DatabaseArn}", options.DatabaseName, response.Database.Arn);
            return response;
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Database does not exist");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Describe database failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }

    }

    /// <summary>
    /// update your databases.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<UpdateDatabaseResponse> UpdateDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel)
    {
        Console.WriteLine("Updating Database");

        try
        {
            var updateDatabaseRequest = new UpdateDatabaseRequest
            {
                DatabaseName = options.DatabaseName,
                KmsKeyId = options.KmsKeyId
            };
            var response = await _timeStreamClient.UpdateDatabaseAsync(updateDatabaseRequest, cancel);
            _logger.LogInformation("Database {OptionsDatabaseName} updated with KmsKeyId {OptionsKmsKeyId}", options.DatabaseName, options.KmsKeyId);
            return response;
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Database does not exist");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Update database failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }

    }
    
    /// <summary>
    /// delete database
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<DeleteDatabaseResponse> DeleteDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel)
    {
        try
        {
            var deleteDatabaseRequest = new DeleteDatabaseRequest
            {
                DatabaseName = options.DatabaseName
            };
            var response = await _timeStreamClient.DeleteDatabaseAsync(deleteDatabaseRequest, cancel);
            _logger.LogInformation("Database {OptionsDatabaseName} delete request status:{ResponseHttpStatusCode}", options.DatabaseName, response.HttpStatusCode);
            return response;
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Database does not exist");
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception while deleting database: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
    
    /// <summary>
    /// list your databases
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    public async Task<List<Database>> ListDatabases(AwsTimeStreamDatabaseRootDto options, CancellationToken cancel)
    {
        try
        {
            var listDatabasesRequest = new ListDatabasesRequest
            {
                MaxResults = options.MaxResult
            };
            
            var response = await _timeStreamClient.ListDatabasesAsync(listDatabasesRequest, cancel);
            List<Database> listResponse = response.Databases;
            
            var nextToken = response.NextToken;
            while (nextToken != null)
            {
                listDatabasesRequest.NextToken = nextToken;
                response = await _timeStreamClient.ListDatabasesAsync(listDatabasesRequest, cancel);
                listResponse.AddRange(response.Databases);
                nextToken = response.NextToken;
            }

            return listResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError("List database failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
}