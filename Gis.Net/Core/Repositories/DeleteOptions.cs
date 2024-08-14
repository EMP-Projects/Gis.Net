using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Delegate;

namespace Gis.Net.Core.Repositories;

/// <inheritdoc />
public class DeleteOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    public bool HardDelete { get; set; }
    
    public TaskModelDelegate<TModel>? OnBeforeDeleteAsync { get; set; } = null;
    public TQuery? QueryParams { get; set; }
    public TaskModelWithParamsDelegate<TModel, TQuery>? OnExtraMappingWithParamsAsync { get; set; }
}