namespace Gis.Net.Aws.AWSCore.DynamoDb.Models;

/// <summary>
/// Represents the base class for a DynamoDB table entry.
/// </summary>
public interface IAwsDynamoDbTableBase
{
    /// <summary>
    /// Gets or sets the row identifier for the table entry.
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp for the table entry.
    /// </summary>
    // string TimeStamp { get; set; }

    /// <summary>
    /// Gets or sets the key associated with the table entry.
    /// </summary>
    // string Key { get; set; }
}