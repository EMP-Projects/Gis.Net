using Gis.Net.Core.Entities;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.Models;

public interface IVectorModel : IModelBase
{
    Guid Guid { get; set; }
    Geometry? Geom { get; set; }
}