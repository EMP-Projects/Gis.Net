using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Represents a DbContext for Osm2Pgsql.
/// </summary>
public class Osm2PgsqlDbContext : DbContext, IOsm2PgsqlDbContext
{
    /// <summary>
    /// Represents a property used by Osm2Pgsql.
    /// </summary>
    public DbSet<Osm2PgsqlProperty>? Osm2PgsqlProperties { get; set; }
    
    /// <summary>
    /// Represents a line geometry in the planet_osm_line table of the Osm2Pgsql database.
    /// </summary>
    public DbSet<PlanetOsmLine>? PlanetOsmLine { get; set; }
    
    /// <summary>
    /// Represents a node in the Planet OSM data model.
    /// </summary>
    public DbSet<PlanetOsmNode>? PlanetOsmNode { get; set; }
    
    /// <summary>
    /// Represents a point feature in the planet_osm_point table.
    /// </summary>
    public DbSet<PlanetOsmPoint>? PlanetOsmPoint { get; set; }
    
    /// <summary>
    /// Represents a polygon feature in the Planet OSM dataset.
    /// </summary>
    public DbSet<PlanetOsmPolygon>? PlanetOsmPolygon { get; set; }
    
    /// <summary>
    /// Represents the PlanetOsmRels table in the OsmPg database.
    /// </summary>
    public DbSet<PlanetOsmRels>? PlanetOsmRels { get; set; }
    
    /// <summary>
    /// Represents the PlanetOsmRoads table in the Osm2PgsqlDbContext.
    /// </summary>
    public DbSet<PlanetOsmRoads>? PlanetOsmRoads { get; set; }
    
    /// <summary>
    /// Represents a class that defines the structure of the "planet_osm_ways" table in the OsmPg database.
    /// </summary>
    public DbSet<PlanetOsmWays>? PlanetOsmWays { get; set; }

    /// <summary>
    /// Represents the database context for Osm2Pgsql.
    /// </summary>
    public Osm2PgsqlDbContext(DbContextOptions<Osm2PgsqlDbContext> options) : base(options) {}

    /// <summary>
    /// This method is called by the framework when creating the database model for the Osm2PgsqlDbContext. It can be used to configure the model using fluent API or Data Annotations.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance that the framework uses to create the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // OpenStreet Map database configuration
        modelBuilder.OsmDbConfig();
    }
    
}