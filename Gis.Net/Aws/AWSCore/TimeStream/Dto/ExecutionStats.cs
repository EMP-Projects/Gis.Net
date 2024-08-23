using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class ExecutionStats
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("executionTimeInMillis")] public int ExecutionTimeInMillis { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("dataWrites")] public int DataWrites { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("bytesMetered")] public int BytesMetered { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("recordsIngested")] public int RecordsIngested { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("queryResultRows")] public int QueryResultRows { get; set; }
}