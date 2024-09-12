using Amazon.TimestreamQuery.Model;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;

namespace Gis.Net.Aws.AWSCore.TimeStream.Services;

/// <summary>
/// Interface for AWS TimeStream query service operations.
/// </summary>
public interface IAwsTimeStreamQueryService
{
    /// <summary>
    /// Runs a query asynchronously.
    /// </summary>
    /// <param name="options">The options for running the query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with a list of query results as strings.</returns>
    Task<List<string>> RunQueryAsync(AwsTimeStreamSqlDto options, CancellationToken cancel);
    
    /// <summary>
    /// Cancels a running query.
    /// </summary>
    /// <param name="options">The options for canceling the query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the CancelQuery operation.</returns>
    Task<CancelQueryResponse> CancelQuery(AwsTimeStreamSqlDto options, CancellationToken cancel);
    
    /// <summary>
    /// Lists all scheduled queries.
    /// </summary>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with a list of scheduled queries.</returns>
    Task<List<ScheduledQuery>> ListScheduledQueries(CancellationToken cancel);
    
    /// <summary>
    /// Describes a scheduled query.
    /// </summary>
    /// <param name="options">The options for describing the scheduled query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the description of the scheduled query as a string.</returns>
    Task<string> DescribeScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel);
    
    /// <summary>
    /// Executes a scheduled query.
    /// </summary>
    /// <param name="options">The options for executing the scheduled query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the ExecuteScheduledQuery operation.</returns>
    Task<ExecuteScheduledQueryResponse> ExecuteScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel);

    /// <summary>
    /// Updates a scheduled query.
    /// </summary>
    /// <param name="options">The options for updating the scheduled query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the UpdateScheduledQuery operation.</returns>
    Task<UpdateScheduledQueryResponse> UpdateScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel);

    /// <summary>
    /// Deletes a scheduled query.
    /// </summary>
    /// <param name="options">The options for deleting the scheduled query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the DeleteScheduledQuery operation.</returns>
    Task<DeleteScheduledQueryResponse> DeleteScheduledQuery(AwsTimeStreamQueryDto options, CancellationToken cancel);

    /// <summary>
    /// Creates a new scheduled query.
    /// </summary>
    /// <param name="options">The options for creating the scheduled query.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the CreateScheduledQuery operation.</returns>
    Task<CreateScheduledQueryResponse> CreateScheduledQuery(AwsTimeStreamScheduledQueryDto options, CancellationToken cancel);
}