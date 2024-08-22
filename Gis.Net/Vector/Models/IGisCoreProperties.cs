using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents the interface for GIS core properties.
/// </summary>
/// <typeparam name="T">The type of properties associated with the model.</typeparam>
public interface IGisCoreProperties<T> where T : ModelBase
{
    /// <summary>
    /// Gets or sets the identifier for the properties associated with the GIS.
    /// </summary>
    long IdProperties { get; set; }

    /// <summary>
    /// Gets or sets the properties associated with the GIS.
    /// </summary>
    T Properties { get; set; }
}