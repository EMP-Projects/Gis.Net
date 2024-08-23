using Amazon.TimestreamWrite.Model;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a DTO (Data Transfer Object) for an AWS Timestream table.
/// </summary>
public class AwsTimeStreamTableDto : AwsTimeStreamDatabaseDto
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an AWS Timestream table.
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// Gets or sets the time-to-live (TTL) period, in hours, for the memory store in the AWS Timestream table.
    /// </summary>
    /// <remarks>
    /// The memory store is the primary store for data in the table. After the TTL period has passed, data in the memory store is automatically deleted.
    /// </remarks>
    /// <value>
    /// The TTL period, in hours, for the memory store. The default value is 24 hours.
    /// </value>
    public long HtTtlHours { get; set; } = 24;

    /// <summary>
    /// Represents the number of days for the retention period of the Timestream table.
    /// </summary>
    public long CtTtlDays { get; set; } = 6;

    /// <summary>
    /// Represents a boolean flag indicating whether magnetic store writes are enabled for an AWS Timestream table.
    /// </summary>
    public bool EnableMagneticWrite { get; set; } = false;

    /// <summary>
    /// Represents the list of partition keys for an AWS Timestream table.
    /// </summary>
    public List<PartitionKey>? PartitionKeys { get; set; }

    /// <summary>
    /// Represents a DTO (Data Transfer Object) for an AWS Timestream table.
    /// </summary>
    public AwsTimeStreamTableDto(string databaseName) : base(databaseName)
    {
    }

    /// <summary>
    /// Represents a DTO (Data Transfer Object) for an AWS Timestream table.
    /// </summary>
    public AwsTimeStreamTableDto(string databaseName, string tableName) : base(databaseName) => TableName = tableName;
}