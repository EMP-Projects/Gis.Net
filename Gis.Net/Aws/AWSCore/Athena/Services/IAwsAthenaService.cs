namespace Gis.Net.Aws.AWSCore.Athena.Services;

/// <summary>
/// Interface for AWS Athena operations.
/// </summary>
/// <typeparam name="T">The type of the objects returned by the query.</typeparam>
public interface IAwsAthenaService
{
    /// <summary>
    /// Checks if the specified query is still running.
    /// </summary>
    /// <param name="queryId">The ID of the query to check.</param>
    /// <returns>True if the query is still running, otherwise false.</returns>
    Task<bool> IsTheQueryStillRunning(string queryId);
    
    /// <summary>
    /// Executes a query and does not wait for the result.
    /// </summary>
    /// <param name="queryString">The SQL query string to execute.</param>
    /// <returns>The query execution ID.</returns>
    Task<string> QueryAndGoAsync(string queryString);
    
    /// <summary>
    /// Executes a query and returns the results.
    /// </summary>
    /// <param name="queryString">The SQL query string to execute.</param>
    /// <param name="timeoutInMinutes">The timeout for the query in minutes, default is 2 minutes.</param>
    /// <returns>An IEnumerable of type T containing the query results, or null if the query times out or fails.</returns>
    Task<IEnumerable<T>?> QueryAsync<T>(string queryString, int timeoutInMinutes = 2) where T : new();
    
    /// <summary>
    /// Sleep an amount of time before retrying again
    /// </summary>
    int SleepMs { get; set; }
}