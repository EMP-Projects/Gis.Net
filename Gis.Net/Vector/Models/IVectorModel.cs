using Gis.Net.Core.Entities;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents the base interface for all vector models in the system.
/// </summary>
public interface IVectorModel : IModelBase
{
    /// <summary>
    /// Represents a unique identifier for an object.
    /// </summary>
    /// <remarks>
    /// The Guid struct represents a globally unique identifier (UUID) consisting of 128 bits.
    /// A Guid instance contains 32 hexadecimal digits that are typically displayed in five groups
    /// separated by hyphens, such as "c9a646d3-9c61-4cb7-bfcd-ee2522c8f633". The hexadecimal digits
    /// can range from 0 to 9 and from A to F.
    /// </remarks>
    Guid Guid { get; set; }
    
    /// <summary>
    /// Gets or sets the geometry object.
    /// </summary>
    Geometry? Geom { get; set; }
}