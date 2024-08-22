using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a geometry model for OSM nodes, ways, and relations with PostGIS geometries.
/// </summary>
public interface IOsmPgGeometryModel : IOsmPgModel
{
    /// <summary>
    /// Represents the Way property of an OsmPgGeometryModel.
    /// </summary>
    Geometry? Way { get; set; }
}