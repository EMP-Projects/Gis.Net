using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsPublishDto : AwsSnsDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("message")] public string? Message { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("attribute_name")] public string? AttributeName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("attribute_value")] public string? AttributeValue { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("deduplication_id")] public string? DeDuplicationId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("group_id")] public string? GroupId { get; set; }
}