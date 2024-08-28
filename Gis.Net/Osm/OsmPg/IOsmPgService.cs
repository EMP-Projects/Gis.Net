using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Interface defining the service for retrieving feature collections from OpenStreetMap (OSM) data.
/// </summary>
public interface IOsmPgService
{
    /// <summary>
    /// Asynchronously retrieves a feature collection based on specified tags, geometry, and distance.
    /// </summary>
    /// <param name="geom"></param>
    /// <param name="distance"></param>
    Task<FeatureCollection> GetFeatures(Geometry geom);
}