using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

/// <summary>
/// Represents the options for finding a model.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
public class FindOptions<TModel, TDto>
    where TModel : ModelBase
    where TDto : DtoBase
{
    /// <summary>
    /// Represents a delegate that performs extra mapping operations from a model to a DTO.
    /// </summary>
    /// <remarks>
    /// This delegate is used to perform extra mapping operations beyond the automatic mapping performed by the system.
    /// It can be used to map additional properties or modify existing properties before returning the DTO.
    /// </remarks>
    public ModelToDtoExtraMapperDelegate<TModel, TDto>? OnExtraMapping { get; set; } = null;

    /// <summary>
    /// Represents an asynchronous delegate for performing extra mapping operations from a model to a DTO.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public ModelToDtoExtraMapperAsyncDelegate<TModel, TDto>? OnExtraMappingAsync { get; set; }

    /// <summary>
    /// Represents a delegate that allows explicit loading of related entities for a model.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public TaskModelDelegate<TModel>? OnExplicitLoading { get; set; }
}