using Amazon.TimestreamQuery.Model;

namespace Gis.Net.Aws.AWSCore.TimeStream.Dto;

/// <summary>
/// Represents a scheduled query in AWS Timestream.
/// </summary>
public class AwsTimeStreamScheduledQueryDto : AwsTimeStreamSqlDto
{
    /// <summary>
    /// Represents a scheduled query in AWS Timestream.
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// Represents the name of a table in AWS Timestream.
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// Represents the name of a query in AWS Timestream.
    /// </summary>
    public string? QueryName { get; set; }

    /// <summary>
    /// Represents the Amazon Resource Name (ARN) of the topic to which notifications for the scheduled query are sent in AWS Timestream.
    /// </summary>
    public string? TopicArn { get; set; }

    /// <summary>
    /// The Amazon Resource Name (ARN) of the IAM role to use when executing the scheduled query.
    /// </summary>
    public string? RoleArn { get; set; }

    /// <summary>
    /// Represents the name of the S3 bucket where error reports for a scheduled query in AWS Timestream should be stored.
    /// </summary>
    public string? S3ErrorReportBucketName { get; set; }

    /// <summary>
    /// Represents the schedule expression for a scheduled query in AWS Timestream.
    /// </summary>
    public string? ScheduleExpression { get; set; } = "cron(0 0 * * ? *)"; // every day at 12:00 AM (02:00 UTC Rome)

    /// <summary>
    /// Represents the name of the time column in AWS Timestream.
    /// <para>The time column is used to specify the timestamp for the data stored in Timestream.</para>
    /// <para>This property is optional and has a default value of "TimeColumn".</para>
    /// </summary>
    public string? TimeColumn { get; set; } = "TimeColumn";

    /// <summary>
    /// Represents the name of the target multi-measure in a scheduled query in AWS Timestream.
    /// </summary>
    public string? TargetMultiMeasureName { get; set; } = "";

    /// <summary>
    /// Represents the list of multi-measure attribute mappings for a scheduled query in AWS Timestream.
    /// </summary>
    /// <remarks>
    /// The MeasureAttributes property defines how Timestream measures should be mapped to the target multi-measure attribute.
    /// Each MultiMeasureAttributeMapping object in the list represents a mapping between a Timestream measure and a target multi-measure attribute.
    /// </remarks>
    /// <value>
    /// The list of multi-measure attribute mappings.
    /// </value>
    public List<MultiMeasureAttributeMapping>? MeasureAttributes { get; set; } = new();
    
    /// <summary>
    /// 
    /// </summary>
    public List<DimensionMapping>? Dimensions { get; init; } = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    public AwsTimeStreamScheduledQueryDto(string sql) : base(sql)
    {
    }
}