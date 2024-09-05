using NetTopologySuite.Geometries;

namespace Gis.Net.Vector;

/// <summary>
/// Defines the standard names for various types of geometric objects used in GIS (Geographic Information Systems).
/// </summary>
public static class GisGeometries
{
    /// <summary>
    /// Represents the LINESTRING geometry type.
    /// </summary>
    public const string LineString = "LINESTRING";

    /// <summary>
    /// Represents a point geometry.
    /// </summary>
    public const string Point = "POINT";

    /// <summary>
    /// Represents a polygon geometry.
    /// </summary>
    public const string Polygon = "POLYGON";

    /// <summary>
    /// Represents the constant value for the MultiLineString geometry type.
    /// </summary>
    public const string MultiLineString = "MULTILINESTRING";

    /// <summary>
    /// Represents a multi-point geometry.
    /// </summary>
    public const string MultiPoint = "MULTIPOINT";

    /// <summary>
    /// Represents a multi-curve geometry in GIS.
    /// </summary>
    public const string MultiCurve = "MULTICURVE";

    /// <summary>
    /// Represents a MultiPolygon geometry in GIS.
    /// </summary>
    public const string MultiPolygon = "MULTIPOLYGON";

    /// <summary>
    /// Represents a collection of different geometries.
    /// </summary>
    public const string GeometryCollection = "GEOMETRYCOLLECTION";

    /// <summary>
    /// Represents a constant variable for the MultiSurface geometry type.
    /// </summary>
    public const string MultiSurface = "MULTISURFACE";

    /// <summary>
    /// Geometry type identifier for a surface.
    /// </summary>
    public const string Surface = "SURFACE";

    /// <summary>
    /// Represents the geometry type "CURVE".
    /// </summary>
    public const string Curve = "CURVE";

    /// <summary>
    /// Represents a MultiPointZ geometry type.
    /// </summary>
    public const string MultiPointZ = "MULTIPOINTZ";

    /// <summary>
    /// Represents the MULTILINESTRINGZ geometry type.
    /// </summary>
    public const string MultiLineStringZ = "MULTILINESTRINGZ";

    /// <summary>
    /// Represents a MultiPolygonZ geometry.
    /// </summary>
    public const string MultiPolygonZ = "MULTIPOLYGONZ";

    /// <summary>
    /// Represents the constant string value "MULTICURVEZ" used in GIS geometries.
    /// </summary>
    public const string MultiCurveZ = "MULTICURVEZ";

    /// <summary>
    /// Represents the constant value "MULTISURFACEZ" which is used to determine the geometry type.
    /// </summary>
    public const string MultiSurfaceZ = "MULTISURFACEZ";

    /// <summary>
    /// Normalizes the given geometry by returning its boundary if it is a valid multiple geometry type.
    /// </summary>
    /// <param name="geom">The geometry to normalize.</param>
    /// <returns>The boundary of the geometry if it is a valid multiple geometry type; otherwise, the original geometry.</returns>
    public static Geometry NormalizeGeometry(Geometry geom) 
        => IsValidGeometry(geom.GeometryType) && IsMultipleGeometry(geom) ? geom.Boundary : geom;

    /// <summary>
    /// Compares two geometries to determine their spatial relationship based on their types.
    /// </summary>
    /// <param name="geom1">The first geometry to compare.</param>
    /// <param name="geom2">The second geometry to compare.</param>
    /// <param name="distance">An optional distance parameter for point geometries.</param>
    /// <returns>
    /// True if the geometries satisfy the spatial relationship based on their types; otherwise, false.
    /// </returns>
    public static bool CompareGeometries(Geometry geom1, Geometry geom2, double? distance = null)
    {
        if (IsPoint(geom2))
            return geom1.IsWithinDistance(geom2, distance ?? 100);
        if (IsLineString(geom2))
            return geom1.Touches(geom2);
        if (IsPolygon(geom2))
            return geom1.Intersects(geom2);
    
        return geom1.Intersects(NormalizeGeometry(geom2));
    }
    
    /// <summary>
    /// Determines whether the given geometry is a point or a multi-point geometry.
    /// </summary>
    /// <param name="geom">The geometry to check.</param>
    /// <returns>True if the geometry is a point or a multi-point geometry; otherwise, false.</returns>
    public static bool IsPoint(Geometry geom) => geom.GeometryType.Equals(Point) || geom.GeometryType.Equals(MultiPoint) || geom.GeometryType.Equals(MultiPointZ);

    /// <summary>
    /// Determines whether the given geometry is a line string or a multi-line string geometry.
    /// </summary>
    /// <param name="geom">The geometry to check.</param>
    /// <returns>True if the geometry is a line string or a multi-line string geometry; otherwise, false.</returns>
    public static bool IsLineString(Geometry geom) => geom.GeometryType.Equals(LineString) || geom.GeometryType.Equals(MultiLineString) || geom.GeometryType.Equals(MultiLineStringZ) || geom.GeometryType.Equals(Curve) || geom.GeometryType.Equals(MultiCurve) || geom.GeometryType.Equals(MultiCurveZ);

    /// <summary>
    /// Determines whether the given geometry is a polygon or a multi-polygon geometry.
    /// </summary>
    /// <param name="geom">The geometry to check.</param>
    /// <returns>True if the geometry is a polygon or a multi-polygon geometry; otherwise, false.</returns>
    public static bool IsPolygon(Geometry geom) => geom.GeometryType.Equals(Polygon) || geom.GeometryType.Equals(MultiPolygon) || geom.GeometryType.Equals(MultiPolygonZ) || geom.GeometryType.Equals(Surface) || geom.GeometryType.Equals(MultiSurface) || geom.GeometryType.Equals(MultiSurfaceZ);

    /// <summary>
    /// Determines whether the given geometry is a multiple geometry type.
    /// </summary>
    /// <param name="geom">The geometry to check.</param>
    /// <returns>True if the geometry is a multiple geometry type; otherwise, false.</returns>
    public static bool IsMultipleGeometry(Geometry geom)
    {
        return geom.GeometryType.Equals(MultiLineString) ||
               geom.GeometryType.Equals(MultiCurve) ||
               geom.GeometryType.Equals(MultiPoint) ||
               geom.GeometryType.Equals(MultiPolygon) ||
               geom.GeometryType.Equals(GeometryCollection) ||
               geom.GeometryType.Equals(MultiSurface) ||
               geom.GeometryType.Equals(Surface) ||
               geom.GeometryType.Equals(Curve) ||
               geom.GeometryType.Equals(MultiPointZ) ||
               geom.GeometryType.Equals(MultiLineStringZ) ||
               geom.GeometryType.Equals(MultiPointZ) ||
               geom.GeometryType.Equals(MultiCurveZ) ||
               geom.GeometryType.Equals(MultiSurfaceZ);
    }

    /// <summary>
    /// Determines whether a given geometry type is valid.
    /// </summary>
    /// <param name="geomType">The geometry type to be checked. (e.g. "POINT")</param>
    /// <returns>True if the geometry type is valid; otherwise, false.</returns>
    public static bool IsValidGeometry(string geomType)
    {
        return geomType.ToUpper().Equals(LineString) ||
               geomType.ToUpper().Equals(Point) ||
               geomType.ToUpper().Equals(Polygon) ||
               geomType.ToUpper().Equals(MultiLineString) ||
               geomType.ToUpper().Equals(MultiCurve) ||
               geomType.ToUpper().Equals(MultiPoint) ||
               geomType.ToUpper().Equals(MultiPolygon) ||
               geomType.ToUpper().Equals(GeometryCollection) ||
               geomType.ToUpper().Equals(MultiSurface) ||
               geomType.ToUpper().Equals(Surface) ||
               geomType.ToUpper().Equals(Curve) ||
               geomType.ToUpper().Equals(MultiPointZ) ||
               geomType.ToUpper().Equals(MultiLineStringZ) ||
               geomType.ToUpper().Equals(MultiPointZ) ||
               geomType.ToUpper().Equals(MultiCurveZ) ||
               geomType.ToUpper().Equals(MultiSurfaceZ);
    }
}