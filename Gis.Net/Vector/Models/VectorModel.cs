using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.Models;

[Index(nameof(Guid), IsUnique = true)]
public class VectorModel : ModelBase, IVectorModel
{
    [Column("guid")] 
    public Guid Guid { get; set; }
    
    [Column("geom", TypeName = "geography")] 
    public required Geometry Geom { get; set; }
}
