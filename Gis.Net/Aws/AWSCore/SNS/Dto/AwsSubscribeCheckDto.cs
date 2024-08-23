using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsSubscribeCheckDto : AwsSubscribeDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("phone_number")] public string? PhoneNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    public AwsSubscribeCheckDto(string token, string topicArn) : base(token, topicArn)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    /// <param name="protocol"></param>
    public AwsSubscribeCheckDto(string token, string topicArn, string protocol) : base(token, topicArn, protocol)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    /// <param name="protocol"></param>
    /// <param name="endPoint"></param>
    public AwsSubscribeCheckDto(string token, string topicArn, string protocol, string endPoint) : base(token, topicArn, protocol, endPoint)
    {
    }
}