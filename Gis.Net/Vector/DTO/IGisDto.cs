using Gis.Net.Core.DTO;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Interfaccia che definisce le proprietà base per un Data Transfer Object (DTO) GIS.
/// </summary>
public interface IGisDto : IDtoBase
{
    /// <summary>
    /// Identificativo univoco globale (GUID) dell'oggetto.
    /// </summary>
    Guid Guid { get; set; }
    
    /// <summary>
    /// Gets or sets the geometry associated with the GIS vector data.
    /// </summary>
    /// <value>
    /// The geometry of the GIS vector data. It can hold a variety of geometric shapes like points, lines, and polygons.
    /// </value>
    Geometry? Geom { get; set; }
}