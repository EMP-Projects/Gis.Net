namespace Gis.Net.Vector;

public static class GisGeometries
{
    public const string LineString = "LINESTRING";
    
    public const string Point = "POINT";
    
    public const string Polygon = "POLYGON";  
    
    public const string MultiLineString = "MULTILINESTRING";    
    
    public const string MultiPoint = "MULTIPOINT";
    
    public const string MultiCurve = "MULTICURVE";

    public const string MultiPolygon = "MULTIPOLYGON";
    
    public const string GeometryCollection = "GEOMETRYCOLLECTION";
    
    public const string MultiSurface = "MULTISURFACE";
    
    public const string Surface = "SURFACE";
    
    public const string Curve = "CURVE";
    
    public const string MultiPointZ = "MULTIPOINTZ";
    
    public const string MultiLineStringZ = "MULTILINESTRINGZ";
    
    public const string MultiPolygonZ = "MULTIPOLYGONZ";
    
    public const string MultiCurveZ = "MULTICURVEZ";

    public const string MultiSurfaceZ = "MULTISURFACEZ";

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