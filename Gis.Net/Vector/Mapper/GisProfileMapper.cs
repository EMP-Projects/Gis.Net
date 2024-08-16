using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;

namespace Gis.Net.Vector.Mapper;

/// <summary>
/// Class providing mapping profiles specifically for GIS vector data transfer objects (DTOs), models, and requests.
/// </summary>
/// <typeparam name="TDto">The type of the GIS vector DTO.</typeparam>
/// <typeparam name="TModel">The type of the GIS vector data model.</typeparam>
/// <typeparam name="TRequest">The type of the GIS vector data request.</typeparam>
/// <typeparam name="TModelProperties"></typeparam>
/// <typeparam name="TDtoProperties"></typeparam>
public abstract class GisProfileMapper<TModel, TDto, TRequest, TModelProperties, TDtoProperties> : 
    GisProfileBaseMapper<TModel, TDto, TRequest>
    where TDto : GisVectorDto<TDtoProperties>
    where TModel : GisCoreModel<TModelProperties>
    where TRequest : GisVectorRequest
    where TModelProperties: ModelBase
    where TDtoProperties : DtoBase
{
    
}