using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

public class InsertOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    public TQuery? QueryParams { get; set; }
    public DtoToModelExtraMapperDelegate<TDto, TModel>? OnExtraMapping { get; set; }
    public DtoToModelExtraMapperAsyncDelegate<TDto, TModel>? OnExtraMappingAsync { get; set; }
    public DtoToModelExtraMapperWithParamsAsyncDelegate<TDto, TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; }
}