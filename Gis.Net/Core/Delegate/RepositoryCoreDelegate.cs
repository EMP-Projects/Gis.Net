using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Delegate;

/// <summary>
/// Represents a delegate that takes an <see cref="IQueryable{T}"/> as input and returns an <see cref="IQueryable{T}"/> as output.
/// </summary>
/// <typeparam name="T">The type of the elements in the query.</typeparam>
/// <param name="query">The input query to be processed.</param>
/// <returns>The processed query.</returns>
public delegate IQueryable<T> QueryableDelegate<T>(IQueryable<T> query) where T : ModelBase;

/// <summary>
/// Represents a delegate that takes in an IQueryable and a query parameters object, and returns an IQueryable with applied query parameters.
/// </summary>
/// <typeparam name="T">The type of the IQueryable.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters object.</typeparam>
/// <param name="query">The IQueryable to apply the query parameters to.</param>
/// <param name="queryByParams">The query parameters object.</param>
/// <returns>An IQueryable with applied query parameters.</returns>
public delegate IQueryable<T> QueryableParamsDelegate<T, in TQuery>(IQueryable<T> query, TQuery? queryByParams) where T : ModelBase where TQuery : QueryBase;

/// <summary>
/// Represents a delegate that takes an <see cref="IQueryable{T}"/> and <typeparamref name="TQuery"/> as input and returns an <see cref="IQueryable{T}"/> as output.
/// </summary>
/// <typeparam name="T">The type of the elements in the query.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters.</typeparam>
/// <param name="query">The input query to be processed.</param>
/// <param name="queryByParams">The query parameters.</param>
/// <returns>The processed query.</returns>
public delegate Task<ICollection<T>> QueryableParamsDelegateAsync<T, in TQuery>(ICollection<T> models, TQuery? queryByParams) where T : ModelBase where TQuery : QueryBase;

/// <summary>
/// Delegate for performing tasks on a single model.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <param name="model">The model to perform the task on.</param>
/// <returns>A task representing the asynchronous operation.</returns>
public delegate Task TaskModelDelegate<in TModel>(TModel model) where TModel : IModelBase;

/// <summary>
/// Delegate for performing an asynchronous task with a model and query parameters.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters.</typeparam>
/// <param name="model">The model to operate on.</param>
/// <param name="queryByParams">The query parameters.</param>
public delegate Task TaskModelWithParamsDelegate<in TModel, in TQuery>(TModel model, TQuery queryByParams) where TModel : IModelBase where TQuery : QueryBase;

/// <summary>
/// Represents a delegate that maps properties from a model to a DTO.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <param name="model">The model to be mapped to the DTO.</param>
/// <param name="dto">The DTO where the properties are mapped to.</param>
/// <remarks>
/// This delegate is used to perform extra mapping operations beyond the automatic mapping performed by the system.
/// It can be used to map additional properties or modify existing properties before returning the DTO.
/// </remarks>
public delegate void ModelToDtoExtraMapperDelegate<in TModel, in TDto>(TModel model, TDto dto) where TModel : ModelBase where TDto : DtoBase;

/// <summary>
/// Delegate for mapping a Dto to a Model with additional mapping logic.
/// </summary>
/// <typeparam name="TDto">The type of the Dto.</typeparam>
/// <typeparam name="TModel">The type of the Model.</typeparam>
/// <param name="dto">The Dto to map.</param>
/// <param name="model">The Model to map to.</param>
/// <remarks>
/// The mapping logic should be implemented in the delegate body.
/// </remarks>
public delegate void DtoToModelExtraMapperDelegate<in TDto, in TModel>(TDto dto, TModel model) where TModel : ModelBase where TDto : DtoBase;

/// <summary>
/// Represents a delegate for mapping properties from a model to a DTO asynchronously.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <param name="model">The model.</param>
/// <param name="dto">The DTO.</param>
/// <returns>A task representing the asynchronous operation.</returns>
public delegate Task ModelToDtoExtraMapperAsyncDelegate<in TModel, in TDto>(TModel model, TDto dto) where TModel : ModelBase where TDto : DtoBase;

/// <summary>
/// Represents a delegate for mapping a DTO object to a Model object asynchronously.
/// </summary>
/// <typeparam name="TDto">The type of the DTO object.</typeparam>
/// <typeparam name="TModel">The type of the Model object.</typeparam>
/// <param name="dto">The DTO object to be mapped.</param>
/// <param name="model">The Model object to be updated.</param>
/// <returns>A task representing the asynchronous mapping operation.</returns>
public delegate Task DtoToModelExtraMapperAsyncDelegate<in TDto, in TModel>(TDto dto, TModel model) where TModel : ModelBase where TDto : DtoBase;

/// <summary>
/// Represents a delegate that takes a DTO, a model, and a query as input and performs extra mapping asynchronously.
/// </summary>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <param name="dto">The DTO to be mapped.</param>
/// <param name="model">The model to be mapped to.</param>
/// <param name="queryByParams">The query parameters.</param>
public delegate Task DtoToModelExtraMapperWithParamsAsyncDelegate<in TDto, in TModel, in TQuery>(TDto dto,
    TModel model, TQuery queryByParams)
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase;
    