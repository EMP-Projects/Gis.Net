using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;

namespace Gis.Net.Vector.Mapper;

/// <summary>
/// A base class for mapping multiple GIS profiles.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO (Data Transfer Object).</typeparam>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TModelProperties">The type of the model properties.</typeparam>
/// <typeparam name="TDtoProperties">The type of the DTO properties.</typeparam>
public abstract class GisProfileManyMapper<TModel, TDto, TRequest, TModelProperties, TDtoProperties> :
    GisProfileBaseMapper<TModel, TDto, TRequest>
    where TDto : GisVectorManyDto<TDtoProperties>
    where TModel : GisCoreManyModel<TModelProperties>
    where TRequest : GisVectorRequest
    where TModelProperties: ModelBase
    where TDtoProperties : DtoBase
{
    
}