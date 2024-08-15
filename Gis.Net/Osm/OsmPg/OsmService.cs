using Gis.Net.Osm;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg;

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

    protected override OsmOptions<PlanetOsmLine>? OsmOptionsLines(Geometry geom) => null;

    protected override OsmOptions<PlanetOsmPolygon>? OsmOptionsPolygon(Geometry geom) => null;

    protected override OsmOptions<PlanetOsmPoint>? OsmOptionsPoint(Geometry geom) => null;

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