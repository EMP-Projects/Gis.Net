using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents an AWS S3 object.
/// </summary>
public class AwsS3ObjectDto
{
    /// <summary>
    /// Represents the prefix of an AWS S3 object.
    /// </summary>
    [JsonPropertyName("prefix")] public string? Prefix { get; set; }

    /// <summary>
    /// Represents the file name of an AWS S3 object.
    /// </summary>
    [JsonPropertyName("fileName")] public string? FileName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("presignedUrl")] public string? PresignedUrl { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of an AWS S3 object.
    /// </summary>
    [JsonPropertyName("lastModified")] public DateTime? LastModified { get; set; }

    /// <summary>
    /// Represents the size of an AWS S3 object.
    /// </summary>
    [JsonPropertyName("size")] public long? Size { get; init; }
}