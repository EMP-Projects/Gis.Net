using Gis.Net.Core.Entities;
using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

public static class OsmDbManager
{
    public static ModelBuilder OsmDbConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.AddExtensionPostGis();

        modelBuilder.Entity<Osm2PgsqlProperty>(entity => entity.HasKey(e => e.Property).HasName("osm2pgsql_properties_pkey"));
        modelBuilder.Entity<PlanetOsmLine>(entity => entity.HasIndex(e => e.Way, "planet_osm_line_way_idx").HasMethod("gist"));

        modelBuilder.Entity<PlanetOsmNode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planet_osm_nodes_pkey");
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PlanetOsmPoint>(entity => entity.HasIndex(e => e.Way, "planet_osm_point_way_idx").HasMethod("gist"));
        modelBuilder.Entity<PlanetOsmPolygon>(entity => entity.HasIndex(e => e.Way, "planet_osm_polygon_way_idx").HasMethod("gist"));

        modelBuilder.Entity<PlanetOsmRels>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planet_osm_rels_pkey");
            entity.HasIndex(e => e.Parts, "planet_osm_rels_parts_idx")
                .HasMethod("gin")
                .HasAnnotation("Npgsql:StorageParameter:fastupdate", "off");
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PlanetOsmRoads>(entity => entity.HasIndex(e => e.Way, "planet_osm_roads_way_idx").HasMethod("gist"));

        modelBuilder.Entity<PlanetOsmWays>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planet_osm_ways_pkey");
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        return modelBuilder;
    }
}