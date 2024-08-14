using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

public interface IGisVectorPropertiesManyDto<T> where T : DtoBase
{
    ICollection<T>? PropertiesCollection { get; set; }
}