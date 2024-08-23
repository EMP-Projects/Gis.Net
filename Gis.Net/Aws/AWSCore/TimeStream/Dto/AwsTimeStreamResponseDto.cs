using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a generic response DTO for the AWS TimeStream service.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
public class AwsTimeStreamResponseDto<T> : AwsResponseDto<T> where T : class
{
    /// <summary>
    /// Represents a generic response DTO for the AWS TimeStream service.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    public AwsTimeStreamResponseDto(T result) : base(result)
    {
    }
}