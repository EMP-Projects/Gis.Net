using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents an interface for GIS core models that contain multiple properties.
/// </summary>
/// <typeparam name="T">The type of the model.</typeparam>
public interface IGisCoreManyModels<T> where T : ModelBase
{
    /// <summary>
    /// Represents a collection of properties for a GIS core model.
    /// </summary>
    ICollection<T>? PropertiesCollection { get; set; }
}