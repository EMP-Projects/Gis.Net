using Gis.Net.Osm;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg;

/// <inheritdoc />
public abstract class OsmService<T> : OsmPgService<T> where T : DbContext, IOsm2PgsqlDbContext
{
    
    /// <inheritdoc />
    protected OsmService(
        IOsmPg<PlanetOsmLine, T> lines, 
        IOsmPg<PlanetOsmPolygon, T> polygons, 
        IOsmPg<PlanetOsmPoint, T> points, 
        IOsmPg<PlanetOsmRoads, T> roads) : base(lines, polygons, points, roads)
    {
        
    }

    /// <summary>
    /// Generate OsmOptions for querying the PlanetOsmLine table based on the provided geometry.
    /// </summary>
    /// <param name="geom">The geometry used for querying.</param>
    /// <returns>The OsmOptions<PlanetOsmLine> for the query.</PlanetOsmLine></returns>
    protected override OsmOptions<PlanetOsmLine> OsmOptionsLines(Geometry geom) => new()
    {
        Type = "lines",
        Geom = geom,
        SrCode = 3857,
        DistanceMt = 100,
        Tags = []
    };

    /// <summary>
    /// Get the options for querying polygon features.
    /// </summary>
    /// <param name="geom">The geometry used for querying.</param>
    /// <returns>The options for querying polygon features.</returns>
    protected override OsmOptions<PlanetOsmPolygon> OsmOptionsPolygon(Geometry geom) => new()
    {
        Type = "polygons",
        Geom = geom,
        SrCode = 3857,
        DistanceMt = 100,
        Tags = []
    };

    /// <summary>
    /// Determines the OSM options for querying point features based on the given geometry.
    /// </summary>
    /// <param name="geom">The geometry to query.</param>
    /// <returns>The OSM options for querying point features.</returns>
    protected override OsmOptions<PlanetOsmPoint> OsmOptionsPoint(Geometry geom) => new()
    {
        Type = "points",
        Geom = geom,
        SrCode = 3857,
        DistanceMt = 100,
        Tags = []
    };

    /// <summary>
    /// Retrieves OSM features of roads based on the specified geometry.
    /// </summary>
    /// <param name="geom">The geometry to filter the roads.</param>
    /// <returns>A <see cref="OsmOptions{PlanetOsmRoads}"/> instance containing the options for querying OSM road features.</returns>
    protected override OsmOptions<PlanetOsmRoads> OsmOptionsRoads(Geometry geom) => new()
    {
        Type = "roads",
        Geom = geom,
        SrCode = 3857,
        DistanceMt = 100,
        Tags = []
    };
}