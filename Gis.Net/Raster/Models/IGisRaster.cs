using Gis.Net.Core.Entities;

namespace Gis.Net.Raster.Models;

/// <summary>
/// Represents the interface for a GIS raster.
/// </summary>
public interface IGisRaster : IModelBase
{
    /// <summary>
    /// Represents a raster data property.
    /// </summary>
    byte[]? Raster { get; set; }
}