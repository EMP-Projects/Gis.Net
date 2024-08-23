using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AwsSnsResponseDto<T> : AwsResponseDto<T> where T : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public AwsSnsResponseDto(T result) : base(result)
    {
    }
}