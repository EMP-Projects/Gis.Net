using System.Text.Json.Serialization;
using Amazon.TimestreamQuery;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a Data Transfer Object (DTO) for an AWS TimeStream query.
/// </summary>
public class AwsTimeStreamQueryDto
{
    /// <summary>
    /// Gets or sets the Amazon Resource Name (ARN) of the scheduled query.
    /// </summary>
    [JsonPropertyName("scheduled_query_arn")] public string? ScheduledQueryArn { get; set; }

    /// <summary>
    /// Gets or sets the invocation time for a scheduled query.
    /// </summary>
    [JsonPropertyName("invocation_time")] public DateTime InvocationTime { get; set; }

    /// <summary>
    /// Gets or sets the state of the scheduled query.
    /// </summary>
    [JsonPropertyName("state")] public ScheduledQueryState? State { get; set; }
}