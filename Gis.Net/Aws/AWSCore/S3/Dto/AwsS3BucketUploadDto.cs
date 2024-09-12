using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents a data transfer object for uploading files to an AWS S3 bucket.
/// </summary>
public class AwsS3BucketUploadDto : AwsS3BucketFileDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AwsS3BucketUploadDto"/> class with the specified file text.
    /// </summary>
    /// <param name="fileText">The text content of the file to be uploaded.</param>
    public AwsS3BucketUploadDto(string fileText)
    {
        Stream = new MemoryStream(Encoding.UTF8.GetBytes(fileText));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AwsS3BucketUploadDto"/> class with the specified memory stream.
    /// </summary>
    /// <param name="stream">The memory stream of the file to be uploaded.</param>
    public AwsS3BucketUploadDto(MemoryStream stream)
    {
        Stream = stream;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AwsS3BucketUploadDto"/> class with the specified form file.
    /// </summary>
    /// <param name="file">The form file to be uploaded.</param>
    public AwsS3BucketUploadDto(IFormFile file)
    {
        File = file;
    }
    
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
    
    /// <summary>
    /// Represents the memory stream of the file to be uploaded.
    /// </summary>
    [JsonPropertyName("stream")] public MemoryStream? Stream { get; set; }

    /// <summary>
    /// Represents the content type of the file being uploaded.
    /// </summary>
    /// <value>
    /// The content type of the file. Default is "application/json".
    /// </value>
    [JsonPropertyName("contentType")] public string ContentType = "application/json";
}