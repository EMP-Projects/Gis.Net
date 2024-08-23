namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a DTO (Data Transfer Object) for an AWS Timestream database.
/// </summary>
public class AwsTimeStreamDatabaseDto : AwsTimeStreamDatabaseRootDto
{
    /// <summary>
    /// Gets or sets the Key Management Service (KMS) key ID for an AWS Timestream database.
    /// </summary>
    public string? KmsKeyId { get; set; }

    /// <summary>
    /// Represents a DTO (Data Transfer Object) for an AWS Timestream database.
    /// </summary>
    public AwsTimeStreamDatabaseDto(string databaseName) => DatabaseName = databaseName;
    
}