using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a Data Transfer Object (DTO) for an Amazon Timestream SQL query.
/// </summary>
public class AwsTimeStreamSqlDto
{
    /// <summary>
    /// Represents a valid query for an Amazon Timestream SQL query.
    /// </summary>
    [JsonPropertyName("query")] public string? ValidQuery { get; set; }

    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an Amazon Timestream SQL query.
    /// </summary>
    public AwsTimeStreamSqlDto(string sql) => ValidQuery = sql;
}