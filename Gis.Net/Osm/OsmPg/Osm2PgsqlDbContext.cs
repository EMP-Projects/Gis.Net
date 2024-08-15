using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

public class Osm2PgsqlDbContext : DbContext, IOsm2PgsqlDbContext
{
    public DbSet<Osm2PgsqlProperty>? Osm2PgsqlProperties { get; set; }
    public DbSet<PlanetOsmLine>? PlanetOsmLine { get; set; }
    public DbSet<PlanetOsmNode>? PlanetOsmNode { get; set; }
    public DbSet<PlanetOsmPoint>? PlanetOsmPoint { get; set; }
    public DbSet<PlanetOsmPolygon>? PlanetOsmPolygon { get; set; }
    public DbSet<PlanetOsmRels>? PlanetOsmRels { get; set; }
    public DbSet<PlanetOsmRoads>? PlanetOsmRoads { get; set; }
    public DbSet<PlanetOsmWays>? PlanetOsmWays { get; set; }

    /// <inheritdoc />
    public Osm2PgsqlDbContext(DbContextOptions<Osm2PgsqlDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configurazione del database di OpenStreet Map
        modelBuilder.OsmDbConfig();
    }
    
}