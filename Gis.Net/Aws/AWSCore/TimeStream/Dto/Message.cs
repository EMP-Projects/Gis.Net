using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class Message
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("type")] public string? Type { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("arn")] public string? Arn { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("nextInvocationEpochSecond")] public int NextInvocationEpochSecond { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("scheduledQueryRunSummary")] public ScheduledQueryRunSummary? ScheduledQueryRunSummary { get; set; }
}