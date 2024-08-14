using NetTopologySuite.Geometries;

namespace Gis.Net.Raster;

public static class Raster
{
    
    /// <summary>
    /// In base al tipo di geometria restituisce il tipo di funzione spaziale
    /// </summary>
    /// <param name="geom"></param>
    /// <returns></returns>
    private static string SqlFunctionSpatialByGeometry(Geometry geom)
    {
        var sqlSpatial = geom!.GeometryType.ToUpper().Equals("POINT") 
            ? "ST_Within"
            : "ST_Intersects";
        return sqlSpatial;
    }
    
    public static string SqlIntersects(string table, Geometry geom, string? schema)
    {
        var s = schema ?? "public";
        var sql = $"SELECT id, key, ST_Polygon(ST_Union(ST_Clip(r.raster, g.geom))) as geom FROM {s}.{table} AS r " +
                  $"INNER JOIN {geom?.AsText()} AS g(geom) " +
                  $"ON ${SqlFunctionSpatialByGeometry(geom!)}(r.raster, g.geom)";
        return sql;
    }
}