using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

/// Represents the properties of a GIS vector entity.
/// @typeparam T - The type of the properties DTO.
/// /
public interface IGisVectorPropertiesDto<T> where T : DtoBase
{
    /// <summary>
    /// Gets or sets the identifier of the properties associated with the GIS vector data.
    /// </summary>
    /// <remarks>
    /// This property is used to link the GIS vector data with its corresponding properties.
    /// The value of this property should be unique and meaningful within the context of the GIS vector data.
    /// </remarks>
    /// <value>The identifier of the properties associated with the GIS vector data.</value>
    long? IdProperties { get; set; }
    
    /// <summary>
    /// Represents a property of a GIS vector object.
    /// </summary>
    T? Properties { get; set; }
}