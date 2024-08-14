using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

public abstract class GisVectorManyDto<T> : GisVectorDto<T>, IGisVectorPropertiesManyDto<T> where T : DtoBase
{
    [JsonPropertyName("propertiesCollection")] public ICollection<T>? PropertiesCollection { get; set; }
}