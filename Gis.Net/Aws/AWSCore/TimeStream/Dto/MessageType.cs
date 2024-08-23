using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class MessageType
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Type")] public string? Type { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Value")] public string? Value { get; set; }
}