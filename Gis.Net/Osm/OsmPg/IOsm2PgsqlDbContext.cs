using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

/// Represents the interface for the Osm2Pgsql database context.
/// /
public interface IOsm2PgsqlDbContext
{
    /// <summary>
    /// Represents a property used by Osm2Pgsql.
    /// </summary>
    DbSet<Osm2PgsqlProperty> Osm2PgsqlProperties { get; set; }

    /// <summary>
    /// Represents a line object in the Planet OSM database.
    /// </summary>
    DbSet<PlanetOsmLine> PlanetOsmLine { get; set; }

    /// <summary>
    /// Represents a node in the Planet OSM data model.
    /// </summary>
    DbSet<PlanetOsmNode> PlanetOsmNode { get; set; }

    /// <summary>
    /// Represents a point geometry from the planet_osm_point table in the OSM PostgreSQL database.
    /// </summary>
    DbSet<PlanetOsmPoint> PlanetOsmPoint { get; set; }

    /// <summary>
    /// Represents a polygon in the Planet OSM database.
    /// </summary>
    DbSet<PlanetOsmPolygon> PlanetOsmPolygon { get; set; }

    /// <summary>
    /// Represents the PlanetOsmRels table in the OsmPg database.
    /// </summary>
    DbSet<PlanetOsmRels> PlanetOsmRels { get; set; }

    /// <summary>
    /// Represents the table "planet_osm_roads" in the Osm2Pgsql database.
    /// </summary>
    DbSet<PlanetOsmRoads> PlanetOsmRoads { get; set; }

    /// <summary>
    /// Represents a class that defines the structure of the "planet_osm_ways" table in the OsmPg database.
    /// </summary>
    DbSet<PlanetOsmWays> PlanetOsmWays { get; set; }
}