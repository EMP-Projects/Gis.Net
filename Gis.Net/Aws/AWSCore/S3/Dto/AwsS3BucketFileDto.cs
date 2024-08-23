using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents a data transfer object for an AWS S3 bucket file.
/// </summary>
public class AwsS3BucketFileDto : AwsS3BucketDto
{
    /// <summary>
    /// Represents a data transfer object for an AWS S3 bucket file.
    /// </summary>
    [JsonPropertyName("minutes")] public long Minutes { get; set; } = 5;

    /// <summary>
    /// Represents the filename of an AWS S3 bucket file.
    /// </summary>
    [JsonPropertyName("filename")] public string? Filename { get; set; }
    
}