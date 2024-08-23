using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class MessageAttributes
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("queryArn")] public QueryArn? QueryArn { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("notificationType")] public NotificationType? NotificationType { get; set; }
}