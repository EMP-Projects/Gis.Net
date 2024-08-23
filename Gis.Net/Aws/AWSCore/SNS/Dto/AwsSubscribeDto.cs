using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsSubscribeDto : AwsSnsDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("protocol")] public string? Protocol { get; set; } = AwsProtocolsConstants.Http;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("endpoint")] public string? EndPoint { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("filter_policy")] public string? FilterPolicy { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    public AwsSubscribeDto(string token, string topicArn)
    {
        Token = token;
        TopicArn = topicArn;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    /// <param name="protocol"></param>
    public AwsSubscribeDto(string token, string topicArn, string protocol) : this(token, topicArn)
    {
        Protocol = protocol;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    /// <param name="protocol"></param>
    /// <param name="endPoint"></param>
    public AwsSubscribeDto(string token, string topicArn, string protocol, string endPoint ) : this(token, topicArn, protocol)
    {
        EndPoint = endPoint;
    }
}