using NetTopologySuite.Geometries;

namespace Gis.Net.Vector;

/// <summary>
/// classe per controllare ed eseguire operazioni spaziali con le geometrie
/// </summary>
public class GisGeometry
{
    /// <summary>
    /// La geometria dell'elemento GIS. Può essere null.
    /// </summary>
    public Geometry? Geom { get; set; }
    
    /// <summary>
    /// Distance tollerabile tra due punti
    /// </summary>
    public double? Distance { get; set; }
    
    /// <summary>
    /// Sistema di riferimento obbligatorio
    /// </summary>
    public int SrCode { get; set; }
    
    /// <summary>
    /// Costruttore che inizializza una nuova istanza di GisGeometry con codice SR specificato.
    /// </summary>
    /// <param name="srCode">Sistema di riferimento</param>
    public GisGeometry()
    {
        SrCode = (int)ESrCode.Wgs84;
    }

    /// <summary>
    /// Costruttore che inizializza una nuova istanza di GisGeometry con codice SR specificato.
    /// </summary>
    /// <param name="srCode">Sistema di riferimento</param>
    /// <param name="srCode"></param>
    public GisGeometry(int srCode)
    {
        SrCode = srCode;
    }
 
    /// <summary>
    /// Costruttore che inizializza una nuova istanza di GisGeometry con una geometria specificata.
    /// </summary>
    /// <param name="geom">La geometria da assegnare all'elemento.</param>
    /// <param name="srCode">Sistema di riferimento</param>
    public GisGeometry(int srCode, Geometry geom) : this(srCode)
    {
        Geom = geom;
    }

    /// <summary>
    /// Costruttore che inizializza una nuova istanza di GisGeometry con latitudine, longitudine, codice SR e buffer.
    /// </summary>
    /// <param name="lat">Latitudine del punto.</param>
    /// <param name="lng">Longitudine del punto.</param>
    /// <param name="srCode">Codice del sistema di riferimento spaziale.</param>
    /// <param name="distance">Distanza di tolleranza dall'elemento.</param>
    public GisGeometry(int srCode, double lat, double lng, double distance) : this(srCode)
    {
        Distance = distance;
        Geom = GisUtility.CreatePoint(srCode, new Coordinate(lng, lat));
    }
    
    /// <summary>
    /// Costruttore che inizializza una nuova istanza di GisGeometry con coordinate di bounding box, codice SR e buffer.
    /// </summary>
    /// <param name="lngMin">Longitudine minima.</param>
    /// <param name="latMin">Latitudine minima.</param>
    /// <param name="lngMax">Longitudine massima.</param>
    /// <param name="latMax">Latitudine massima.</param>
    /// <param name="srCode">Codice del sistema di riferimento spaziale.</param>
    public GisGeometry(int srCode, double lngMin, double latMin, double lngMax, double latMax) : this(srCode)
    {
        Geom = GisUtility.CreateGeometryFromBBox(
            srCode,
            lngMin,
            latMin,
            lngMax,
            latMax);
    }

    /// <summary>
    /// Costruttore che inizializza una nuova istanza di GisGeometry con una stringa JSON che rappresenta la geometria e un buffer.
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="geoJson">Stringa JSON che rappresenta la geometria.</param>
    public GisGeometry(int srCode, string geoJson) : this(srCode)
    {
        Geom = GisUtility.CreateGeometryFromFilter(geoJson, null);
    }

    /// <summary>
    /// Verifica se la geometria è una linea.
    /// </summary>
    /// <returns>True se la geometria è una linea, altrimenti false.</returns>
    public bool IsLine => Geom is not null && (Geom.GeometryType.ToUpper().Equals(GisGeometries.LineString) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiLineString) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiCurve) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.Curve) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiLineStringZ) ||
                                               Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiCurveZ));
        
    /// <summary>
    /// Verifica se la geometria è un poligono.
    /// </summary>
    /// <returns>True se la geometria è un poligono, altrimenti false.</returns>
    public bool IsPolygon => 
        Geom is not null && (Geom.GeometryType.ToUpper().Equals(GisGeometries.Polygon) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPolygon) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiSurface) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.Surface) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPolygonZ) ||
                             Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiSurfaceZ));

    /// <summary>
    /// Verifica se la geometria è un punto.
    /// </summary>
    /// <returns>True se la geometria è un punto, altrimenti false.</returns>
    public bool IsPoint => Geom is not null && (Geom.GeometryType.ToUpper().Equals(GisGeometries.Point) ||
                                                Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPoint) ||
                                                Geom.GeometryType.ToUpper().Equals(GisGeometries.MultiPointZ));
    
    /// <summary>
    /// Esegue un confronto tra due geometrie per determinare se interagiscono in un certo modo (ad esempio, se si toccano, si intersecano, etc.).
    /// </summary>
    /// <param name="geom1">La prima geometria per il confronto.</param>
    /// <param name="geom2">La seconda geometria per il confronto.</param>
    /// <returns>True se le geometrie soddisfano la condizione di interazione specificata, altrimenti false.</returns>
    public static bool operator& (GisGeometry geom1, GisGeometry geom2)
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
    /// Calcola la differenza tra due geometrie, restituendo una nuova geometria che rappresenta la parte geometrica che si interseca con il primo operatore, oppure null.
    /// </summary>
    /// <param name="geom1">La prima geometria per il calcolo della differenza.</param>
    /// <param name="geom2">La seconda geometria per il calcolo della differenza.</param>
    /// <returns>Una nuova geometria che rappresenta la parte di intersezione, oppure null se non c'è intersezione.</returns>
    public static GisGeometry? operator- (GisGeometry geom1, GisGeometry geom2)
    {
        if (geom1.IsPoint)
        {
            // nel confronto di punti verificare che la distanza sia minore del buffer
            if (geom2.IsPoint)
                return geom2.Geom is not null && geom2.Geom?.Distance(geom1.Geom) <= geom1.Distance
                    ? geom2
                    : null;

            switch (geom2)
            {
                // nel caso in cui la geomeria del modello è una linea,
                // è valida solo se si tocca con il punto di ricerca
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