using System.Diagnostics;
using Amazon.Athena;
using Amazon.Athena.Model;
using Gis.Net.Aws.AWSCore.Athena.Attributes;
using Gis.Net.Aws.AWSCore.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Gis.Net.Aws.AWSCore.Athena.Services;

/// <inheritdoc />
public class AwsAthenaService : IAwsAthenaService 
{
    private readonly IAmazonAthena _athenaClient;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AwsAthenaService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration settings.</param>
    /// <param name="athenaClient">The Amazon Athena client.</param>
    public AwsAthenaService(IConfiguration configuration, IAmazonAthena athenaClient)
    {
        _configuration = configuration;
        _athenaClient = athenaClient;
    }

    /// <inheritdoc />
    public int SleepMs { get; set; } = 1000;
 
    private async Task<StartQueryExecutionResponse> QueryResponse(string queryString)
    {
        if (_athenaClient == null || string.IsNullOrEmpty(queryString))
            throw new AmazonAthenaException("Internal error Athena client not initialized");
        
        var qry = await _athenaClient.StartQueryExecutionAsync(new StartQueryExecutionRequest
        {
            QueryString = queryString,
            ResultConfiguration = new ResultConfiguration
            {
                OutputLocation = _configuration["S3_RESULT_BUCKET_NAME"]
            }
        });
        
        if (qry is null)
            throw new AmazonAthenaException("I was unable to execute the query");
        
        return qry;
    }

    /// <summary>
    /// Execute an SQL query using Amazon Athena, wait for the result of the query 
    /// and map the result to a C# Entity object. If the Amazon Athena did not complete 
    /// processing the giving query and the timeout is reached, this method 
    /// will throw exception and return the QueryExecutionId that 
    /// can be used later to get the result
    /// </summary>
    /// <typeparam name="T">Type of the entity result</typeparam>
    /// <param name="queryString">SQL query</param>
    /// <param name="timeoutInMinutes"> default 2 minutes: timeout in minutes for the application to abort waiting.</param>
    /// <returns>The enumerator, which supports a simple iteration over a collection of a specified type</returns>
    public async Task<IEnumerable<T>?> QueryAsync<T>(string queryString, int timeoutInMinutes = 2) where T : new()
    {
        var qry = await QueryResponse(queryString);
        await WaitForQueryToComplete(qry.QueryExecutionId, timeoutInMinutes);
        return await ProcessQueryResultsAsync<T>(qry.QueryExecutionId);
    }

    /// <summary>
    /// Execute an SQL query using Amazon Athena and return QueryExecutionId 
    /// without waiting for the result, the QueryExecutionId can be used later to get the result. 
    /// </summary>
    /// <param name="queryString">SQL query</param>
    public async Task<string> QueryAndGoAsync(string queryString)
    {
        var qry = await QueryResponse(queryString);
        return qry.QueryExecutionId;
    }

    /// <summary>
    /// Retive the query result and return it as a collection of a specified type
    /// </summary>
    /// <typeparam name="T">Type of the entity result</typeparam>
    /// <param name="queryExecutionId"></param>
    /// <returns></returns>
    private async Task<IEnumerable<T>> ProcessQueryResultsAsync<T>(string queryExecutionId) where T : new()
    {
        if (_athenaClient == null || string.IsNullOrEmpty(queryExecutionId))
            throw new AmazonAthenaException("Internal error Athena client not initialized");

        var results = new List<T>();
        try
        {
            // Max Results can be set but if its not set,
            // it will choose the maximum page size
            // As of the writing of this code, the maximum value is 1000
            var getQueryResultsRequest = new GetQueryResultsRequest { QueryExecutionId = queryExecutionId };
            var getQueryResultsResults = await _athenaClient.GetQueryResultsAsync(getQueryResultsRequest);
            var columnInfoList = getQueryResultsResults.ResultSet.ResultSetMetadata.ColumnInfo;
            var rows = getQueryResultsResults.ResultSet.Rows;
            var columnPositionMap = MapColumnsPositions(rows[0].Data, columnInfoList);
            rows.RemoveAt(0);
            results.AddRange(rows.Select(row => ProcessRow<T>(row.Data, columnPositionMap)));
        }
        catch (AmazonAthenaException e)
        {
            Debug.WriteLine(e);
            throw;
        }

        return results;
    }

    /// <summary>
    /// Check if the query still running and return FALSE n case of completion. 
    /// Otherwise It will return TRUE or throw an exception in case of Failed or Cancelled
    /// </summary>
    /// <param name="queryExecutionId"></param>
    /// <returns></returns>
    public async Task<bool> IsTheQueryStillRunning(string queryExecutionId)
    {
        if (_athenaClient == null || string.IsNullOrEmpty(queryExecutionId))
            throw new AwsExceptions("Internal error Athena client not initialized");
        
        var getQueryExecutionRequest = new GetQueryExecutionRequest { QueryExecutionId = queryExecutionId };
        var isQueryStillRunning = true;
        var getQueryExecutionResponse = await _athenaClient.GetQueryExecutionAsync(getQueryExecutionRequest);
        var queryState = getQueryExecutionResponse.QueryExecution.Status.State;
        
        if (queryState == QueryExecutionState.FAILED)
            throw new AmazonAthenaException("Query Failed to run with Error Message: " + getQueryExecutionResponse.QueryExecution.Status.StateChangeReason);
        
        if (queryState == QueryExecutionState.CANCELLED)
            throw new AmazonAthenaException("Query was cancelled.");
        
        if (queryState == QueryExecutionState.SUCCEEDED)
            isQueryStillRunning = false;

        Debug.WriteLine("Current Status is: " + queryState);
        return isQueryStillRunning;
    }

    /// <summary>
    /// Wait for Amazon Athena to complete execution of the query. If It is not completed until the timeout, then It should throw exception.
    /// </summary>
    /// <param name="queryExecutionId">Execution Id to track the query progress</param>
    /// <param name="timeout">max DateTime to wait before abort</param>
    /// <returns></returns>
    private async Task WaitForQueryToComplete(string queryExecutionId, int timeout)
    {
        if (_athenaClient == null || string.IsNullOrEmpty(queryExecutionId))
            throw new AwsExceptions("Internal error Athena client not initialized");
        
        var isQueryStillRunning = true;
        var endTimeOffset = DateTimeOffset.Now.AddMinutes(timeout);

        while (isQueryStillRunning && DateTimeOffset.Now <= endTimeOffset)
        {
            isQueryStillRunning = await IsTheQueryStillRunning(queryExecutionId);
            if (isQueryStillRunning)
                // Sleep an amount of time before retrying again.
                await Task.Delay(SleepMs);
        }

        if (isQueryStillRunning && DateTimeOffset.Now > endTimeOffset)
            throw new AmazonAthenaException("Timeout: Amazon Athena still processing your query, use the RequestId to get the result later.")
            {
                RequestId = queryExecutionId
            };
    }

    /// <summary>
    /// Map the columns with tier positions on the first row and the columns Info
    /// </summary>
    /// <param name="columnsPositions">first row columns position</param>
    /// <param name="columnInfoList">columns info</param>
    /// <returns></returns>
    private static IReadOnlyDictionary<string, ColumnPositionInfo> MapColumnsPositions(IReadOnlyList<Datum>? columnsPositions, IReadOnlyList<ColumnInfo>? columnInfoList)
    {
        var result = new Dictionary<string, ColumnPositionInfo>();

        if (columnsPositions == null || columnInfoList == null)
            return result;

        for (var i = 0; i < columnsPositions.Count; i++)
        {
            var column = columnsPositions[i].VarCharValue.ToLower();
            
            result.Add(column, new ColumnPositionInfo
            {
                IndexPosition = i,
                ColumnInfo = columnInfoList.FirstOrDefault(f => f.Name.Equals(column, StringComparison.CurrentCultureIgnoreCase))
            });
        }

        return result;
    }

    /// <summary>
    /// Compute the row data to entity object
    /// </summary>
    /// <typeparam name="T">Specified entity object type</typeparam>
    /// <param name="columnsData">Collection of columns data in row</param>
    /// <param name="columnsPositionMap">Map of columns position and info for each data</param>
    /// <returns></returns>
    private T ProcessRow<T>(IReadOnlyList<Datum> columnsData, IReadOnlyDictionary<string, ColumnPositionInfo> columnsPositionMap) where T : new()
    {
        //Debug.WriteLine(string.Join(" | ", columnsData.Select(s => s.VarCharValue)));

        var entityItem = new T();

        foreach (var prop in entityItem.GetType().GetProperties())
        {
            var propColumnName = prop.Name.ToLower();
            var att = prop.GetCustomAttributes(typeof(AthenaColumnAttribute), false).FirstOrDefault();
            if (att is AthenaColumnAttribute attribute)
                propColumnName = attribute.ColumnName;

            if (!columnsPositionMap.TryGetValue(propColumnName, value: out var mapped))
                continue;
            
            var athenaColumnInfo = mapped.ColumnInfo;
            var i = mapped.IndexPosition;
            switch (athenaColumnInfo?.Type)
            {
                // For more detail about Amazon Athena Data Type, check: https://docs.aws.amazon.com/athena/latest/ug/data-types.html
                case "integer":
                case "tinyint":
                case "smallint":
                    prop.SetValue(entityItem, Convert.ToInt32(columnsData[i].VarCharValue));
                    break;
                case "bigint":
                    prop.SetValue(entityItem, Convert.ToInt64(columnsData[i].VarCharValue));
                    break;
                case "double":
                case "float":
                    prop.SetValue(entityItem, Convert.ToDouble(columnsData[i].VarCharValue));
                    break;
                case "decimal":
                    prop.SetValue(entityItem, Convert.ToDecimal(columnsData[i].VarCharValue));
                    break;
                case "date":
                case "timestamp":
                    prop.SetValue(entityItem, Convert.ToDateTime(columnsData[i].VarCharValue));
                    break;
                default:
                    prop.SetValue(entityItem, columnsData[i].VarCharValue);
                    break;
            }
        }

        return entityItem;
    }
}