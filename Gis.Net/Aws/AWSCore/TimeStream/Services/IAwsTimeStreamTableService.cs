using Amazon.TimestreamWrite.Model;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;

namespace Gis.Net.Aws.AWSCore.TimeStream.Services;

/// <summary>
/// Interface for AWS TimeStream table service operations.
/// </summary>
public interface IAwsTimeStreamTableService
{
    /// <summary>
    /// Creates a new TimeStream table.
    /// </summary>
    /// <param name="options">The options for creating the table.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the CreateTable operation.</returns>
    Task<CreateTableResponse> CreateTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// Describes an existing TimeStream table.
    /// </summary>
    /// <param name="options">The options for describing the table.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the DescribeTable operation.</returns>
    Task<DescribeTableResponse> DescribeTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// Updates an existing TimeStream table.
    /// </summary>
    /// <param name="options">The options for updating the table.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the UpdateTable operation.</returns>
    Task<UpdateTableResponse> UpdateTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// Deletes an existing TimeStream table.
    /// </summary>
    /// <param name="options">The options for deleting the table.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the DeleteTable operation.</returns>
    Task<DeleteTableResponse> DeleteTable(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// Lists all TimeStream tables.
    /// </summary>
    /// <param name="options">The options for listing the tables.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with a list of tables.</returns>
    Task<List<Table>> ListTables(AwsTimeStreamTableDto options, CancellationToken cancel);
    
    /// <summary>
    /// Writes records to a TimeStream table.
    /// </summary>
    /// <param name="options">The options for writing the records.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the response from the WriteRecords operation.</returns>
    Task<WriteRecordsResponse> WriteRecords(AwsTimeStreamRecordsDto options, CancellationToken cancel);
}