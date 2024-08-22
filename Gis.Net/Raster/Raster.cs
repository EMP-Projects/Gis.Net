using NetTopologySuite.Geometries;

namespace Gis.Net.Raster;

/// <summary>
/// Provides utility methods for working with raster data.
/// </summary>
public static class Raster
{
    /// <summary>
    /// Determines the SQL spatial function based on the type of geometry and returns the SQL query string.
    /// </summary>
    /// <param name="geom">The geometry to compare.</param>
    /// <returns>The SQL spatial function.</returns>
    private static string SqlFunctionSpatialByGeometry(Geometry geom)
    {
        var sqlSpatial = geom!.GeometryType.ToUpper().Equals("POINT") 
            ? "ST_Within"
            : "ST_Intersects";
        return sqlSpatial;
    }

    /// <summary>
    /// Takes a table name, a geometry, and an optional schema and returns an SQL query that selects the id, key, and intersecting geometry from the specified table.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <param name="geom">The geometry to compare against.</param>
    /// <param name="schema">The optional schema. Default is "public".</param>
    /// <returns>The SQL query string.</returns>
    public static string SqlIntersects(string table, Geometry geom, string? schema)
    {
        var s = schema ?? "public";
        var sql = $"SELECT id, key, ST_Polygon(ST_Union(ST_Clip(r.raster, g.geom))) as geom FROM {s}.{table} AS r " +
                  $"INNER JOIN {geom?.AsText()} AS g(geom) " +
                  $"ON ${SqlFunctionSpatialByGeometry(geom!)}(r.raster, g.geom)";
        return sql;
    }
}