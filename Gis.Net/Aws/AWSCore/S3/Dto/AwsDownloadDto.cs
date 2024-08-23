namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents a data transfer object for downloading a file from AWS S3.
/// </summary>
public class AwsDownloadDto
{

    /// <summary>
    /// Represents a file stream.
    /// </summary>
    public Stream? FileStream { get; set; }

    /// <summary>
    /// Represents the content type of a file.
    /// </summary>
    public string FileContentType { get; set; }

    /// <summary>
    /// Service for interacting with AWS S3 bucket.
    /// </summary>
    public AwsDownloadDto(Stream stream, string contentType)
    {
        FileStream = stream;
        FileContentType = contentType;
    }
}