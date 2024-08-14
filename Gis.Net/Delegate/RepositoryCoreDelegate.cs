using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Delegate;

public delegate IQueryable<T> QueryableDelegate<T>(IQueryable<T> query) where T : ModelBase;
public delegate IQueryable<T> QueryableParamsDelegate<T, in TQuery>(IQueryable<T> query, TQuery? queryByParams) where T : ModelBase where TQuery : QueryBase;
public delegate Task<ICollection<T>> QueryableParamsDelegateAsync<T, in TQuery>(ICollection<T> models, TQuery? queryByParams) where T : ModelBase where TQuery : QueryBase;
public delegate T FindDelegate<out T>(long id) where T : ModelBase;
public delegate bool BooleanResultByModelDelegate<in TModel>(TModel model) where TModel : IModelBase;
public delegate Task TaskModelDelegate<in TModel>(TModel model) where TModel : IModelBase;
public delegate Task TaskModelWithParamsDelegate<in TModel, in TQuery>(TModel model, TQuery queryByParams) where TModel : IModelBase where TQuery : QueryBase;
public delegate void ModelToDtoExtraMapperDelegate<in TModel, in TDto>(TModel model, TDto dto) where TModel : ModelBase where TDto : DtoBase;
public delegate void DtoToModelExtraMapperDelegate<in TDto, in TModel>(TDto dto, TModel model) where TModel : ModelBase where TDto : DtoBase;
public delegate Task ModelToDtoExtraMapperAsyncDelegate<in TModel, in TDto>(TModel model, TDto dto) where TModel : ModelBase where TDto : DtoBase;
public delegate Task DtoToModelExtraMapperAsyncDelegate<in TDto, in TModel>(TDto dto, TModel model) where TModel : ModelBase where TDto : DtoBase;
public delegate Task DtoToModelExtraMapperWithParamsAsyncDelegate<in TDto, in TModel, in TQuery>(TDto dto,
    TModel model, TQuery queryByParams)
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase;
    