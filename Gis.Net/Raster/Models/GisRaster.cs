using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Raster.Models;

/// <summary>
/// Represents a GIS raster.
/// </summary>
public class GisRaster : ModelBase, IGisRaster
{
    /// <summary>
    /// Represents the interface for a raster.
    /// </summary>
    [Column("raster", TypeName = "raster")] 
    public byte[]? Raster { get; set; }
}