using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents a data transfer object for uploading files to an AWS S3 bucket.
/// </summary>
public class AwsS3BucketUploadDto : AwsS3BucketFileDto
{
    /// <summary>
    /// Represents a file for uploading to an AWS S3 bucket.
    /// </summary>
    [JsonPropertyName("file")] public IFormFile? File { get; set; }

    /// <summary>
    /// Represents a property that determines whether to replace an existing file.
    /// </summary>
    /// <value>
    /// <c>true</c> to replace the existing file; otherwise, <c>false</c>.
    /// </value>
    [JsonPropertyName("replace")] public bool? Replace { get; set; }

    /// <summary>
    /// TRUE if sharing file
    /// </summary>
    [JsonPropertyName("share")] public bool? Share { get; set; } = true;
}