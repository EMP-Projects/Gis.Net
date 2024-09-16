using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using Gis.Net.Core;
using Gis.Net.Vector.DTO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.IO;
using NetTopologySuite.Operation.Buffer;
using NetTopologySuite.Precision;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Shapefile = NetTopologySuite.IO.Esri.Shapefile;

namespace Gis.Net.Vector;

/// <summary>
/// Utility class for GIS operations.
/// </summary>
public static class GisUtility
{
    /// <summary>
    /// Conversion factor from the default unit to meters.
    /// </summary>
    public const double ConvToMeters = 0.00001;

    /// <summary>
    /// Calculates the measure of a feature based on its geometry and a given comparison geometry.
    /// </summary>
    /// <param name="feature">The feature for which to calculate the measure.</param>
    /// <param name="geom">The comparison geometry used for measurement.</param>
    public static void CalculateMeasure(ref Feature feature, Geometry geom)
    {
        if (geom.GeometryType == GisGeometries.Point)
            return;
        
        // ReSharper disable once StringLiteralTypo
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

    /// <summary>
    /// Deserializes XML data into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize the XML data into.</typeparam>
    /// <param name="xml">The XML data to be deserialized.</param>
    /// <returns>The deserialized object of type T, or null if the deserialization fails.</returns>
    public static T? DeserializeXml<T>(string xml) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xml);
        return (T?)serializer.Deserialize(reader);
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="degrees">The angle in degrees.</param>
    /// <returns>The angle converted to radians.</returns>
    public static double ConvertDegreesToRadians(double degrees) => (Math.PI / 180) * degrees;
    
    private static Polygon? ConvertToPolygon(Geometry? geom, double buffer)
    {
        while (true)
        {
            if (geom is null) return null;
            GeometryTransformer gt = new();
            var result = gt.Transform(BufferGeometry(geom, buffer));
            if (result.GeometryType.Equals(GisGeometries.Polygon, StringComparison.CurrentCultureIgnoreCase)) return (Polygon)result;
            geom = result.Envelope;
        }
    }

    /// <summary>
    /// Returns a buffered geometry based on the input geometry, buffer size, segment count, and end cap style.
    /// </summary>
    /// <param name="geom">The input geometry to buffer.</param>
    /// <param name="buffer">The buffer size in meters.</param>
    /// <param name="segment">The number of segments used to approximate a quarter circle.</param>
    /// <param name="style">The end cap style used in buffering.</param>
    /// <returns>The buffered geometry.</returns>
    public static Geometry BufferGeometry(Geometry geom, double buffer, int segment = 8, EndCapStyle style = EndCapStyle.Round) => 
        buffer > 0 ? geom.Buffer(buffer * ConvToMeters, segment, style) : geom;

    /// <summary>
    /// Checks if a given geometry intersects with the geometry of a feature. If they intersect, the feature's geometry is updated to be the intersection of the two geometries.
    /// </summary>
    /// <param name="feature">The feature whose geometry to check and update.</param>
    /// <param name="geom">The comparison geometry to check against.</param>
    public static void WithInFeatures(ref Feature feature, Geometry geom)
    {
        if (feature.Geometry.Intersects(geom))
            feature.Geometry = feature.Geometry.Intersection(geom);
    }

    /// <summary>
    /// Checks if a given feature is within the specified geometry and creates a new feature based on the intersection.
    /// </summary>
    /// <param name="geomFeature"></param>
    /// <param name="geom">The geometry to compare with.</param>
    /// <param name="srCode"></param>
    /// <param name="buffer"></param>
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
    /// Creates an empty Point geometry with the given spatial reference code.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="coordinates"></param>
    /// <returns>An empty Point geometry.</returns>
    public static Point CreatePoint(int srCode, Coordinate coordinates)
        => CreateGeometryFactory(srCode).CreatePoint(coordinates);

    /// <summary>
    /// Creates an empty point geometry with the specified spatial reference code.
    /// </summary>
    /// <param name="srCode">The spatial reference code for the geometry.</param>
    /// <param name="coordinates"></param>
    /// <returns>An empty point geometry.</returns>
    public static Point CreatePoint(int srCode, double[] coordinates)
    {
        if (coordinates[0] == 0 && coordinates[1] == 0)
            throw new Exception("I cannot create a geometric point with coordinates equal to 0");
        
        var newCoordinates =  new Coordinate(coordinates[0], coordinates[1]);
        return CreateGeometryFactory(srCode).CreatePoint(newCoordinates);
    }
        
    private static LineString CreateLine(int srCode) => CreateGeometryFactory(srCode).CreateLineString();

    /// <summary>
    /// Creates a LineString geometry based on the given spatial reference code and a list of coordinates.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="coordinates">The list of coordinates for the LineString.</param>
    /// <returns>A LineString object representing the created geometry.</returns>
    public static LineString CreateLine(int srCode, List<double[]> coordinates)
    {
        if (coordinates.Count == 0) return CreateLine(srCode);
        var newCoordinates = coordinates.Select(coordinate => new Coordinate(coordinate[0], coordinate[1])).ToArray();
        return CreateLine(srCode, newCoordinates);
    }

    /// <summary>
    /// Determines the SQL spatial function to be used based on the geometry type.
    /// </summary>
    /// <param name="geom">The input geometry.</param>
    /// <returns>The SQL spatial function to be used.</returns>
    public static string SqlFunctionSpatialByGeometry(Geometry geom)
    {
        var sqlSpatial = geom!.GeometryType.ToUpper().Equals("POINT") 
            ? "ST_Within"
            : "ST_Intersects";
        return sqlSpatial;
    }

    /// <summary>
    /// Creates a LineString geometry with the given spatial reference code and coordinates.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="coordinates">The coordinates to create the LineString from.</param>
    /// <returns>The LineString geometry.</returns>
    public static LineString CreateLine(int srCode, Coordinate[] coordinates) =>
        coordinates.Length == 0 ? CreateLine(srCode) : CreateGeometryFactory(srCode).CreateLineString(coordinates);
    
    private static LinearRing CreateLineRing(int srCode) => CreateGeometryFactory(srCode).CreateLinearRing();

    /// <summary>
    /// Creates a linear ring geometry based on a list of coordinate pairs.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="coordinates">The list of coordinate pairs.</param>
    /// <returns>A linear ring geometry object.</returns>
    public static LinearRing CreateLineRing(int srCode, List<double[]> coordinates)
    {
        if (coordinates.Count == 0) return CreateLineRing(srCode);
        var line = CreateLine(srCode, coordinates);
        if (!line.IsClosed) return CreateLineRing(srCode);
        var newCoordinates = coordinates.Select(coordinate => new Coordinate(coordinate[0], coordinate[1])).ToArray();
        return CreateGeometryFactory(srCode).CreateLinearRing(newCoordinates);
    }


    /// <summary>
    /// Creates a copy of a given geometry.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="geom">The geometry to be copied.</param>
    /// <returns>The copied geometry.</returns>
    public static Geometry CopyGeometry(int srCode, Geometry geom) =>
        CreateGeometryFactory(srCode).CreateGeometry(geom);

    /// <summary>
    /// Create empty polygon
    /// </summary>
    /// <param name="srCode"></param>
    /// <returns></returns>
    private static Geometry CreatePolygon(int srCode) => CreateGeometryFactory(srCode).CreateEmpty(Dimension.Surface);

    /// <summary>
    /// Creates a polygon geometry based on the given coordinates.
    /// </summary>
    /// <param name="srCode">The spatial reference code of the polygon.</param>
    /// <param name="coordinates">The array of coordinates to create the polygon.</param>
    /// <returns>The created polygon geometry.</returns>
    public static Polygon CreatePolygon(int srCode, Coordinate[] coordinates) => CreateGeometryFactory(srCode).CreatePolygon(coordinates);

    /// <summary>
    /// Creates a geometry object from a bounding box.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="env"></param>
    /// <returns>The geometry object created from the bounding box.</returns>
    public static Geometry CreateGeometryFromBBox(int srCode, Envelope env) => CreateGeometryFactory(srCode).ToGeometry(env);

    /// <summary>
    /// Creates a Geometry from the given bounding box coordinates.
    /// </summary>
    /// <param name="srCode">The spatial reference code of the geometry.</param>
    /// <param name="lngXMin">The minimum longitude value of the bounding box.</param>
    /// <param name="latYMin">The minimum latitude value of the bounding box.</param>
    /// <param name="lngXMax">The maximum longitude value of the bounding box.</param>
    /// <param name="latYMax">The maximum latitude value of the bounding box.</param>
    /// <returns>The created Geometry.</returns>
    public static Geometry CreateGeometryFromBBox(int srCode, double lngXMin, double latYMin, double lngXMax, double latYMax)
        => CreateGeometryFromBBox(srCode, new Envelope(new Coordinate(lngXMin, latYMin),
            new Coordinate(lngXMax, latYMax)));

    /// <summary>
    /// Validates a geometry by converting it into a valid polygon if it is a LineString and not already valid.
    /// </summary>
    /// <param name="geom">The geometry to be validated.</param>
    /// <param name="srCode">The spatial reference code of the geometry.</param>
    /// <returns>The validated geometry. If the input is a LineString and not valid, it is converted into a valid polygon. Otherwise, the input geometry is returned.</returns>
    public static Geometry ValidateGeometry(Geometry geom, int srCode)
    {
        if (!geom.GeometryType.Equals(GisGeometries.LineString, StringComparison.CurrentCultureIgnoreCase) && geom.IsValid) return geom;
        var lr = new LinearRing(geom.Coordinates);
        var newGeom = lr.IsClosed ? CreatePolygon(srCode, geom.Coordinates) : 
            CreatePolygonFromEnvelope(srCode, geom.EnvelopeInternal!);
        return newGeom;
    }

    /// <summary>
    /// Calculates the intersection of two geometries and returns the result as a new geometry.
    /// </summary>
    /// <param name="geom1">The first geometry.</param>
    /// <param name="geom2">The second geometry.</param>
    /// <param name="buffer">The buffer value to use for reducing the geometries, or null if no buffer should be applied.</param>
    /// <returns>The intersection of the two geometries as a new geometry, or null if there is no intersection.</returns>
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
    /// Converts the area geometry from square meters to the specified unit.
    /// </summary>
    /// <param name="area">The area in square meters to be converted.</param>
    /// <returns>The converted area.</returns>
    public static double ConvertAreaGeometry(double area) => Math.Round((float)area / Math.Pow(ConvToMeters, 2), 2);

    /// <summary>
    /// Converts the length of a geometry from the default unit (meters) to the desired unit.
    /// </summary>
    /// <param name="lenght"></param>
    /// <returns>The converted length in the desired unit.</returns>
    public static double ConvertLenghtGeometry(double lenght) => Math.Round((float)lenght / ConvToMeters, 2);

    /// <summary>
    /// Reduces the precision of a geometry.
    /// </summary>
    /// <param name="geom">The geometry to be reduced.</param>
    /// <returns>The reduced geometry.</returns>
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
    /// Adds a property to a feature by name and value.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="feature">The feature to add the property to.</param>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
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

    /// <summary>
    /// Copies all features from the origin FeatureCollection to the destination FeatureCollection.
    /// </summary>
    /// <param name="origin">The source FeatureCollection from which features will be copied.</param>
    /// <param name="destination">The target FeatureCollection where features will be copied to.</param>
    /// <returns>The resulting FeatureCollection after copying all features from the origin to the destination.</returns>
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
    /// Creates a feature collection based on an array of features.
    /// </summary>
    /// <param name="features">The array of features to be included in the collection.</param>
    /// <returns>A feature collection containing the given features.</returns>
    public static FeatureCollection CreateFeatureCollection(Feature[]? features)
    {
        FeatureCollection fColl = [];
        if (features is null || features.Length == 0) return fColl;
        
        foreach (var f in features)
            fColl.Add(f);
        return fColl;
    }

    /// <summary>
    /// Returns the bounding box coordinates of a geometry in the format "minY,minX,maxY,maxX".
    /// </summary>
    /// <param name="geom">The geometry for which to calculate the bounding box.</param>
    /// <returns>The bounding box coordinates of the geometry in the format "minY,minX,maxY,maxX".</returns>
    public static string CoordinatesBBoxFromGeometry(Geometry geom)
    {
        var envBBox = geom.EnvelopeInternal;
        return string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:F8},{1:F8},{2:F8},{3:F8}", envBBox.MinY, envBBox.MinX, envBBox.MaxY, envBBox.MaxX);
    }

    /// <summary>
    /// Creates an Envelope object from the given bounding box coordinates.
    /// </summary>
    /// <param name="minLat">The minimum latitude of the bounding box.</param>
    /// <param name="minLon">The minimum longitude of the bounding box.</param>
    /// <param name="maxLat">The maximum latitude of the bounding box.</param>
    /// <param name="maxLon">The maximum longitude of the bounding box.</param>
    /// <returns>An Envelope object representing the bounding box.</returns>
    public static Envelope CreateEnvelopeFromBBox(double minLat, double minLon, double maxLat, double maxLon)
    {
        var minCoordinate = ConvertToCoordinate(minLon, minLat);
        var maxCoordinate = ConvertToCoordinate(maxLon, maxLat);
        return new Envelope(minCoordinate, maxCoordinate);
    }

    /// <summary>
    /// Creates an Envelope object from the given bounding box coordinates.
    /// </summary>
    /// <returns>An Envelope object representing the bounding box.</returns>
    public static Envelope CreateEnvelopeFromBBox(double[] bbox)
        => CreateEnvelopeFromBBox(bbox[0], bbox[1], bbox[2], bbox[3]);

    /// <summary>
    /// Converts longitude and latitude to a coordinate.
    /// </summary>
    /// <param name="lon">The longitude value.</param>
    /// <param name="lat">The latitude value.</param>
    /// <returns>The coordinate representing the converted longitude and latitude.</returns>
    private static Coordinate ConvertToCoordinate(double lon, double lat) =>
        new(Convert.ToDouble(lon, CultureInfo.InvariantCulture), Convert.ToDouble(lat, CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates a feature that represents the boundary of a given geometry.
    /// </summary>
    /// <param name="geom">The geometry for which to create the boundary feature.</param>
    /// <param name="srCode">The spatial reference code to use for the boundary feature.</param>
    /// <returns>The created boundary feature.</returns>
    public static Feature CreateBoundary(Geometry geom, int srCode)
    {
        // calculate boundary
        var featureWithBoundary = CreateEmptyFeature(srCode, BufferGeometry(geom, 0).Boundary);
        AddProperty(ref featureWithBoundary, "Name", "Boundary");
        return featureWithBoundary;
    }

    /// <summary>
    /// Adjusts a single quote in a given string value by replacing it with two single quotes.
    /// </summary>
    /// <param name="value">The string value to adjust.</param>
    /// <returns>The adjusted string value with two single quotes replacing the single quote.</returns>
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
    /// Retrieves the raw content of a GeoJSON file.
    /// </summary>
    /// <param name="geoJsonFile">The name of the GeoJSON file to retrieve.</param>
    /// <returns>The raw content of the specified GeoJSON file.</returns>
    private static string GetRawGeoJson(string geoJsonFile)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeoJson");
        return GetRawContent(geoJsonFile, path);
    }

    /// <summary>
    /// Retrieves the raw content of a GeoJSON file as a string.
    /// </summary>
    /// <param name="geoJsonFile">The name of the GeoJSON file.</param>
    /// <param name="pathDir">The directory path where the GeoJSON file is located.</param>
    /// <returns>The raw content of the GeoJSON file as a string.</returns>
    private static string GetRawGeoJson(string geoJsonFile, string pathDir) => GetRawContent(geoJsonFile, pathDir);

    private static string GetRawContent(string fileName, string path)
    {
        var pathDir = Path.Combine(path, fileName);
        return File.ReadAllText(pathDir);
    }

    /// <summary>
    /// Gets a feature collection from a GeoJSON file.
    /// </summary>
    /// <param name="geoJsonFileName">The name of the GeoJSON file.</param>
    /// <returns>The feature collection parsed from the GeoJSON file.</returns>
    public static FeatureCollection? GetFeatureCollectionByGeoJson(string geoJsonFileName)
    {
        var pathGeoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeoJson");
        var fileGeoJson = Path.Combine(pathGeoJson, geoJsonFileName);
        using var streamGeoJson = File.OpenRead(fileGeoJson);
        return JsonSerializer.Deserialize<FeatureCollection>(streamGeoJson);
    }

    /// <summary>
    /// Retrieves a feature collection from a GeoJSON file.
    /// </summary>
    /// <param name="geoJsonFileName">The name of the GeoJSON file.</param>
    /// <param name="pathDir">The directory path where the GeoJSON file is located.</param>
    /// <returns>A feature collection parsed from the GeoJSON file.</returns>
    public static FeatureCollection GetFeatureCollectionByGeoJson(string geoJsonFileName, string pathDir)
    {
        var geoJson = AdjustSingleQuote(GetRawGeoJson(geoJsonFileName, pathDir));
        return NetCore.DeserializeString<FeatureCollection>(geoJson)!;
    }
    
    /// <summary>
    /// Serializes a FeatureCollection object to a JSON string.
    /// </summary>
    /// <param name="featureCollection">The FeatureCollection object to serialize.</param>
    /// <returns>A JSON string representing the serialized FeatureCollection.</returns>
    public static string SerializeFeatureCollection(FeatureCollection? featureCollection)
    {
        return SerializeObject(featureCollection);
    }
    
    /// <summary>
    /// Serializes a Feature object to a JSON string.
    /// </summary>
    /// <param name="feature">The Feature object to serialize. Can be null.</param>
    /// <returns>A JSON string representing the serialized Feature.</returns>
    public static string SerializeFeature(Feature? feature)
    {
        return SerializeObject(feature);
    }
    
    /// <summary>
    /// Serializes an object of type T to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize. Can be null.</param>
    /// <returns>A JSON string representing the serialized object.</returns>
    public static string SerializeObject<T>(T? obj) where T : class
    {
        var geoJsonWriter = new GeoJsonWriter();
        return geoJsonWriter.Write(obj);
    }
    
    /// <summary>
    /// Deserializes a JSON string into a FeatureCollection object.
    /// </summary>
    /// <param name="geoJson">The JSON string representing the FeatureCollection.</param>
    /// <returns>The deserialized FeatureCollection object.</returns>
    public static FeatureCollection DeserializeFeatureCollection(string geoJson)
    {
        return DeserializeObject<FeatureCollection>(geoJson);
    }

    /// <summary>
    /// Deserializes a JSON string into a Feature object.
    /// </summary>
    /// <param name="geoJson">The JSON string representing the Feature.</param>
    /// <returns>The deserialized Feature object.</returns>
    public static Feature DeserializeFeature(string geoJson)
    {
        return DeserializeObject<Feature>(geoJson);
    }

    /// <summary>
    /// Deserializes a JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="geoJson">The JSON string representing the object.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T DeserializeObject<T>(string geoJson) where T : class
    {
        var geoJsonReader = new GeoJsonReader();
        return geoJsonReader.Read<T>(geoJson);
    }

    /// <summary>
    /// Reads features from an ESRI file at the specified path using the Shapefile class.
    /// </summary>
    /// <param name="path">The path to the ESRI file.</param>
    /// <param name="key">The key used for reading the features. This parameter is not used in the method.</param>
    /// <returns>
    /// An enumerable collection of IFeature objects representing the features read from the ESRI file.
    /// </returns>
    public static IEnumerable<IFeature> ReadFeaturesByEsriFile(string path, string key)
        => Shapefile.ReadAllFeatures(path).AsEnumerable();

    /// <summary>
    /// Creates a geometry object from a filter string and an optional buffer value.
    /// </summary>
    /// <param name="geomFilter">The filter string defining the geometry.</param>
    /// <param name="buffer">The buffer distance to apply to the geometry (optional).</param>
    /// <returns>The created geometry object.</returns>
    public static Geometry? CreateGeometryFromFilter(string geomFilter, double? buffer)
    {
        var geom = NetCore.DeserializeString<Geometry>(geomFilter);
        
        // check if Geom is valid and convert to polygon 
        if (geom is not null && !geom.IsEmpty && !IsPolygon(geom) && buffer > 0) 
            geom = BufferGeometry(geom, (double)buffer);
        
        return geom;
    }

    /// <summary>
    /// Calculates the centre point of a given FeatureCollection based on its bounding box.
    /// </summary>
    /// <param name="features">The FeatureCollection for which to calculate the centre.</param>
    /// <param name="srCode">The spatial reference code (SRCode) used for creating the centre point geometry.</param>
    /// <returns>The centre point of the FeatureCollection as a Point object.</returns>
    public static Point CalculateCentre(FeatureCollection features, int srCode)
    {
        var center = features.BoundingBox.Centre;
        return CreatePoint(srCode, center);
    }
    
}