using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Gis.Net.Aws.AWSCore.DynamoDb.Models;

namespace Gis.Net.Aws.AWSCore.DynamoDb;

/// <summary>
/// Represents a service interface for interacting with AWS DynamoDB.
/// </summary>
public interface IAwsDynamoDbService<TModel> 
    where TModel : AwsDynamoDbTableBase
{
    /// <summary>
    /// Gets the DynamoDB client.
    /// </summary>
    AmazonDynamoDBClient Client { get; }

    /// <summary>
    /// Gets the DynamoDB context.
    /// </summary>
    DynamoDBContext Context { get; }

    /// <summary>
    /// Lists all tables in the DynamoDB asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous list tables operation. The task result contains the response from the ListTables operation.</returns>
    Task<ListTablesResponse> ListTablesAsync();

    /// <summary>
    /// Checks if a table exists in the DynamoDB asynchronously.
    /// </summary>
    /// <param name="tableName">The name of the table to check.</param>
    /// <returns>A task that represents the asynchronous table existence check operation. The task result contains a boolean indicating whether the table exists.</returns>
    Task<bool> TableExistsAsync(string tableName);
    
    /// <summary>
    /// Inserts or Update an item into the DynamoDB table asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the item to insert.</typeparam>
    /// <param name="item">The item to insert.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous insert operation.</returns>
    Task InsertOrUpdateAsync(TModel item, CancellationToken cancel = default);

    /// <summary>
    /// Queries the DynamoDB table asynchronously.
    /// </summary>
    /// <typeparam name="TModel">The type of the items to query.</typeparam>
    /// <param name="hashKeyValue">The hash key value to query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous query operation. The task result contains a list of queried items.</returns>
    Task<List<TModel>?> QueryAsync(string hashKeyValue, CancellationToken cancel = default);

    /// <summary>
    /// Retrieves an item from the DynamoDB table asynchronously by its identifier.
    /// </summary>
    /// <typeparam name="TModel">The type of the item to retrieve.</typeparam>
    /// <param name="id">The identifier of the item to retrieve.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous get operation. The task result contains the retrieved item.</returns>
    Task<TModel> GetItemAsync(long id, CancellationToken cancel = default);

    /// <summary>
    /// Retrieves an item from the DynamoDB table asynchronously by its identifier and timestamp.
    /// </summary>
    /// <typeparam name="TModel">The type of the item to retrieve.</typeparam>
    /// <param name="id">The identifier of the item to retrieve.</param>
    /// <param name="timeStamp">The timestamp of the item to retrieve.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous get operation. The task result contains the retrieved item.</returns>
    Task<TModel> GetItemAsync(long id, DateTime timeStamp, CancellationToken cancel = default);

    /// <summary>
    /// Deletes an item from the DynamoDB table asynchronously by its identifier.
    /// </summary>
    /// <typeparam name="T">The type of the item to delete.</typeparam>
    /// <param name="id">The identifier of the item to delete.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteItemAsync(long id, CancellationToken cancel = default);
    
    /// <summary>
    /// Deletes an item from the DynamoDB table asynchronously by its identifier and timestamp.
    /// </summary>
    /// <typeparam name="T">The type of the item to delete.</typeparam>
    /// <param name="id">The identifier of the item to delete.</param>
    /// <param name="timeStamp">The timestamp of the item to delete.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteItemAsync(long id, DateTime timeStamp, CancellationToken cancel = default);
    
    /// <summary>
    /// Lists items from the DynamoDB table asynchronously based on the specified scan conditions.
    /// </summary>
    /// <param name="conditions">The scan conditions to apply. If null, all items are listed.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous list operation. The task result contains an enumerable of listed items.</returns>
    Task<IEnumerable<TModel>> ListItemsAsync(List<ScanCondition>? conditions = null, CancellationToken cancel = default);

    /// <summary>
    /// Adds a property converter for the specified type.
    /// </summary>
    /// <typeparam name="T">The type for which the converter is added. Must be a class.</typeparam>
    void AddConverter<T>() where T : class;
    
    /// <summary>
    /// Creates a new table in DynamoDB asynchronously.
    /// </summary>
    /// <param name="tableName">The name of the table to create.</param>
    /// <param name="keySchema">The key schema for the table.</param>
    /// <param name="attributeDefinitions">The attribute definitions for the table.</param>
    /// <param name="provisionedThroughput">The provisioned throughput settings for the table.</param>
    /// <returns>A task that represents the asynchronous create table operation.</returns>
    Task CreateTableAsync(string tableName, List<KeySchemaElement> keySchema, List<AttributeDefinition> attributeDefinitions, ProvisionedThroughput provisionedThroughput);
}