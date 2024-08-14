using NetTopologySuite.Geometries;
namespace Gis.Net.OsmPg.Models;

public interface IOsmPgGeometryModel : IOsmPgModel
{
    Geometry? Way { get; set; }
}