using Amazon.TimestreamWrite.Model;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.TimeStream.Dto;

namespace Gis.Net.Aws.AWSCore.TimeStream.Services;

/// <summary>
/// Interface for interacting with the AWS TimeStream database service.
/// </summary>
public interface IAwsTimeStreamDatabaseService
{
    /// <summary>
    /// Create a database.
    /// </summary>
    /// <param name="options">The options for creating the database.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the response of the create operation.</returns>
    /// <exception cref="AwsExceptions">Thrown when an error occurs while creating the database.</exception>
    Task<CreateDatabaseResponse> CreateDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel);

    /// <summary>
    /// Retrieves information about a database in AWS Timestream.
    /// </summary>
    /// <param name="options">The options for describing the database.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the response of the describe operation.</returns>
    /// <exception cref="ResourceNotFoundException">Thrown when the specified database is not found.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while describing the database.</exception>
    Task<DescribeDatabaseResponse> DescribeDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel);

    /// <summary>
    /// Updates a database in AWS Timestream.
    /// </summary>
    /// <param name="options">The options for updating the database.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response of the update operation.</returns>
    /// <exception cref="ResourceNotFoundException">Thrown when the specified database is not found.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while updating the database.</exception>
    Task<UpdateDatabaseResponse> UpdateDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel);

    /// <summary>
    /// Deletes a database in AWS Timestream.
    /// </summary>
    /// <param name="options">The options for deleting the database.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response of the delete operation.</returns>
    /// <exception cref="AwsExceptions">Thrown when an error occurs while deleting the database.</exception>
    Task<DeleteDatabaseResponse> DeleteDatabase(AwsTimeStreamDatabaseDto options, CancellationToken cancel);

    /// <summary>
    /// Lists the databases.
    /// </summary>
    /// <param name="options">The options for listing databases.</param>
    /// <param name="cancel">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of databases.</returns>
    Task<List<Database>> ListDatabases(AwsTimeStreamDatabaseRootDto options, CancellationToken cancel);
}