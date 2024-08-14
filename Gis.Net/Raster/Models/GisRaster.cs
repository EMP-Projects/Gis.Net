using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Raster.Models;

public class GisRaster : ModelBase, IGisRaster
{
    [Column("raster", TypeName = "raster")] 
    public byte[]? Raster { get; set; }
}