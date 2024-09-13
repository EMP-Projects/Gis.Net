using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Gis.Net.Core.Repositories;

/// <summary>
/// Defines a repository interface for performing CRUD operations on a data store.
/// </summary>
/// <typeparam name="TDto">The Data Transfer Object type.</typeparam>
/// <typeparam name="TModel">The database model type.</typeparam>
/// <typeparam name="TQuery">The query parameters type.</typeparam>
/// <typeparam name="TContext"></typeparam>
public interface IRepositoryCore<TModel, TDto, TQuery, out TContext>
    where TDto : DtoBase
    where TModel : ModelBase
    where TQuery : QueryBase
    where TContext : DbContext
{
    /// <summary>
    /// Retrieves a collection of DTOs based on the provided query options.
    /// </summary>
    /// <param name="options">The options specifying the query parameters and filters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of DTOs.</returns>
    Task<ICollection<TDto>> GetRows(ListOptions<TModel, TDto, TQuery> options);
    
    /// <summary>
    /// Retrieves a collection of DTOs for the given models using the specified options.
    /// </summary>
    /// <param name="rows">The collection of models to retrieve DTOs for.</param>
    /// <param name="options">Optional query options to apply.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of DTOs.</returns>
    Task<ICollection<TDto>> GetRows(IEnumerable<TModel> rows, ListOptions<TModel, TDto, TQuery>? options = null);
    
    /// <summary>
    /// Retrieves the first DTO based on the provided query options.
    /// </summary>
    /// <param name="options">The options specifying the query parameters and filters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the DTO or null if not found.</returns>
    Task<TDto?> GetRowByFirst(ListOptions<TModel, TDto, TQuery> options);
    
    /// <summary>
    /// Retrieves the last DTO based on the provided query options.
    /// </summary>
    /// <param name="options">The options specifying the query parameters and filters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the DTO or null if not found.</returns>
    Task<TDto?> GetRowByLast(ListOptions<TModel, TDto, TQuery> options);

    /// <summary>
    /// Finds a DTO by its identifier with optional find options.
    /// </summary>
    /// <param name="id">The unique identifier for the DTO.</param>
    /// <param name="options">Optional find options to apply.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the found DTO.</returns>
    Task<TDto> Find(long id, FindOptions<TModel, TDto>? options);
    
    /// <summary>
    /// Finds a DTO by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the found DTO.</returns>
    Task<TDto> Find(long id);
    
    /// <summary>
    /// Inserts a new model asynchronously based on the provided DTO and options.
    /// </summary>
    /// <param name="dto">The DTO to insert as a new model.</param>
    /// <param name="options">The options to apply during the insert operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the newly inserted model.</returns>
    Task<TModel> InsertAsync(TDto dto, InsertOptions<TModel, TDto, TQuery> options);

    /// <summary>
    /// Updates an existing model based on the provided DTO and options.
    /// </summary>
    /// <param name="dto">The DTO containing the updated data.</param>
    /// <param name="options">The options to apply during the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated model.</returns>
    Task<TModel> Update(TDto dto, UpdateOptions<TModel, TDto, TQuery> options);

    /// <summary>
    /// Deletes a model by its identifier using the provided delete options.
    /// </summary>
    /// <param name="id">The unique identifier of the model to delete.</param>
    /// <param name="options">The options to apply during the delete operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the delete operation.</returns>
    Task<TModel> Delete(long id, DeleteOptions<TModel, TDto, TQuery> options);

    /// <summary>
    /// Deletes a model using the provided delete options, with additional parameters.
    /// </summary>
    /// <param name="model">The model to delete.</param>
    /// <param name="options">The options to apply during the delete operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the delete operation.</returns>
    Task<TModel> Delete(TModel model, DeleteOptions<TModel, TDto, TQuery> options);

    /// <summary>
    /// Persists all pending changes to the data store.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the data store.</returns>
    Task<int> SaveChanges();

    /// <summary>
    /// Creates a token for the specified identifier using the provided secret key.
    /// </summary>
    /// <param name="id">The unique identifier for which to create the token.</param>
    /// <param name="secret">The secret key used for token creation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created token or null if the operation fails.</returns>
    Task<string?> CreateToken(long id, string secret);

    /// <summary>
    /// Decodes a JWT token and deserializes it to the specified type. <seealso>
    ///     <cref>Tools.DecodeToken</cref>
    /// </seealso>
    /// </summary>
    /// <param name="token">The JWT token to decode.</param>
    /// <param name="secret">The secret key for decoding the token.</param>
    /// <returns>The deserialized object of type T from the token.</returns>
    TDto ReadToken(string token, string secret);

    /// <summary>
    /// Validates the given query parameters.
    /// </summary>
    /// <param name="queryParameters">The query parameters to validate.</param>
    /// <returns>True if the parameters are valid; otherwise, false.</returns>
    bool ValidateParameters(TQuery? queryParameters);

    /// <summary>
    /// Allows you to use the EF context for low-level operations
    /// </summary>
    /// <returns></returns>
    TContext GetDbContext();

    /// <summary>
    /// Retrieves the entity entry for the specified model.
    /// </summary>
    /// <param name="model">The model for which to retrieve the entity entry.</param>
    /// <returns>The entity entry for the specified model.</returns>
    EntityEntry<TModel> Entry(TModel model);
    
    /// <summary>
    /// Runs the database migrations asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RunMigrations();
}