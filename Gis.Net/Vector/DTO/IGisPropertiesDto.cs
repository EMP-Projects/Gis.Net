using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

public interface IGisPropertiesDto<TGisDto, TDto> 
    where TGisDto : IGisVectorPropertiesDto<TDto>
    where TDto: DtoBase
{
    long GisId { get; set; }
    TGisDto? Gis { get; set; }
}