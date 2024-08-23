using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class ScheduledQueryRunSummary
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("invocationEpochSecond")] public int InvocationEpochSecond { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("triggerTimeMillis")] public long TriggerTimeMillis { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("runStatus")] public string? RunStatus { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("executionStats")] public ExecutionStats? ExecutionStats { get; set; }
}