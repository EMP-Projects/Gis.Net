using System.Globalization;
using Amazon.DynamoDBv2.DataModel;

namespace Gis.Net.Aws.AWSCore.DynamoDb.Models;

/// <inheritdoc />
public abstract class AwsDynamoDbTableBase : IAwsDynamoDbTableBase
{
    /// <inheritdoc />
    [DynamoDBHashKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <inheritdoc />
    [DynamoDBRangeKey]
    public string TimeStamp { get; set; } = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

    /// <inheritdoc />
    [DynamoDBGlobalSecondaryIndexHashKey("key-index")]
    public required string Key { get; set; }

    /// <inheritdoc />
    [DynamoDBGlobalSecondaryIndexRangeKey("key-index")]
    public required string NextTimeStamp { get; set; }
}