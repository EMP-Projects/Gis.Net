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
    /// The distance associated with the GIS element.
    /// </summary>
    public double? Distance { get; set; }

    /// <summary>
    /// The spatial reference code (SRCode) of the GIS element.
    /// </summary>
    public int SrCode { get; set; }

    /// <summary>
    /// Represents a GIS geometry object.
    /// </summary>
    public GisGeometry()
    {
        SrCode = (int)ESrCode.Wgs84;
    }

    /// <summary>
    /// Represents a GIS geometry with associated spatial reference code (SR code).
    /// </summary>
    public GisGeometry(int srCode)
    {
        SrCode = srCode;
    }

    /// <summary>
    /// Class representing a GIS geometry.
    /// </summary>
    public GisGeometry(int srCode, Geometry geom) : this(srCode)
    {
        Geom = geom;
    }

    /// <summary>
    /// Represents a GIS geometry object.
    /// </summary>
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
    /// Esegue un confronto tra due geometrie per determinare se interagiscono in un certo modo (ad esempio, se si toccano, si intersecano, etc.).
    /// </summary>
    /// <param name="geom1">La prima geometria per il confronto.</param>
    /// <param name="geom2">La seconda geometria per il confronto.</param>
    /// <returns>True se le geometrie soddisfano la condizione di interazione specificata, altrimenti false.</returns>
    public static bool operator &(GisGeometry geom1, GisGeometry geom2)
    {
        if (geom1.IsPoint)
        {
            switch (geom2)
            {
                // nel confronto di punti verificare che la distanza sia minore del buffer
                case { IsPoint: true, Geom: not null } when geom1.Distance is not null:
                    return geom2.Geom.Distance(geom1.Geom) <= geom1.Distance.Value;
                // nel caso in cui la geometria del modello è una linea,
                // è valida solo se si tocca con il punto di ricerca
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                // se la geometria del modello è un poligono è valida solo se il punto di ricerca
                // cade all'interno della geometria o si toccano
                case { IsPolygon: true, Geom: not null } when geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom):
                    return true;
            }
        }
        
        if (geom1.IsLine)
        {
            switch (geom2)
            {
                // la geometria è valida solo se si tocca con il punto di ricerca
                case { IsPoint: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                // se il confronto è tra due linee devono almeno toccarsi
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                // se ci sono punti in comune con il poligono di ricerca restituisce
                // solo la parte geometrica che si interseca
                case { IsPolygon: true, Geom: not null } when geom2.Geom.Crosses(geom1.Geom):
                    return true;
            }
        }
        
        if (!geom1.IsPolygon) return false;

        switch (geom2)
        {
            // se il confronto è con un punto geometrico devono almeno toccarsi o deve essere contenuto nel poligono
            case { IsPoint: true, Geom: not null } when (geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom)):
            // nel caso di confronto tra poligono e linea devono almeno incrociarsi
            case { IsLine: true, Geom: not null } when geom2.Geom.Crosses(geom1.Geom):
                return true;
            default:
                // nel caso di confronto di due poligono devono almeno intersecarsi
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
                // se la geometria del modello è un poligono è valida solo se il punto di ricerca
                // cade all'interno della geometria o si toccano
                case { IsPolygon: true, Geom: not null } when geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom):
                    return geom2;
            }
        }

        if (geom1.IsLine)
        {
            switch (geom2)
            {
                // la geometria è valida solo se si tocca con il punto di ricerca
                case { IsPoint: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                    return geom2;
                // se il confronto è tra due linee devono almeno toccarsi
                case { IsLine: true, Geom: not null } when geom2.Geom.Touches(geom1.Geom):
                    return geom2;
            }

            // se ci sono punti in comune con il poligono di ricerca restituisce
            // solo la parte geometrica che si interseca
            if (geom2.IsPolygon && geom1.Geom is not null && geom2.Geom!.Crosses(geom1.Geom))
                return new GisGeometry(geom1.SrCode, geom1.Geom.Intersection(geom2.Geom)!);
        }

        if (!geom1.IsPolygon) return null;

        return geom2 switch
        {
            // se il confronto è con un punto geometrico devono almeno toccarsi o deve essere contenuto nel poligono
            { IsPoint: true, Geom: not null } when geom2.Geom.Within(geom1.Geom) || geom2.Geom.Touches(geom1.Geom) => geom2,
            // nel caso di confronto tra poligono e linea devono almeno incrociarsi
            { IsLine: true, Geom: not null } when geom2.Geom.Crosses(geom1.Geom) => geom2,
            // nel caso di confronto di due poligonio devono almento inetersecarsi
            { IsPolygon: true, Geom: not null } when geom1.Geom is not null && geom2.Geom.Intersects(geom1.Geom) 
                => new GisGeometry( geom1.SrCode, geom1.Geom.Intersection(geom2.Geom)!),
            _ => null
        };

    }
    
}