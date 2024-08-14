using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

public class UpdateOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    public TQuery? QueryParams { get; set; }
    public DtoToModelExtraMapperDelegate<TDto, TModel>? OnExtraMapping { get; set; } = null;
    
    public DtoToModelExtraMapperAsyncDelegate<TDto, TModel>? OnExtraMappingAsync { get; set; } = null;
    
    public DtoToModelExtraMapperWithParamsAsyncDelegate<TDto, TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; } = null;
}