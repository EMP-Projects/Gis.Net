using Gis.Net.Core.Entities;

namespace Gis.Net.Raster.Models;

public interface IGisRaster : IModelBase
{
    byte[]? Raster { get; set; }
}