using Amazon.TimestreamWrite.Model;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a DTO (Data Transfer Object) for a single AWS Timestream record.
/// </summary>
public class AwsTimeStreamRecordsDto : AwsTimeStreamTableDto
{
    /// <summary>
    /// Represents a DTO (Data Transfer Object) for a single AWS Timestream record.
    /// </summary>
    public List<Record> Records { get; set; } = new();

    /// <summary>
    /// Represents a DTO (Data Transfer Object) for a single AWS Timestream record.
    /// </summary>
    public AwsTimeStreamRecordsDto(string databaseName) : base(databaseName)
    {
    }

    /// <summary>
    /// Represents a DTO (Data Transfer Object) for a single AWS Timestream record.
    /// </summary>
    public AwsTimeStreamRecordsDto(string databaseName, string tableName) : base(databaseName, tableName)
    {
    }
}