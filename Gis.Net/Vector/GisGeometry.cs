using NetTopologySuite.Geometries;

namespace Gis.Net.Vector;

/// <summary>
/// Represents a GIS Geometry object.
/// </summary>
public class GisGeometry
{
    /// <summary>
    /// The geometry of the GIS element. Can be null.
    /// </summary>
    public Geometry? Geom { get; set; }

    /// <summary>
    /// The distance in meters associated with the GIS element.
    /// </summary>
    public double? Distance { get; set; } = 100;

    /// <summary>
    /// The spatial reference code (SRCode) of the GIS element.
    /// </summary>
    public int SrCode { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="GisGeometry"/> class with the default spatial reference code (WebMercator).
    /// </summary>
    public GisGeometry()
    {
        SrCode = (int)ESrCode.WebMercator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GisGeometry"/> class with the specified spatial reference code (SR code).
    /// </summary>
    /// <param name="srCode">The spatial reference code (SR code) of the GIS element.</param>
    public GisGeometry(int srCode)
    {
        SrCode = srCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GisGeometry"/> class with the specified spatial reference code (SR code) and geometry.
    /// </summary>
    /// <param name="srCode">The spatial reference code (SR code) of the GIS element.</param>
    /// <param name="geom">The geometry of the GIS element.</param>
    public GisGeometry(int srCode, Geometry geom) : this(srCode)
    {
        Geom = geom;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="GisGeometry"/> class with the specified spatial reference code (SR code), latitude, longitude, and distance.
    /// </summary>
    /// <param name="srCode">The spatial reference code (SR code) of the GIS element.</param>
    /// <param name="lat">The latitude of the GIS element.</param>
    /// <param name="lng">The longitude of the GIS element.</param>
    /// <param name="distance">The distance in meters associated with the GIS element.</param>
    public GisGeometry(int srCode, double lat, double lng, double distance) : this(srCode)
    {
        Distance = distance;
        Geom = GisUtility.CreatePoint(srCode, new Coordinate(lng, lat));
    }

    /// <summary>
    /// Represents a GIS geometry.
    /// </summary>
    public GisGeometry(int srCode, double lngMin, double latMin, double lngMax, double latMax) : this(srCode)
    {
        Geom = GisUtility.CreateGeometryFromBBox(
            srCode,
            lngMin,
            latMin,
            lngMax,
            latMax);
    }

    /// The GisGeometry class represents a GIS geometry object.
    /// /
    public GisGeometry(int srCode, string geoJson) : this(srCode)
    {
        Geom = GisUtility.CreateGeometryFromFilter(geoJson, null);
    }

    /// <summary>
    /// Gets a value indicating whether the geometry is a line or a multi-line.
    /// </summary>
    /// <value><c>true</c> if the geometry is a line or a multi-line; otherwise, <c>false</c>.</value>
    public bool IsLine => Geom is not null && (Geom.GeometryType.ToUpper().Equals(GisGeometries.LineString) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiLineString) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiCurve) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.Curve) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiLineStringZ) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiCurveZ));

    /// <summary>
    /// Determines whether the geometry represents a polygon or not.
    /// </summary>
    /// <value>
    /// <c>true</c> if the geometry represents a polygon; otherwise, <c>false</c>.
    /// </value>
    public bool IsPolygon =>
        Geom is not null && (Geom.GeometryType.ToUpper().Equals(GisGeometries.Polygon) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPolygon) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiSurface) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.Surface) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPolygonZ) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiSurfaceZ));

    /// <summary>
    /// Gets a value indicating whether the GisGeometry is a point.
    /// </summary>
    /// <remarks>
    /// The GisGeometry is considered a point if the Geom property is not null and the GeometryType is "POINT",
    /// "MULTIPOINT", or "MULTIPOINTZ".
    /// </remarks>
    /// <value>
    /// <c>true</c> if the GisGeometry is a point; otherwise, <c>false</c>.
    /// </value>
    public bool IsPoint => Geom is not null && (Geom.GeometryType.ToUpper().Equals(GisGeometries.Point) ||
                                                Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPoint) ||
                                                Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPointZ));

    /// <summary>
    /// Performs a comparison between two geometries to determine if they interact in a certain way (for example, if they touch each other, intersect, etc.).
    /// </summary>
    /// <param name="geom1">The first geometry for comparison.</param>
    /// <param name="geom2">The second geometry for comparison.</param>
    /// <returns>True if the geometries satisfy the specified interaction condition, otherwise false.</returns>
    public static bool operator &(GisGeometry geom1, GisGeometry geom2)
    {
        if (geom1.IsPoint)
        {
            switch (geom2)
            {
                // when comparing points, check that the distance is less than the buffer
                case { IsPoint: true, Geom: not null } when geom1.Distance is not null:
                    return geom2.Geom.Distance(geom1.Geom) <= geom1.Distance.Value;
                // in case the model geometry is a line,
                // is only valid if touched with the search point
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                // if the model geometry is a polygon it is valid only if the search point
                // falls inside the geometry or touches each other
                case { IsPolygon: true, Geom: not null } when geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom):
                    return true;
            }
        }
        
        if (geom1.IsLine)
        {
            switch (geom2)
            {
                // geometry is only valid if touched with the search point
                case { IsPoint: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                // if the comparison is between two lines they must at least touch each other
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                // if there are points in common with the search polygon returns
                // only the geometric part that intersects
                case { IsPolygon: true, Geom: not null } when geom2.Geom.Crosses(geom1.Geom):
                    return true;
            }
        }
        
        if (!geom1.IsPolygon) return false;

        switch (geom2)
        {
            // if the comparison is with a geometric point they must at least touch each other or it must be contained in the polygon
            case { IsPoint: true, Geom: not null } when (geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom)):
            // in the case of comparison between polygon and line they must at least intersect
            case { IsLine: true, Geom: not null } when geom2.Geom.Crosses(geom1.Geom):
                return true;
            default:
                // in the case of comparison of two polygons they must at least intersect
                return geom2 is { IsPolygon: true, Geom: not null } && geom2.Geom.Intersects(geom1.Geom);
        }
    }

    /// <summary>
    /// Calculates the difference between two geometries, returning a new geometry that represents the intersecting part with the first operand, or null.
    /// </summary>
    /// <param name="geom1">The first geometry for the difference calculation.</param>
    /// <param name="geom2">The second geometry for the difference calculation.</param>
    /// <returns>A new geometry that represents the intersection part, or null if there is no intersection.</returns>
    public static GisGeometry? operator -(GisGeometry geom1, GisGeometry geom2)
    {
        if (geom1.IsPoint)
        {
            // when comparing points, check that the distance is less than the buffer
            if (geom2.IsPoint)
                return geom2.Geom is not null && geom2.Geom?.Distance(geom1.Geom) <= geom1.Distance
                    ? geom2
                    : null;

            switch (geom2)
            {
                // in case the model geometry is a line, it is valid only if it touches the search point
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                    return geom2;
                // if the model geometry is a polygon it is valid only if the search point
                // falls inside the geometry or touches each other
                case { IsPolygon: true, Geom: not null } when geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom):
                    return geom2;
            }
        }

        if (geom1.IsLine)
        {
            switch (geom2)
            {
                // geometry is only valid if touched with the search point
                case { IsPoint: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                    return geom2;
                // if the comparison is between two lines they must at least touch each other
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                    return geom2;
            }

            // if there are points in common with the search polygon returns
            // only the geometric part that intersects
            if (geom2.IsPolygon && geom1.Geom is not null && geom2.Geom!.Crosses(geom1.Geom))
                return new GisGeometry(geom1.SrCode, geom1.Geom.Intersection(geom2.Geom)!);
        }

        if (!geom1.IsPolygon) return null;

        return geom2 switch
        {
            // if the comparison is with a geometric point they must at least touch each other or it must be contained in the polygon
            { IsPoint: true, Geom: not null } when geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom) => geom2,
            // in the case of comparison between polygon and line they must at least intersect
            { IsLine: true, Geom: not null } when geom2.Geom.Crosses(geom1.Geom) => geom2,
            // in the case of comparison of two polygons they must at least intersect
            { IsPolygon: true, Geom: not null } when geom1.Geom is not null && geom2.Geom.Intersects(geom1.Geom) 
                => new GisGeometry( geom1.SrCode, geom1.Geom.Intersection(geom2.Geom)!),
            _ => null
        };

    }
    
}