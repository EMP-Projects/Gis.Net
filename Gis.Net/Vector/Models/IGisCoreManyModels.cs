using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

public interface IGisCoreManyModels<T> where T : ModelBase
{
    ICollection<T>? PropertiesCollection { get; set; }
}