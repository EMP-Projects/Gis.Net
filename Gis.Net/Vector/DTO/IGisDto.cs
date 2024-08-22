using Gis.Net.Core.DTO;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents the base interface for GIS Data Transfer Objects (DTOs).
/// </summary>
public interface IGisDto : IDtoBase
{

    /// <summary>
    /// Represents a globally unique identifier (GUID).
    /// </summary>
    Guid Guid { get; set; }

    /// <summary>
    /// Represents a GIS Data Transfer Object (DTO) with geometry information.
    /// </summary>
    Geometry? Geom { get; set; }
}