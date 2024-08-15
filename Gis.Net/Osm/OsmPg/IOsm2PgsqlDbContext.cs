using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

public interface IOsm2PgsqlDbContext
{
    DbSet<Osm2PgsqlProperty> Osm2PgsqlProperties { get; set; }

    DbSet<PlanetOsmLine> PlanetOsmLine { get; set; }

    DbSet<PlanetOsmNode> PlanetOsmNode { get; set; }

    DbSet<PlanetOsmPoint> PlanetOsmPoint { get; set; }

    DbSet<PlanetOsmPolygon> PlanetOsmPolygon { get; set; }

    DbSet<PlanetOsmRels> PlanetOsmRels { get; set; }

    DbSet<PlanetOsmRoads> PlanetOsmRoads { get; set; }

    DbSet<PlanetOsmWays> PlanetOsmWays { get; set; }
}