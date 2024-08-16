using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Core.Entities;

public static class ContextDbBuilder
{

    public static ModelBuilder AddExtensionPostGis(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "pg_cron")
            .HasPostgresExtension("hstore")
            .HasPostgresExtension("pgrouting")
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("postgis_raster")
            .HasPostgresExtension("topology", "postgis_topology");
        
        return modelBuilder;
    }
    
    /// <summary>
    /// Configures the model builder for a vector core model.
    /// </summary>
    /// <param name="modelBuilder">The model builder to be configured.</param>
    /// <typeparam name="T">The type of the vector core model.</typeparam>
    /// <returns>The configured model builder.</returns>
    public static ModelBuilder SetTimeStamp<T>(this ModelBuilder modelBuilder) 
        where T : ModelBase
    {
        modelBuilder
            .Entity<T>()
            .Property(e => e.TimeStamp)
            .HasDefaultValueSql("now()");

        return modelBuilder;
    }
    
    /// <summary>
    /// Configures the model builder for core properties model with text search functionality.
    /// </summary>
    /// <param name="modelBuilder">The model builder to be configured.</param>
    /// <param name="properties">An expression specifying the properties to include in the text search.</param>
    /// <typeparam name="T">The type of the core properties model.</typeparam>
    /// <returns>The configured model builder.</returns>
    /// <remarks>
    /// This method configures the model to include a generated tsvector column for full-text search and 
    /// creates a GIN index on the SearchText property for efficient search operations.
    /// </remarks>
    public static ModelBuilder SetFullTextSearch<T>(this ModelBuilder modelBuilder, Expression<Func<T,object>> properties) 
        where T : ModelBase
    {
        modelBuilder.Entity<T>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchText!,
                "english",  // Text search config
                properties)  // Included properties
            .HasIndex(p => p.SearchText)
            .HasMethod("GIN");

        return modelBuilder;
    }
}