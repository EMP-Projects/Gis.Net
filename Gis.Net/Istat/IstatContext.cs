using Gis.Net.Istat.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Istat;

/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext" />
public partial class IstatContext : DbContext, IStatDbContext
{
    /// <inheritdoc />
    public IstatContext(DbContextOptions<IstatContext> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    public virtual DbSet<LimitsItMunicipality> LimitsItMunicipalities { get; set; }

    /// <inheritdoc />
    public virtual DbSet<LimitsItProvince> LimitsItProvinces { get; set; }

    /// <inheritdoc />
    public virtual DbSet<LimitsItRegion> LimitsItRegions { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("fuzzystrmatch")
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("tiger", "postgis_tiger_geocoder")
            .HasPostgresExtension("topology", "postgis_topology");

        modelBuilder.Entity<LimitsItMunicipality>(entity =>
        {
            entity.HasKey(e => e.OgcFid).HasName("limits_it_municipalities_pkey");

            entity.ToTable("limits_it_municipalities");

            entity.HasIndex(e => e.WkbGeometry, "limits_it_municipalities_wkb_geometry_geom_idx").HasMethod("gist");

            entity.Property(e => e.OgcFid).HasColumnName("ogc_fid");
            entity.Property(e => e.ComCatastoCode)
                .HasColumnType("character varying")
                .HasColumnName("com_catasto_code");
            entity.Property(e => e.ComIstatCode)
                .HasColumnType("character varying")
                .HasColumnName("com_istat_code");
            entity.Property(e => e.ComIstatCodeNum).HasColumnName("com_istat_code_num");
            entity.Property(e => e.MinintElettorale)
                .HasColumnType("character varying")
                .HasColumnName("minint_elettorale");
            entity.Property(e => e.MinintFinloc)
                .HasColumnType("character varying")
                .HasColumnName("minint_finloc");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.NameDe)
                .HasColumnType("character varying")
                .HasColumnName("name_de");
            entity.Property(e => e.NameIt)
                .HasColumnType("character varying")
                .HasColumnName("name_it");
            entity.Property(e => e.NameSl)
                .HasColumnType("character varying")
                .HasColumnName("name_sl");
            entity.Property(e => e.OpId)
                .HasColumnType("character varying")
                .HasColumnName("op_id");
            entity.Property(e => e.OpdmId)
                .HasColumnType("character varying")
                .HasColumnName("opdm_id");
            entity.Property(e => e.ProvAcr)
                .HasColumnType("character varying")
                .HasColumnName("prov_acr");
            entity.Property(e => e.ProvIstatCode)
                .HasColumnType("character varying")
                .HasColumnName("prov_istat_code");
            entity.Property(e => e.ProvIstatCodeNum).HasColumnName("prov_istat_code_num");
            entity.Property(e => e.ProvName)
                .HasColumnType("character varying")
                .HasColumnName("prov_name");
            entity.Property(e => e.RegIstatCode)
                .HasColumnType("character varying")
                .HasColumnName("reg_istat_code");
            entity.Property(e => e.RegIstatCodeNum).HasColumnName("reg_istat_code_num");
            entity.Property(e => e.RegName)
                .HasColumnType("character varying")
                .HasColumnName("reg_name");
            entity.Property(e => e.WkbGeometry)
                .HasColumnType("geometry(MultiPolygon,3857)")
                .HasColumnName("wkb_geometry");
        });

        modelBuilder.Entity<LimitsItProvince>(entity =>
        {
            entity.HasKey(e => e.OgcFid).HasName("limits_it_provinces_pkey");

            entity.ToTable("limits_it_provinces");

            entity.HasIndex(e => e.WkbGeometry, "limits_it_provinces_wkb_geometry_geom_idx").HasMethod("gist");

            entity.Property(e => e.OgcFid).HasColumnName("ogc_fid");
            entity.Property(e => e.ProvAcr)
                .HasColumnType("character varying")
                .HasColumnName("prov_acr");
            entity.Property(e => e.ProvIstatCode)
                .HasColumnType("character varying")
                .HasColumnName("prov_istat_code");
            entity.Property(e => e.ProvIstatCodeNum).HasColumnName("prov_istat_code_num");
            entity.Property(e => e.ProvName)
                .HasColumnType("character varying")
                .HasColumnName("prov_name");
            entity.Property(e => e.RegIstatCode)
                .HasColumnType("character varying")
                .HasColumnName("reg_istat_code");
            entity.Property(e => e.RegIstatCodeNum).HasColumnName("reg_istat_code_num");
            entity.Property(e => e.RegName)
                .HasColumnType("character varying")
                .HasColumnName("reg_name");
            entity.Property(e => e.WkbGeometry)
                .HasColumnType("geometry(Geometry,3857)")
                .HasColumnName("wkb_geometry");
        });

        modelBuilder.Entity<LimitsItRegion>(entity =>
        {
            entity.HasKey(e => e.OgcFid).HasName("limits_it_regions_pkey");

            entity.ToTable("limits_it_regions");

            entity.HasIndex(e => e.WkbGeometry, "limits_it_regions_wkb_geometry_geom_idx").HasMethod("gist");

            entity.Property(e => e.OgcFid).HasColumnName("ogc_fid");
            entity.Property(e => e.RegIstatCode)
                .HasColumnType("character varying")
                .HasColumnName("reg_istat_code");
            entity.Property(e => e.RegIstatCodeNum).HasColumnName("reg_istat_code_num");
            entity.Property(e => e.RegName)
                .HasColumnType("character varying")
                .HasColumnName("reg_name");
            entity.Property(e => e.WkbGeometry)
                .HasColumnType("geometry(Geometry,3857)")
                .HasColumnName("wkb_geometry");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
