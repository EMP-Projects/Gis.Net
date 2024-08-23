using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents a data transfer object for the root properties of an AWS S3 bucket.
/// </summary>
public class AwsS3BucketRootDto
{
    /// <summary>
    /// Gets or sets the name of the AWS S3 bucket.
    /// </summary>
    [JsonPropertyName("bucket_name")] public string? BucketName { get; set; }

    /// <summary>
    /// Gets or sets the region of the AWS S3 bucket.
    /// </summary>
    [JsonPropertyName("region")] public string? Region { get; set; }
}