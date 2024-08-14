using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

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