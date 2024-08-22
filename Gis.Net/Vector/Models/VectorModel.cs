using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents a vector model that contains geometric data.
/// </summary>
[Index(nameof(Guid), IsUnique = true)]
public class VectorModel : ModelBase, IVectorModel
{
    /// <summary>
    /// Represents the unique identifier (GUID) of a vector model.
    /// </summary>
    [Column("guid")] 
    public Guid Guid { get; set; }

    /// <summary>
    /// Represents the Geom property of a VectorModel.
    /// </summary>
    /// <remarks>
    /// This property stores the geometry information associated with a VectorModel.
    /// </remarks>
    [Column("geom", TypeName = "geography")] 
    public required Geometry Geom { get; set; }
}
