using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Core.Services;

/// <summary>
/// Defines a set of service operations for managing data transfer objects (DTOs) and corresponding models with query parameters.
/// </summary>
/// <typeparam name="TDto">The type of data transfer object.</typeparam>
/// <typeparam name="TModel">The type of model that represents the entity.</typeparam>
/// <typeparam name="TQuery">The type of query parameters used for filtering data.</typeparam>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TContext"></typeparam>
public interface IServiceCore<TModel, TDto, TQuery, in TRequest, out TContext>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
    where TRequest : RequestBase
    where TContext : DbContext
{
    /// <summary>
    /// Retrieves a collection of DTOs based on the specified query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters used to filter the results.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of DTOs.</returns>
    Task<ICollection<TDto>> List(TQuery? queryParams);
    
    /// <summary>
    /// Finds a DTO by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the DTO to find.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the found DTO, if any.</returns>
    Task<TDto> Find(long id);

    /// <summary>
    /// Inserts a new model based on the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO used to create a new model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the inserted model, if the operation was successful.</returns>
    Task<TModel?> Insert(TDto dto);
    
    /// <summary>
    /// Updates an existing model based on the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO used to update an existing model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated model, if the operation was successful.</returns>
    Task<TModel?> Update(TDto dto);
    
    /// <summary>
    /// Finds a security token based on the model's identifier and a secret key.
    /// </summary>
    /// <param name="id">The identifier of the model.</param>
    /// <param name="secret">The secret key used for token generation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the security token, if found.</returns>
    Task<string?> FindToken(long id, string secret);
    
    /// <summary>
    /// Deletes a model by its identifier, with an option to force delete.
    /// </summary>
    /// <param name="id">The identifier of the model to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the delete operation.</returns>
    Task<TModel> Delete(long id);

    /// <summary>
    /// Persists all changes made in the data context.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveContext(TModel model, ECrudActions crudAction);

    /// <summary>
    /// Saves the changes made to the context.
    /// </summary>
    /// <param name="model">The model to be saved.</param>
    /// <param name="crudAction">The CRUD action performed on the model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates the number of state entries written to the database.</returns>
    Task<int> SaveContext();
    
    /// <summary>
    /// Validates the given DTO based on the specified CRUD operation.
    /// </summary>
    /// <param name="dto">The DTO to validate.</param>
    /// <param name="crudEnum">The CRUD operation for which validation is performed.</param>
    /// <returns>A task representing the asynchronous validation operation.</returns>
    Task Validate(TDto dto, ECrudActions crudEnum);

    /// <summary>
    /// Validates query parameters sent by the client for filtering a list of results. The default implementation does nothing.
    /// In case of an error, the service implementing this method should throw the corresponding exception.
    /// </summary>
    /// <param name="queryParams">The DTO containing query string parameters to validate.</param>
    /// <returns>A task representing the asynchronous validation operation.</returns>
    Task ValidateQueryParams(TQuery queryParams);
    
    /// <summary>
    /// Gets the repository associated with the service.
    /// </summary>
    /// <returns>The repository instance.</returns>
    IRepositoryCore<TModel, TDto, TQuery, TContext> GetRepository();

    /// <summary>
    /// Validates the given request based on the specified CRUD action.
    /// </summary>
    /// <param name="request">The request to be validated.</param>
    /// <param name="crudAction">The CRUD action to be performed.</param>
    /// <returns>A task that represents the asynchronous validation operation.</returns>
    Task ValidateRequest(TRequest request, ECrudActions crudAction);
}