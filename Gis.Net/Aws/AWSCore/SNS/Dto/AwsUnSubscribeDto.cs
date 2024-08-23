using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsUnSubscribeDto : AwsSnsDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("subscription_arn")] public string? SubscriptionArn { get; set; }
}