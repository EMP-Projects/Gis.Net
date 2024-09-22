using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsSnsDto 
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("topic_arn")] public required string TopicArn { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("topic_name")] public string? TopicName { get; set; } = string.Empty;
    
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("token")] public string? Token { get; set; } = string.Empty;
}