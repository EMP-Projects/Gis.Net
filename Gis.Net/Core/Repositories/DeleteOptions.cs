using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

/// <summary>
/// Represents the options for deleting a model from a repository.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters.</typeparam>
public class DeleteOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    /// <summary>
    /// Represents a delegate that is invoked before deleting a model asynchronously.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <returns>A task representing the asynchronous operation.</returns>
    public TaskModelDelegate<TModel>? OnBeforeDeleteAsync { get; set; } = null;
    
    /// <summary>
    /// Represents the query parameters for the delete operation.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query parameters.</typeparam>
    public TQuery? QueryParams { get; set; }

    /// <summary>
    /// Delegate for performing an asynchronous task with a model and query parameters.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TQuery">The type of the query parameters.</typeparam>
    public TaskModelWithParamsDelegate<TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; }
}