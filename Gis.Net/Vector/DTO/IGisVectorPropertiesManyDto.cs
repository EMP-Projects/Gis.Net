using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

/// Provides the interface for a collection of GIS vector properties.
/// @typeparam T - The type of the properties DTO.
/// /
public interface IGisVectorPropertiesManyDto<T> where T : DtoBase
{
    /// <summary>
    /// Represents a collection of properties for a GIS vector entity.
    /// </summary>
    ICollection<T>? PropertiesCollection { get; set; }
}