using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsTopicRequestDto : AwsSnsDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("use_fifo_topic")] public bool UseFifoTopic { get; set; } = false;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("use_content_based_deduplication")] public bool UseContentBasedDeduplication { get; set; } = false;
}