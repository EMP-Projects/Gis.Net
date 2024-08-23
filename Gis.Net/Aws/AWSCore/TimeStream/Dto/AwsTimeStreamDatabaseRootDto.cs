namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a root DTO (Data Transfer Object) for AWS Timestream databases.
/// </summary>
public class AwsTimeStreamDatabaseRootDto
{
    /// <summary>
    /// Represents the maximum number of results to be returned.
    /// </summary>
    public int MaxResult { get; set; } = 5;

    /// <summary>
    /// Represents the name of a database in AWS Timestream.
    /// </summary>
    public string? DatabaseName { get; init; }

    /// <summary>
    /// Represents a root DTO (Data Transfer Object) for AWS Timestream databases.
    /// </summary>
    public AwsTimeStreamDatabaseRootDto() {} 
}