using System.Globalization;
using Amazon.DynamoDBv2.DataModel;

namespace Gis.Net.Aws.AWSCore.DynamoDb.Models;

/// <inheritdoc />
public abstract class AwsDynamoDbTableBase : IAwsDynamoDbTableBase
{
    /// <inheritdoc />
    [DynamoDBHashKey("Id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <inheritdoc />
    [DynamoDBRangeKey("TimeStamp")]
    public string TimeStamp { get; set; } = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

    /// <inheritdoc />
    [DynamoDBGlobalSecondaryIndexHashKey("Key-NextTimeStamp-Index", AttributeName = "Key")]
    public required string Key { get; set; }

    /// <inheritdoc />
    [DynamoDBGlobalSecondaryIndexRangeKey("Key-NextTimeStamp-Index", AttributeName = "NextTimeStamp")]
    public required string NextTimeStamp { get; set; }
}