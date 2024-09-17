using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Gis.Net.Aws.AWSCore.DynamoDb.Models;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Features;

namespace Gis.Net.Aws.AWSCore.DynamoDb;

/// <inheritdoc />
public abstract class AwsDynamoDbService<TModel> : IAwsDynamoDbService<TModel> 
    where TModel : AwsDynamoDbTableBase
{
    private readonly IConfiguration _configuration;
    
    protected AwsDynamoDbService(IConfiguration configuration)
    {
        _configuration = configuration;
        
        // Info: https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/CRUDHighLevelExample1.html
        Client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
            // This client will access the US East 1 region.
            RegionEndpoint = _configuration.GetAWSOptions().Region
        }); 
        Context = new DynamoDBContext(Client);

        // Register the GeoJson converter.
        Context.ConverterCache.Add(typeof(FeatureCollection), new AwsDynamoDbConverter<FeatureCollection>());
        Context.ConverterCache.Add(typeof(Feature), new AwsDynamoDbConverter<Feature>());
    }

    /// <inheritdoc />
    public virtual void AddConverter<T>() where T : class
    {
        if (!Context.ConverterCache.ContainsValue(new AwsDynamoDbConverter<T>()))
            Context.ConverterCache.Add(typeof(T), new AwsDynamoDbConverter<T>());
    }

    /// <inheritdoc />
    public async Task<ListTablesResponse> ListTablesAsync() => await Client.ListTablesAsync();

    /// <inheritdoc />
    public async Task<bool> TableExistsAsync(string tableName)
    {
        var tables = await ListTablesAsync();
        return tables.TableNames.Contains(tableName);
    }

    /// <inheritdoc />
    public async Task CreateTableAsync(string tableName, List<KeySchemaElement> keySchema, List<AttributeDefinition> attributeDefinitions, ProvisionedThroughput provisionedThroughput)
    {
        if (await TableExistsAsync(tableName)) return;
        
        var request = new CreateTableRequest
        {
            TableName = tableName,
            KeySchema = keySchema,
            AttributeDefinitions = attributeDefinitions,
            ProvisionedThroughput = provisionedThroughput
        };
        await Client.CreateTableAsync(request);
    }

    /// <inheritdoc />
    public AmazonDynamoDBClient Client { get; }

    /// <inheritdoc />
    public DynamoDBContext Context { get; }

    /// <inheritdoc />
    public async Task InsertOrUpdateAsync(TModel item, CancellationToken cancel = default)
    {
        if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null.");
        
        // Insert the item into the table.
        await Context.SaveAsync(item, new DynamoDBOperationConfig
        {
            ConsistentRead = true,
            
        }, cancel);
    }

    /// <inheritdoc />
    public async Task<List<TModel>?> QueryAsync(string hashKeyValue, CancellationToken cancel = default)
    {
        // Construct a query that finds all replies by a specific author.
        var query = Context.QueryAsync<TModel>(hashKeyValue, new DynamoDBOperationConfig { ConsistentRead = true});

        // Return the list of replies.
        return await query.GetNextSetAsync(cancel);
    }

    /// <inheritdoc />
    public async Task<TModel> GetItemAsync(long id, CancellationToken cancel = default)
    {
        return await Context.LoadAsync<TModel>(id, new DynamoDBContextConfig
        {
            ConsistentRead = true,
        }, cancel);
    }

    /// <inheritdoc />
    public async Task<TModel> GetItemAsync(long id, DateTime timeStamp, CancellationToken cancel = default)
    {
        return await Context.LoadAsync<TModel>(id, timeStamp, cancel);
    }

    /// <inheritdoc />
    public async Task DeleteItemAsync(long id, CancellationToken cancel = default)
    {
        await Context.DeleteAsync<TModel>(id, cancel);
    }

    /// <inheritdoc />
    public async Task DeleteItemAsync(long id, DateTime timeStamp, CancellationToken cancel = default)
    {
        await Context.DeleteAsync<TModel>(id, timeStamp, cancel);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TModel>> ListItemsAsync(List<ScanCondition>? conditions = null, CancellationToken cancel = default)
    {
        var query = conditions ?? [];
        return await Context.ScanAsync<TModel>(query).GetRemainingAsync(cancel);
    }
}