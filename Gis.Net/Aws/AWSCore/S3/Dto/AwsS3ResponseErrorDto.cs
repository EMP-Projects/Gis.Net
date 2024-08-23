using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// Represents an error response from AWS S3 service.
/// </summary>
public class AwsS3ResponseErrorDto : AwsResponseErrorDto
{
    /// <summary>
    /// Represents an error response from AWS S3 service.
    /// </summary>
    public AwsS3ResponseErrorDto(string message) : base(message)
    {
    }
}