using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg.Models;

public interface IOsmPgGeometryModel : IOsmPgModel
{
    Geometry? Way { get; set; }
}