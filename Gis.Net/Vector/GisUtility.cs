using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using Gis.Net.Core;
using Gis.Net.Vector.DTO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.IO.Esri;
using NetTopologySuite.Operation.Buffer;
using NetTopologySuite.Precision;

namespace Gis.Net.Vector;

public static class GisUtility
{
    private const double ConvToMeters = 0.00001;
    
    /// <summary>
    /// calcolo le dimensioni dell'area, lunghezza rispetto alla geometria di confronto
    /// </summary>
    /// <param name="feature"></param>
    /// <param name="geom"></param>
    public static void CalculateMeasure(ref Feature feature, Geometry geom)
    {
        if (geom.GeometryType == GisGeometries.Point)
            return;
        
        // se la geometria di confronto non è un punto allora esegui i calcoli
        const string propName = "Measure";

        var area = ConvertAreaGeometry(feature.Geometry.Area);
        var length = ConvertLenghtGeometry(feature.Geometry.Length);
        var areaGeom = ConvertAreaGeometry(geom.Area);
        var percentage = Math.Round((float)(area / areaGeom) * 100, 1);
        
        MeasureResponse m = new()
        {
            Area = area,
            Lenght = length,
            Percentage = percentage
        };
        
        if (feature.Attributes.Exists(propName))
            feature.Attributes.DeleteAttribute(propName);
        
        feature.Attributes.Add(propName, m);
    }
    
    public static T? DeserializeXml<T>(string xml) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xml);
        return (T?)serializer.Deserialize(reader);
    }
    
    public static double ConvertDegreesToRadians(double degrees) => (Math.PI / 180) * degrees;
    
    private static Polygon? ConvertToPolygon(Geometry? geom, double buffer)
    {
        while (true)
        {
            if (geom is null) return null;
            GeometryTransformer gt = new();
            var result = gt.Transform(BufferGeometry(geom, buffer));
            if (result.GeometryType.ToUpper() == GisGeometries.Polygon) return (Polygon)result;
            geom = result.Envelope;
        }
    }
    
    public static Geometry BufferGeometry(Geometry geom, double buffer, int segment = 8, EndCapStyle style = EndCapStyle.Round) => 
        buffer > 0 ? geom.Buffer(buffer * ConvToMeters, segment, style) : geom;
    
    public static void WithInFeatures(ref Feature feature, Geometry geom)
    {
        if (feature.Geometry.Intersects(geom))
            feature.Geometry = feature.Geometry.Intersection(geom);
    }
    
    public static Feature WithInFeatures(Geometry geomFeature, Geometry? geom, int srCode, double buffer) 
        => GisUtility.CreateEmptyFeature(srCode, geomFeature.Intersection((geom)), buffer);

    /// <summary>
    /// Create empty feature with empty geometry
    /// </summary>
    /// <param name="srCode"></param>
    /// <returns></returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static Feature CreateEmptyFeature(int srCode)
    {
        var geom = CreatePolygon(srCode);

        Feature result = new()
        {
            Geometry = geom,
            Attributes = new AttributesTable()
        };

        return result;
    }
        
    /// <summary>
    /// Create empty feature with geometry
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="geom"></param>
    /// <returns></returns>
    public static Feature CreateEmptyFeature(int srCode, Geometry geom)
    {
        var feature = CreateEmptyFeature(srCode);
        feature.Geometry = geom;
        return feature;
    }
        
    /// <summary>
    /// create empty feature with buffer
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="geom"></param>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static Feature CreateEmptyFeature(int srCode, Geometry geom, double buffer)
    {
        var feature = CreateEmptyFeature(srCode, geom);
        feature.Geometry = BufferGeometry(geom, buffer);
        return feature;
    }

    /// <summary>
    /// create empty geometry
    /// </summary>
    /// <param name="srCode"></param>
    /// <returns></returns>
    public static Geometry CreatePoint(int srCode) 
        => CreateGeometryFactory(srCode).CreateEmpty(Dimension.Point);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static Point CreatePoint(int srCode, Coordinate coordinates) 
        => CreateGeometryFactory(srCode).CreatePoint(coordinates);
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static Point CreatePoint(int srCode, double[] coordinates)
    {
        if (coordinates[0] == 0 && coordinates[1] == 0) 
            throw new Exception("Non posso creare un punto geometrico con coordinate uguali a 0");
        
        var newCoordinates =  new Coordinate(coordinates[0], coordinates[1]);
        return CreateGeometryFactory(srCode).CreatePoint(newCoordinates);
    }
        
    private static LineString CreateLine(int srCode) => CreateGeometryFactory(srCode).CreateLineString();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static LineString CreateLine(int srCode, List<double[]> coordinates)
    {
        if (coordinates.Count == 0) return CreateLine(srCode);
        var newCoordinates = coordinates.Select(coordinate => new Coordinate(coordinate[0], coordinate[1])).ToArray();
        return CreateLine(srCode, newCoordinates);
    }
    
    /// <summary>
    /// In base al tipo di geometria restituisce il tipo di funzione spaziale
    /// </summary>
    /// <param name="geom"></param>
    /// <returns></returns>
    public static string SqlFunctionSpatialByGeometry(Geometry geom)
    {
        var sqlSpatial = geom!.GeometryType.ToUpper().Equals("POINT") 
            ? "ST_Within"
            : "ST_Intersects";
        return sqlSpatial;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static LineString CreateLine(int srCode, Coordinate[] coordinates) => 
        coordinates.Length == 0 ? CreateLine(srCode) : CreateGeometryFactory(srCode).CreateLineString(coordinates);
    
    private static LinearRing CreateLineRing(int srCode) => CreateGeometryFactory(srCode).CreateLinearRing();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static LinearRing CreateLineRing(int srCode, List<double[]> coordinates)
    {
        if (coordinates.Count == 0) return CreateLineRing(srCode);
        var line = CreateLine(srCode, coordinates);
        if (!line.IsClosed) return CreateLineRing(srCode);
        var newCoordinates = coordinates.Select(coordinate => new Coordinate(coordinate[0], coordinate[1])).ToArray();
        return CreateGeometryFactory(srCode).CreateLinearRing(newCoordinates);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="geom"></param>
    /// <returns></returns>
    public static Geometry CopyGeometry(int srCode, Geometry geom) =>
        CreateGeometryFactory(srCode).CreateGeometry(geom);
        
    /// <summary>
    /// Create empty polygon
    /// </summary>
    /// <param name="srCode"></param>
    /// <returns></returns>
    private static Geometry CreatePolygon(int srCode) => CreateGeometryFactory(srCode).CreateEmpty(Dimension.Surface);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static Polygon CreatePolygon(int srCode, Coordinate[] coordinates) => CreateGeometryFactory(srCode).CreatePolygon(coordinates);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    public static Geometry CreateGeometryFromBBox(int srCode, Envelope env) => CreateGeometryFactory(srCode).ToGeometry(env);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srCode"></param>
    /// <param name="lngXMin"></param>
    /// <param name="latYMin"></param>
    /// <param name="lngXMax"></param>
    /// <param name="latYMax"></param>
    /// <returns></returns>
    public static Geometry CreateGeometryFromBBox(int srCode, double lngXMin, double latYMin, double lngXMax, double latYMax) 
        => CreateGeometryFromBBox(srCode, new Envelope(new Coordinate(lngXMin, latYMin),
            new Coordinate(lngXMax, latYMax)));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geom"></param>
    /// <param name="srCode"></param>
    /// <returns></returns>
    public static Geometry ValidateGeometry(Geometry geom, int srCode)
    {
        if (geom.GeometryType.ToUpper() != GisGeometries.LineString && geom.IsValid) return geom;
        var lr = new LinearRing(geom.Coordinates);
        var newGeom = lr.IsClosed ? CreatePolygon(srCode, geom.Coordinates) : 
            CreatePolygonFromEnvelope(srCode, geom.EnvelopeInternal!);
        return newGeom;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geom1"></param>
    /// <param name="geom2"></param>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static Geometry? IntersectionsGeometries(Geometry geom1, Geometry geom2, double? buffer)
    {
        if (!geom1.Intersects(geom2)) return null;

        var g1 = geom1.GeometryType.ToUpper() != "POLYGON"
                ? ConvertToPolygon(ReduceGeometry(geom1), buffer ?? 0)
                : ReduceGeometry(geom1);
        var g2 = geom2.GeometryType.ToUpper() != "POLYGON"
            ? ConvertToPolygon(ReduceGeometry(geom2), buffer ?? 0)
            : ReduceGeometry(geom2);

        return GeometryOverlay.NG.Intersection(g1, g2);
       
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="area"></param>
    /// <returns></returns>
    public static double ConvertAreaGeometry(double area) => Math.Round((float)area / Math.Pow(ConvToMeters, 2), 2);
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lenght"></param>
    /// <returns></returns>
    public static double ConvertLenghtGeometry(double lenght) => Math.Round((float)lenght / ConvToMeters, 2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geom"></param>
    /// <returns></returns>
    private static Geometry ReduceGeometry(Geometry geom)
    {
        var pm = new PrecisionModel(PrecisionModels.FloatingSingle);
        var geomReducer = new GeometryPrecisionReducer(pm);

        try
        {
            return geomReducer.Reduce(geom);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return geom;
        }
    }

    private static Geometry CreatePolygonFromEnvelope(int srCode, Envelope bbox) =>
        CreateGeometryFactory(srCode).ToGeometry(bbox);
        
    /// <summary>
    /// Create Geometry factor
    /// </summary>
    /// <param name="srCode"></param>
    /// <returns></returns>
    public static GeometryFactory CreateGeometryFactory(int srCode)
    {
        var curInstance = NetTopologySuite.NtsGeometryServices.Instance;
            
        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
            curInstance.DefaultCoordinateSequenceFactory,
            curInstance.DefaultPrecisionModel,
            srCode,
            GeometryOverlay.NG, // RH: use 'Next Gen' overlay generator
            curInstance.CoordinateEqualityComparer);
        return NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(srCode);
    }
    
    /// <summary>
    /// Calculate Geometry difference from list features
    /// </summary>
    /// <param name="features"></param>
    /// <param name="geom"></param>
    /// <returns></returns>
    public static Geometry? CalculateGeometryDifference(IEnumerable<Feature?> features, Geometry geom)
    {
        try
        {
            var geomDiff = ReduceGeometry(geom.Copy().Normalized());
            foreach (var feature in features.Where(feature => geomDiff.Intersects(feature?.Geometry)))
            {
                var g = ReduceGeometry(feature?.Geometry!.Normalized()!);
                geomDiff = GeometryOverlay.NG.Difference(geomDiff, g);
            }

            return geomDiff.IsEmpty ? null : geomDiff.Intersection(geom);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Check if geometry is polygon
    /// </summary>
    /// <param name="geom"></param>
    /// <returns></returns>
    public static bool IsPolygon(Geometry geom) => geom.GeometryType.ToUpper() == GisGeometries.Polygon;
        
    /// <summary>
    /// Delete properties with null value
    /// </summary>
    /// <param name="feature"></param>
    public static void DeleteNullProperties(ref Feature feature)
    {
        for (var i = 0; i < feature.Attributes.Count; i++)
        {
            var attrName = feature.Attributes.GetNames()[i];
            var value = feature.Attributes.GetValues()[i];
            if (value is null)
                feature.Attributes.DeleteAttribute(attrName);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="feature"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    public static void AddProperty<T>(ref Feature feature, string name, T value) where T : class 
        => feature.Attributes.Add(name, value);
        
    /// <summary>
    /// Check if property exist in feature
    /// </summary>
    /// <param name="feature"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static bool IsPropertyExist(Feature feature, string propertyName) 
        => feature.Attributes.Exists(propertyName);
        
    public static FeatureCollection CopyFeatureCollection(FeatureCollection origin, FeatureCollection destination)
    {
        foreach (var feature in origin)
            destination.Add(feature);
        return destination;
    }
    
    /// <summary>
    /// Create Feature Collection from Feature
    /// </summary>
    /// <param name="features"></param>
    /// <returns></returns>
    public static FeatureCollection CreateFeatureCollection(List<IFeature>? features)
    {
        FeatureCollection fColl = [];
        if (features is null || features.Count == 0) return fColl;
        foreach (var f in features.OfType<Feature>()) fColl.Add(f);
        return fColl;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="features"></param>
    /// <returns></returns>
    public static FeatureCollection CreateFeatureCollection(Feature[]? features)
    {
        FeatureCollection fColl = new();
        if (features is null || features.Length == 0) return fColl;
        
        foreach (var f in features)
            fColl.Add(f);
        return fColl;
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="geom"></param>
    /// <returns></returns>
    public static string CoordinatesBBoxFromGeometry(Geometry geom)
    {
        var envBBox = geom.EnvelopeInternal;
        return string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:F8},{1:F8},{2:F8},{3:F8}", envBBox.MinY, envBBox.MinX, envBBox.MaxY, envBBox.MaxX);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minLat"></param>
    /// <param name="minLon"></param>
    /// <param name="maxLat"></param>
    /// <param name="maxLon"></param>
    /// <returns></returns>
    public static Envelope CreateEnvelopeFromBBox(double minLat, double minLon, double maxLat, double maxLon)
    {
        var minCoordinate = ConvertToCoordinate(minLon, minLat);
        var maxCoordinate = ConvertToCoordinate(maxLon, maxLat);
        return new Envelope(minCoordinate, maxCoordinate);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbox"></param>
    /// <returns></returns>
    public static Envelope CreateEnvelopeFromBBox(double[] bbox) 
        => CreateEnvelopeFromBBox(bbox[0], bbox[1], bbox[2], bbox[3]);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lon"></param>
    /// <param name="lat"></param>
    /// <returns></returns>
    private static Coordinate ConvertToCoordinate(double lon, double lat) =>
        new(Convert.ToDouble(lon, CultureInfo.InvariantCulture), Convert.ToDouble(lat, CultureInfo.InvariantCulture));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geom"></param>
    /// <param name="srCode"></param>
    /// <returns></returns>
    public static Feature CreateBoundary(Geometry geom, int srCode)
    {
        // calculate boundary
        var featureWithBoundary = CreateEmptyFeature(srCode, BufferGeometry(geom, 0).Boundary);
        AddProperty(ref featureWithBoundary, "Name", "Boundary");
        return featureWithBoundary;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string AdjustSingleQuote(string value) => value.Replace("'", "''");
    
    /// <summary>
    /// Encode Base64 Data
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Base64Encode(string text)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(textBytes);
    }

    /// <summary>
    /// Decode Base64 Data
    /// </summary>
    /// <param name="base64"></param>
    /// <returns></returns>
    public static string Base64Decode(string base64)
    {
        var base64Bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(base64Bytes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoJsonFile"></param>
    /// <returns></returns>
    private static string GetRawGeoJson(string geoJsonFile)
    {
        var path = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeoJson"));
        return GetRawContent(geoJsonFile, path);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoJsonFile"></param>
    /// <param name="pathDir"></param>
    /// <returns></returns>
    private static string GetRawGeoJson(string geoJsonFile, string pathDir) => GetRawContent(geoJsonFile, pathDir);

    private static string GetRawContent(string fileName, string path)
    {
        var pathDir = Path.Combine(path, fileName);
        Console.WriteLine($"GeoJson path -> {pathDir}");
        return File.ReadAllText(pathDir);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoJsonFileName"></param>
    /// <returns></returns>
    public static FeatureCollection? GetFeatureCollectionByGeoJson(string geoJsonFileName)
    {
        var geoJson = AdjustSingleQuote(GetRawGeoJson(geoJsonFileName));
        return NetCore.DeserializeString<FeatureCollection>(geoJson);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoJsonFileName"></param>
    /// <param name="pathDir"></param>
    /// <returns></returns>
    public static FeatureCollection GetFeatureCollectionByGeoJson(string geoJsonFileName, string pathDir)
    {
        var geoJson = AdjustSingleQuote(GetRawGeoJson(geoJsonFileName, pathDir));
        return NetCore.DeserializeString<FeatureCollection>(geoJson)!;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IEnumerable<IFeature> ReadFeaturesByEsriFile(string path, string key) 
        => Shapefile.ReadAllFeatures(path).AsEnumerable();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geomFilter"></param>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static Geometry CreateGeometryFromFilter(string geomFilter, double? buffer)
    {
       var geom = NetCore.DeserializeString<Geometry>(geomFilter);
        
        // check if Geom is valid and convert to polygon 
        if (geom is not null && !geom.IsEmpty && !IsPolygon(geom) && buffer > 0) 
            geom = BufferGeometry(geom, (double)buffer);
        
        return geom;
    }
    
    /// <summary>
    /// Calcolo il punto centrale di una features Collection
    /// </summary>
    /// <param name="features"></param>
    /// <param name="srCode"></param>
    /// <returns></returns>
    public static Point CalculateCentre(FeatureCollection features, int srCode)
    {
        var center = features.BoundingBox.Centre;
        return CreatePoint(srCode, center);
    }
    
}