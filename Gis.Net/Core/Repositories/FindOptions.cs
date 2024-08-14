using Gis.Net.Core.Delegate;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Repositories;

public class FindOptions<TModel, TDto>
    where TModel : ModelBase
    where TDto : DtoBase
{
    public ModelToDtoExtraMapperDelegate<TModel, TDto>? OnExtraMapping { get; set; } = null;
    
    public ModelToDtoExtraMapperAsyncDelegate<TModel, TDto>? OnExtraMappingAsync { get; set; }

    public TaskModelDelegate<TModel>? OnExplicitLoading { get; set; }
}