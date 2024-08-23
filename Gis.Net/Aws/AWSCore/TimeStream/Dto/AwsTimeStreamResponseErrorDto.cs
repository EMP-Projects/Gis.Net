using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class AwsTimeStreamResponseErrorDto : AwsResponseErrorDto
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public AwsTimeStreamResponseErrorDto(string message) : base(message)
    {
    }
}