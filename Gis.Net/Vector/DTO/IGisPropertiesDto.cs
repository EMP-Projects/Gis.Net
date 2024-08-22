using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents the properties of a GIS entity.
/// </summary>
/// <typeparam name="TGisDto">The type of the GIS entity.</typeparam>
/// <typeparam name="TDto">The type of the properties DTO.</typeparam>
public interface IGisPropertiesDto<TGisDto, TDto> 
    where TGisDto : IGisVectorPropertiesDto<TDto>
    where TDto: DtoBase
{
    /// <summary>
    /// Represents the GIS ID of a vector entity.
    /// </summary>
    /// <remarks>
    /// The GIS ID is a unique identifier assigned to a GIS vector entity.
    /// </remarks>
    long GisId { get; set; }
    
    /// <summary>
    /// Represents the properties of a GIS entity.
    /// </summary>
    /// <typeparam name="TGisDto">The type of the GIS vector properties DTO.</typeparam>
    /// <typeparam name="TDto">The type of the properties DTO.</typeparam>
    TGisDto? Gis { get; set; }
}