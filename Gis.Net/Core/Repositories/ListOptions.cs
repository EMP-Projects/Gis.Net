using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

public class ListOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    public QueryableDelegate<TModel>? OnBeforeQuery { get; set; }

    public QueryableDelegate<TModel>? OnSort { get; set; }
    
    public QueryableParamsDelegate<TModel, TQuery>? OnSortParams { get; set; }

    public QueryableParamsDelegateAsync<TModel, TQuery>? OnAfterQueryParams { get; set; }
    
    public ModelToDtoExtraMapperDelegate<TModel, TDto>? OnExtraMapping { get; set; } = null;

    public ModelToDtoExtraMapperAsyncDelegate<TModel, TDto>? OnExtraMappingAsync { get; set; }

    public TQuery? QueryParams { get; set; }

    private ListOptions()
    {
    }

    /// <inheritdoc />
    public ListOptions(TQuery queryParams) : this() => QueryParams = queryParams;
}