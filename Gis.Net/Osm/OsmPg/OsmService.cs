using Gis.Net.Osm;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg;

/// <inheritdoc />
public class OsmService<T> : OsmPgService<T> where T : DbContext, IOsm2PgsqlDbContext
{
    
    private readonly IConfiguration _configuration;
    
    /// <inheritdoc />
    public OsmService(
        IOsmPg<PlanetOsmLine, T> lines, 
        IOsmPg<PlanetOsmPolygon, T> polygons, 
        IOsmPg<PlanetOsmPoint, T> points, 
        IOsmPg<PlanetOsmRoads, T> roads, 
        IConfiguration configuration) : base(lines, polygons, points, roads)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Generate OsmOptions for querying the PlanetOsmLine table based on the provided geometry.
    /// </summary>
    /// <param name="geom">The geometry used for querying.</param>
    /// <returns>The OsmOptions<PlanetOsmLine> for the query.</PlanetOsmLine></returns>
    protected override OsmOptions<PlanetOsmLine>? OsmOptionsLines(Geometry geom) => null;

    /// <summary>
    /// Get the options for querying polygon features.
    /// </summary>
    /// <param name="geom">The geometry used for querying.</param>
    /// <returns>The options for querying polygon features.</returns>
    protected override OsmOptions<PlanetOsmPolygon>? OsmOptionsPolygon(Geometry geom) => null;

    /// <summary>
    /// Determines the OSM options for querying point features based on the given geometry.
    /// </summary>
    /// <param name="geom">The geometry to query.</param>
    /// <returns>The OSM options for querying point features.</returns>
    protected override OsmOptions<PlanetOsmPoint>? OsmOptionsPoint(Geometry geom) => null;

    /// <summary>
    /// Retrieves OSM features of roads based on the specified geometry.
    /// </summary>
    /// <param name="geom">The geometry to filter the roads.</param>
    /// <returns>A <see cref="OsmOptions{PlanetOsmRoads}"/> instance containing the options for querying OSM road features.</returns>
    protected override OsmOptions<PlanetOsmRoads> OsmOptionsRoads(Geometry geom) => new()
    {
        Type = "roads",
        Geom = geom,
        SrCode = int.Parse(_configuration["Gis:SrCode"]!),
        DistanceMt = int.Parse(_configuration["Gis:Distance"]!),
        OnBeforeQuery = query => query.Where(x => x.Highway != null),
        Tags = OsmTag.Items(EOsmTag.Highway),
        OnAfterQuery = (query, tags ) => query.Where(x => tags.Contains(x.Highway))
    };
}