using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

public interface IGisVectorPropertiesDto<T> where T : DtoBase
{
    long? IdProperties { get; set; }
    T? Properties { get; set; }
}