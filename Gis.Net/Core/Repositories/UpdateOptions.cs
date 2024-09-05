using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

/// <summary>
/// Represents the options for updating an object.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters.</typeparam>
public class UpdateOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    /// <summary>
    /// Represents the query parameters for the update operation in a repository.
    /// </summary>
    public TQuery? QueryParams { get; set; }
    
    /// <summary>
    /// Gets or sets the delegate for mapping a Dto to a Model with additional mapping logic.
    /// </summary>
    /// <remarks>
    /// The mapping logic should be implemented in this delegate.
    /// </remarks>
    public DtoToModelExtraMapperDelegate<TDto, TModel>? OnExtraMapping { get; set; } = null;

    /// <summary>
    /// Represents a delegate for mapping a DTO object to a Model object asynchronously with extra parameters.
    /// </summary>
    public DtoToModelExtraMapperAsyncDelegate<TDto, TModel>? OnExtraMappingAsync { get; set; } = null;

    /// <summary>
    /// Represents a delegate that takes a DTO, a model, and a query as input and performs extra mapping asynchronously.
    /// </summary>
    public DtoToModelExtraMapperWithParamsAsyncDelegate<TDto, TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; } = null;
}