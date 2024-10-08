using AutoMapper;
using Gis.Net.Vector.DTO;

namespace Gis.Net.Vector.Mapper;

/// <summary>
/// Base class for mapping GIS Vector Model to GIS Vector DTO and vice versa.
/// </summary>
/// <typeparam name="TModel">The type of the GIS Vector Model.</typeparam>
/// <typeparam name="TDto">The type of the GIS Vector DTO.</typeparam>
/// <typeparam name="TRequest">The type of the GIS Vector Request.</typeparam>
public abstract class GisProfileBaseMapper<TModel, TDto, TRequest> : Profile
    where TDto : GisVectorDto
    where TModel : Models.VectorModel
    where TRequest : GisVectorRequest
{
    /// <summary>
    /// Mapping expression from GIS Vector Model to GIS Vector DTO.
    /// </summary>
    protected IMappingExpression<TModel, TDto> GisVectorModelToDtoMapper;

    /// <summary>
    /// Mapping expression from GIS Vector DTO to GIS Vector Model.
    /// </summary>
    protected IMappingExpression<TDto, TModel> GisVectorDtoToModelMapper;

    /// <summary>
    /// Mapping expression from GIS Vector Request to GIS Vector DTO.
    /// </summary>
    protected IMappingExpression<TRequest, TDto> GisVectorRequestToDtoMapper;

    /// <summary>
    /// Initializes a new instance of the GisVectorProfileMapper class.
    /// </summary>
    protected GisProfileBaseMapper()
    {
        // Map from GIS Vector Model to DTO with default mapping.
        GisVectorModelToDtoMapper = CreateMap<TModel, TDto>();
        GisVectorDtoToModelMapper = CreateMap<TDto, TModel>();

        GisVectorDtoToModelMapper?.ForAllMembers(o
            => o.Condition((src, dest, sourceMbr, targetMbr) => sourceMbr is not null));

        // Map from GIS Vector Request to DTO and configure the geometry field.
        GisVectorRequestToDtoMapper = CreateMap<TRequest, TDto>()
            .ForMember(dest => dest.Geom, // Map the geometry field.
                opt => opt.MapFrom(src 
                    => GisUtility.CreateGeometryFromFilter(src.Geometry!, null))); // Use GisUtility to create the geometry from the request filter.
    }
}