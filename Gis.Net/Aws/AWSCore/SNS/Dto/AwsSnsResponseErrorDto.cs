using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsSnsResponseErrorDto : AwsResponseErrorDto
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public AwsSnsResponseErrorDto(string message) : base(message)
    {
    }
}