using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents the DTO (Data Transfer Object) for moving a file from one AWS S3 bucket to another.
/// </summary>
public class AwsS3MoveFileDto : AwsS3BucketDto
{
    /// <summary>
    /// Represents the name of the destination bucket for moving a file in AWS S3.
    /// </summary>
    [JsonPropertyName("destination_bucket_name")]
    public string? DestinationBucketName { get; set; }

    /// <summary>
    /// Represents the destination key for moving a file from one AWS S3 bucket to another.
    /// </summary>
    [JsonPropertyName("destination_key")] public string? DestinationKey { get; set; }
}