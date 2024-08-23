using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// 
/// </summary>
public class AwsNotificationDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Type")] public string? Type { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("MessageId")] public string? MessageId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Token")] public string? Token { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("TopicArn")] public string? TopicArn { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Message")] public object? Message { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Timestamp")] public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("SubscribeUrl")] public string? SubscribeUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("SignatureVersion")] public string? SignatureVersion { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("Signature")] public string? Signature { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("SigningCertURL")] public string? SigningCertUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("UnsubscribeURL")] public string? UnSubscribeUrl { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("MessageAttributes")] public MessageAttributes? MessageAttributes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Message? MessageJson => IsNotification ? JsonSerializer.Deserialize<Message>(Message?.ToString()!) : null;

    /// <summary>
    /// 
    /// </summary>
    public bool IsSubscription => Type!.ToUpper().Equals("SubscriptionConfirmation".ToUpper());
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsNotification => Type!.ToUpper().Equals("Notification".ToUpper());
}