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
    /// Initializes a new instance of the <see cref="AwsTimeStreamDatabaseService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging.</param>
    /// <param name="timeStreamClient">The Amazon Timestream client instance.</param>
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

        var createDatabaseRequest = new CreateDatabaseRequest
        {
            DatabaseName = options.DatabaseName
        };
        _logger.LogInformation("Database {DatabaseName} created", options.DatabaseName);
        return await _timeStreamClient.CreateDatabaseAsync(createDatabaseRequest, cancel);
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
        var describeDatabaseRequest = new DescribeDatabaseRequest
        {
            DatabaseName = options.DatabaseName
        };
        var response = await _timeStreamClient.DescribeDatabaseAsync(describeDatabaseRequest, cancel);
        _logger.LogInformation("Database {DatabaseName} has id: {DatabaseArn}", options.DatabaseName, response.Database.Arn);
        return response;
    }

    /// <summary>
    /// update your databases.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<UpdateDatabaseResponse> UpdateDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel)
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
    
    /// <summary>
    /// delete database
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<DeleteDatabaseResponse> DeleteDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel)
    {
        var deleteDatabaseRequest = new DeleteDatabaseRequest
        {
            DatabaseName = options.DatabaseName
        };
        var response = await _timeStreamClient.DeleteDatabaseAsync(deleteDatabaseRequest, cancel);
        _logger.LogInformation("Database {OptionsDatabaseName} delete request status:{ResponseHttpStatusCode}", options.DatabaseName, response.HttpStatusCode);
        return response;
    }
    
    /// <summary>
    /// list your databases
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    public async Task<List<Database>> ListDatabases(AwsTimeStreamDatabaseRootDto options, CancellationToken cancel)
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
}