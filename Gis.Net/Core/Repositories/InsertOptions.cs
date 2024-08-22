using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

/// <summary>
/// Specifies the options for inserting a model into a repository.
/// </summary>
/// <typeparam name="TModel">The type of model to insert.</typeparam>
/// <typeparam name="TDto">The type of DTO for the model.</typeparam>
/// <typeparam name="TQuery">The type of query parameters for the model.</typeparam>
public class InsertOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    /// <summary>
    /// Represents the query parameters for the insertion operation.
    /// </summary>
    /// <typeparam name="TQuery">The type of query parameters.</typeparam>
    /// <seealso cref="InsertOptions{TModel, TDto, TQuery}"/>
    public TQuery? QueryParams { get; set; }

    /// <summary>
    /// Delegate for mapping a Dto to a Model with additional mapping logic.
    /// </summary>
    /// <typeparam name="TDto">The type of the Dto.</typeparam>
    /// <typeparam name="TModel">The type of the Model.</typeparam>
    /// <remarks>
    /// The mapping logic should be implemented in the delegate body.
    /// </remarks>
    public DtoToModelExtraMapperDelegate<TDto, TModel>? OnExtraMapping { get; set; }

    /// <summary>
    /// Represents a delegate for mapping a DTO object to a Model object asynchronously with extra parameters.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO object.</typeparam>
    /// <typeparam name="TModel">The type of the Model object.</typeparam>
    /// <typeparam name="TQuery">The type of the Query object for additional parameters.</typeparam>
    /// <returns>A task representing the asynchronous mapping operation.</returns>
    public DtoToModelExtraMapperAsyncDelegate<TDto, TModel>? OnExtraMappingAsync { get; set; }

    /// <summary>
    /// Represents a delegate that takes a DTO and a model as input and performs extra mapping asynchronously with query parameters.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>A task representing the asynchronous operation.</returns>
    public DtoToModelExtraMapperWithParamsAsyncDelegate<TDto, TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; }
}