using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents a generic abstract class for a GIS core model with multiple properties.
/// </summary>
/// <typeparam name="T">The type that inherits from ModelBase.</typeparam>
public abstract class GisCoreManyModel<T> : VectorModel, IGisCoreManyModels<T> where T : ModelBase
{
    /// <inheritdoc />
    public ICollection<T>? PropertiesCollection { get; set; }
}