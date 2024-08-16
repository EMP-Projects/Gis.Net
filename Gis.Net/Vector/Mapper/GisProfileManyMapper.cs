using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;

namespace Gis.Net.Vector.Mapper;

public abstract class GisProfileManyMapper<TModel, TDto, TRequest, TModelProperties, TDtoProperties> : 
    GisProfileBaseMapper<TModel, TDto, TRequest>
    where TDto : GisVectorManyDto<TDtoProperties>
    where TModel : GisCoreManyModel<TModelProperties>
    where TRequest : GisVectorRequest
    where TModelProperties: ModelBase
    where TDtoProperties : DtoBase
{
    
}