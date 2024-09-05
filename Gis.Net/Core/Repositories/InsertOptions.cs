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
    /// <seealso cref="InsertOptions{TModel, TDto, TQuery}"/>
    public TQuery? QueryParams { get; set; }

    /// <summary>
    /// Delegate for mapping a Dto to a Model with additional mapping logic.
    /// </summary>
    /// <remarks>
    /// The mapping logic should be implemented in the delegate body.
    /// </remarks>
    public DtoToModelExtraMapperDelegate<TDto, TModel>? OnExtraMapping { get; set; }

    /// <summary>
    /// Represents a delegate for mapping a DTO object to a Model object asynchronously with extra parameters.
    /// </summary>
    /// <returns>A task representing the asynchronous mapping operation.</returns>
    public DtoToModelExtraMapperAsyncDelegate<TDto, TModel>? OnExtraMappingAsync { get; set; }

    /// <summary>
    /// Represents a delegate that takes a DTO and a model as input and performs extra mapping asynchronously with query parameters.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public DtoToModelExtraMapperWithParamsAsyncDelegate<TDto, TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; }
}