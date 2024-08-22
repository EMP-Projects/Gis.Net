using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a base class for DTOs containing multiple GIS vector properties.
/// </summary>
/// <typeparam name="T">The type of the properties DTO.</typeparam>
public abstract class GisVectorManyDto<T> : GisVectorDto<T>, IGisVectorPropertiesManyDto<T> where T : DtoBase
{
    /// <summary>
    /// Represents a collection of properties for GIS vector entities.
    /// </summary>
    /// <typeparam name="T">The type of the properties DTO.</typeparam>
    [JsonPropertyName("propertiesCollection")] public ICollection<T>? PropertiesCollection { get; set; }
}