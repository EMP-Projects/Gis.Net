using Amazon.TimestreamQuery;
using Amazon.TimestreamQuery.Model;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gis.Net.Aws.AWSCore.TimeStream.Services;

/// <summary>
/// 
/// </summary>
public interface IAwsTimeStreamQueryService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<List<string>> RunQueryAsync(AwsTimeStreamSqlDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<CancelQueryResponse> CancelQuery(AwsTimeStreamSqlDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<List<ScheduledQuery>> ListScheduledQueries(CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string> DescribeScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<ExecuteScheduledQueryResponse> ExecuteScheduledQuery(AwsTimeStreamQueryDto options,
        CancellationToken cancel);
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<UpdateScheduledQueryResponse> UpdateScheduledQuery(AwsTimeStreamQueryDto options,
        CancellationToken cancel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<DeleteScheduledQueryResponse> DeleteScheduledQuery(AwsTimeStreamQueryDto options,
        CancellationToken cancel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<CreateScheduledQueryResponse> CreateScheduledQuery(AwsTimeStreamScheduledQueryDto options,
        CancellationToken cancel);
}

/// <summary>
/// 
/// </summary>
public class AwsTimeStreamQueryService : IAwsTimeStreamQueryService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AwsTimeStreamQueryService> _logger;
    private readonly IAmazonTimestreamQuery _timeStreamQueryClient;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <param name="timeStreamQueryClient"></param>
    public AwsTimeStreamQueryService(IConfiguration configuration, 
                                     ILogger<AwsTimeStreamQueryService> logger, 
                                     IAmazonTimestreamQuery timeStreamQueryClient)
    {
        _configuration = configuration;
        _logger = logger;
        _timeStreamQueryClient = timeStreamQueryClient;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private IEnumerable<string> ParseQueryResult(QueryResponse response) 
        => response.Rows.Select(row => ParseRow(response.ColumnInfo, row)).ToList();
    
    private string ParseRow(IReadOnlyList<ColumnInfo> columnInfo, Row row)
    {
        var rowOutput = row.Data.Select((d, index) => ParseDatum(columnInfo[index], d)).ToList();
        return $"{{{string.Join(",", rowOutput)}}}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="datum"></param>
    /// <returns></returns>
    private string ParseDatum(ColumnInfo info, Datum datum)
    {
        if (datum.NullValue)
            return $"\"{info.Name}\": \"NULL\"";

        var columnType = info.Type;
        
        if (columnType.TimeSeriesMeasureValueColumnInfo != null)
            return ParseTimeSeries(info, datum);

        if (columnType.ArrayColumnInfo == null)
            return columnType.RowColumnInfo is not { Count: > 0 }
                ? ParseScalarType(info, datum)
                : ParseRow(info.Type.RowColumnInfo, datum.RowValue);
        
        List<Datum> arrayValues = datum.ArrayValue;
        return $"\"{info.Name}\"=\"{ParseArray(info.Type.ArrayColumnInfo, arrayValues)}\"";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="datum"></param>
    /// <returns></returns>
    private string ParseTimeSeries(ColumnInfo info, Datum datum)
    {
        var timeSeriesString = datum.TimeSeriesValue
            .Select(value => $"{{\"time\":\"{value.Time}\", \"value\":\"{ParseDatum(info.Type.TimeSeriesMeasureValueColumnInfo, value.Value)}\"}}")
            .Aggregate((current, next) => current + "," + next);

        return $"[{timeSeriesString}]";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="datum"></param>
    /// <returns></returns>
    private static string ParseScalarType(ColumnInfo info, Datum datum) 
        => ParseColumnName(info) + $"\"{datum.ScalarValue}\"";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    private static string ParseColumnName(ColumnInfo info) 
        => info.Name == null ? "" : ($"\"{info.Name}\"" + ":");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arrayColumnInfo"></param>
    /// <param name="arrayValues"></param>
    /// <returns></returns>
    private string ParseArray(ColumnInfo arrayColumnInfo, IEnumerable<Datum> arrayValues) 
        => $"[{arrayValues.Select(value => ParseDatum(arrayColumnInfo, value)).Aggregate((current, next) => current + "," + next)}]";

    /// <summary>
    /// When you run a query, TimeStream returns the result set in a paginated manner to optimize
    /// the responsiveness of your applications.
    /// The RunQueryAsync shows how you can paginate through the result set.
    /// You must loop through all the result set pages until you encounter a null value.
    /// Pagination tokens expire 3 hours after being issued by TimeStream.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<List<string>> RunQueryAsync(AwsTimeStreamSqlDto options, CancellationToken cancel)
    {
        try
        {
            var queryRequest = new QueryRequest
            {
                QueryString = options.ValidQuery
            };

            List<string> queryStringResult = new();
            
            var queryResponse = await _timeStreamQueryClient.QueryAsync(queryRequest, cancel);
            while (true)
            {
                queryStringResult.AddRange(ParseQueryResult(queryResponse).ToList());
                if (queryResponse.NextToken == null)
                    break;
                queryRequest.NextToken = queryResponse.NextToken;
                queryResponse = await _timeStreamQueryClient.QueryAsync(queryRequest, cancel);
            }

            return queryStringResult;
        } 
        catch(Exception ex)
        {
            // Some queries might fail with 500 if the result of a sequence function has more than 10000 entries
            _logger.LogError("{Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// cancel a query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<CancelQueryResponse> CancelQuery(AwsTimeStreamSqlDto options, CancellationToken cancel)
    {
        var queryRequest = new QueryRequest
        {
            QueryString = options.ValidQuery
        };
        var queryResponse = await _timeStreamQueryClient.QueryAsync(queryRequest, cancel);

        var cancelQueryRequest = new CancelQueryRequest
        {
            QueryId = queryResponse.QueryId
        };

        try
        {
            return await _timeStreamQueryClient.CancelQueryAsync(cancelQueryRequest, cancel);
        } 
        catch(Exception ex)
        {
            _logger.LogError("Could not cancel the query: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
    
    /// <summary>
    /// list scheduled query
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="AwsExceptions"></exception>
    public async Task<List<ScheduledQuery>> ListScheduledQueries(CancellationToken cancel)
    {
        try
        {
            string nextToken;
            List<ScheduledQuery> listResult = new();
            
            do
            {
                var response = await _timeStreamQueryClient.ListScheduledQueriesAsync(new ListScheduledQueriesRequest(), cancel);
                listResult.AddRange(response.ScheduledQueries.Select(q => q).ToList());
                nextToken = response.NextToken;
            } while (nextToken != null);

            return listResult;
        }
        catch (Exception ex)
        {
            _logger.LogError("List Scheduled Query failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// describe a scheduled query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> DescribeScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel)
    {
        try
        {
            var response = await _timeStreamQueryClient.DescribeScheduledQueryAsync(
                new DescribeScheduledQueryRequest
                {
                    ScheduledQueryArn = options.ScheduledQueryArn
                }, cancel);
            return $"{JsonConvert.SerializeObject(response.ScheduledQuery)}";
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Scheduled Query doesn't exist: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Describe Scheduled Query failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// run a scheduled query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    public async Task<ExecuteScheduledQueryResponse> ExecuteScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel)
    {
        try
        {
            return await _timeStreamQueryClient.ExecuteScheduledQueryAsync(new ExecuteScheduledQueryRequest()
            {
                ScheduledQueryArn = options.ScheduledQueryArn,
                InvocationTime = options.InvocationTime
            }, cancel);
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Scheduled Query doesn't exist: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Execute Scheduled Query failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
    
    /// <summary>
    /// update a scheduled query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    public async Task<UpdateScheduledQueryResponse> UpdateScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel)
    {
        try
        {
            return await _timeStreamQueryClient.UpdateScheduledQueryAsync(new UpdateScheduledQueryRequest
            {
                ScheduledQueryArn = options.ScheduledQueryArn,
                State = options.State
            }, cancel);
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogError("Scheduled Query doesn't exist: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Update Scheduled Query failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
    
    /// <summary>
    /// delete a scheduled query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<DeleteScheduledQueryResponse> DeleteScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel)
    {
        try
        {
            return await _timeStreamQueryClient.DeleteScheduledQueryAsync(new DeleteScheduledQueryRequest
            {
                ScheduledQueryArn = options.ScheduledQueryArn
            }, cancel);
        }
        catch (Exception ex)
        {
            _logger.LogError("Scheduled Query deletion failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }

    /// <summary>
    /// Create scheduled Query
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<CreateScheduledQueryResponse> CreateScheduledQuery(AwsTimeStreamScheduledQueryDto options, CancellationToken cancel)
    {
        try
        {
            var targetConfiguration = new TargetConfiguration
            {
                TimestreamConfiguration = new TimestreamConfiguration
                {
                    DatabaseName = options.DatabaseName,
                    TableName = options.TableName,
                    TimeColumn = options.TimeColumn,
                    DimensionMappings = options.Dimensions,
                    MultiMeasureMappings = new MultiMeasureMappings
                    {
                        TargetMultiMeasureName = options.TargetMultiMeasureName,
                        MultiMeasureAttributeMappings = options.MeasureAttributes
                    }
                }
            };
            
            return await _timeStreamQueryClient.CreateScheduledQueryAsync(
                new CreateScheduledQueryRequest
                {
                    Name = options.QueryName,
                    QueryString = options.ValidQuery,
                    ScheduleConfiguration = new ScheduleConfiguration
                    {
                        ScheduleExpression = options.ScheduleExpression
                    },
                    NotificationConfiguration = new NotificationConfiguration
                    {
                        SnsConfiguration = new SnsConfiguration
                        {
                            TopicArn = options.TopicArn
                        }
                    },
                    TargetConfiguration = targetConfiguration,
                    ErrorReportConfiguration = new ErrorReportConfiguration
                    {
                        S3Configuration = new S3Configuration
                        {
                            BucketName = options.S3ErrorReportBucketName
                        }
                    },
                    ScheduledQueryExecutionRoleArn = options.RoleArn
                }, cancel);

        }
        catch (Exception ex)
        {
            _logger.LogError("Scheduled Query creation failed: {Message}", ex.ToString());
            throw new AwsExceptions(ex.Message);
        }
    }
}