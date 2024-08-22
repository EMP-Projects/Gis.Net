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