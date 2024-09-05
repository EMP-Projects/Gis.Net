using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents a data transfer object for an AWS S3 bucket.
/// </summary>
public class AwsS3BucketDto : AwsS3BucketRootDto
{
    /// <summary>
    /// Path files
    /// </summary>
    [JsonPropertyName("prefix")] public string? Prefix { get; set; }
    
    /// <summary>
    /// Search Key
    /// </summary>
    [JsonPropertyName("key")] public string? Key { get; set; }
    
    /// <summary>
    /// Gets or sets the continuation token for paginated results.
    /// </summary>
    [JsonPropertyName("continuationToken")] public string? ContinuationToken { get; set; }
}