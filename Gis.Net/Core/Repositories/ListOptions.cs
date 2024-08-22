using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

/// <summary>
/// Represents the options for retrieving a list of items from a repository.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters.</typeparam>
public class ListOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    /// <summary>
    /// Gets or sets the delegate that is invoked before executing the query.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>The modified query.</returns>
    public QueryableDelegate<TModel>? OnBeforeQuery { get; set; }

    /// <summary>
    /// Gets or sets the delegate that is called to perform sorting on a queryable collection of models.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <returns>The sorted queryable collection of models.</returns>
    public QueryableDelegate<TModel>? OnSort { get; set; }

    /// <summary>
    /// Gets or sets the delegate that is invoked to modify the sorting parameters of the query.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>The modified query.</returns>
    public QueryableParamsDelegate<TModel, TQuery>? OnSortParams { get; set; }

    /// <summary>
    /// Gets or sets the delegate that is invoked after executing the query with parameters.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>A task representing the asynchronous operation.</returns>
    public QueryableParamsDelegateAsync<TModel, TQuery>? OnAfterQueryParams { get; set; }

    /// <summary>
    /// Represents a delegate that maps properties from a model to a DTO.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <remarks>
    /// This delegate is used to perform extra mapping operations beyond the automatic mapping performed by the system.
    /// It can be used to map additional properties or modify existing properties before returning the DTO.
    /// </remarks>
    public ModelToDtoExtraMapperDelegate<TModel, TDto>? OnExtraMapping { get; set; } = null;

    /// <summary>
    /// Represents a delegate for mapping properties from a model to a DTO asynchronously.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <returns>A task representing the asynchronous operation.</returns>
    public ModelToDtoExtraMapperAsyncDelegate<TModel, TDto>? OnExtraMappingAsync { get; set; }

    /// <summary>
    /// Represents the query parameters for listing options.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query parameters.</typeparam>
    public TQuery? QueryParams { get; set; }

    /// <summary>
    /// Represents the options used for listing data from a repository.
    /// </summary>
    /// <typeparam name="TModel">The type of the model entity.</typeparam>
    /// <typeparam name="TDto">The type of the DTO (Data Transfer Object).</typeparam>
    /// <typeparam name="TQuery">The type of the query parameters.</typeparam>
    private ListOptions()
    {
    }

    /// <summary>
    /// Class representing options for listing data.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query parameters.</typeparam>
    public ListOptions(TQuery queryParams) : this() => QueryParams = queryParams;
}