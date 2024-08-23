using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.S3.Dto;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AwsS3ResponseDto<T> : AwsResponseDto<T> where T : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public AwsS3ResponseDto(T result) : base(result)
    {
    }
}