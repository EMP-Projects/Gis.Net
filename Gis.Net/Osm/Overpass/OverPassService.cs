using Gis.Net.Core;
using Gis.Net.Osm;
using Gis.Net.Osm.Overpass.Dto;
using Gis.Net.Vector;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents a service for interacting with the OverPass API.
/// </summary>
public class OverPassService : IOverPass
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="OverPassService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used to make requests to the OverPass API.</param>
    public OverPassService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets or sets the list of OverPass elements.
    /// </summary>
    private List<OverPassElement>? Elements { get; set; } =
    [
        new OverPassElement(EOsmTag.Amenity),
        new OverPassElement(EOsmTag.AerialWay),
        new OverPassElement(EOsmTag.AeroWay),
        new OverPassElement(EOsmTag.Boundary),
        new OverPassElement(EOsmTag.Building),
        new OverPassElement(EOsmTag.Denomination),
        new OverPassElement(EOsmTag.Emergency),
        new OverPassElement(EOsmTag.Historic),
        new OverPassElement(EOsmTag.Highway),
        new OverPassElement(EOsmTag.LandUse),
        new OverPassElement(EOsmTag.Leisure),
        new OverPassElement(EOsmTag.Military),
        new OverPassElement(EOsmTag.Natural),
        new OverPassElement(EOsmTag.Office),
        new OverPassElement(EOsmTag.Place),
        new OverPassElement(EOsmTag.Railway),
        new OverPassElement(EOsmTag.Religion),
        new OverPassElement(EOsmTag.Shop),
        new OverPassElement(EOsmTag.Sport),
        new OverPassElement(EOsmTag.Tourism),
        new OverPassElement(EOsmTag.Tourist),
        new OverPassElement(EOsmTag.Waterway)
    ];

    /// <summary>
    /// Sets the query for each OverPass element.
    /// </summary>
    /// <param name="value">The dictionary containing the query parameters.</param>
    private void SetQuery(Dictionary<string, List<string>> value)
    {
        if (Elements is null) return;
        foreach (var element in Elements)
            element.Query = value;
    }

    /// <summary>
    /// Generates the payload for the OverPass query based on the provided geometry.
    /// </summary>
    /// <param name="geom">The geometry to use in the query.</param>
    /// <returns>The generated payload string.</returns>
    private string Payload(Geometry geom)
    {
        return Elements is null 
            ? string.Empty 
            : Elements.Aggregate(string.Empty, (current, element) => current + element.GetPayload(geom));
    }

    /// <summary>
    /// Creates a feature based on the provided options and OSM property.
    /// </summary>
    /// <param name="options">The options for creating the feature.</param>
    /// <param name="osmProperty">The OSM property to use for creating the feature.</param>
    /// <returns>The created feature.</returns>
    private static Feature CreateFeature(OverPassOptions options, OsmDto osmProperty)
    {
        var feature = GisUtility.CreateEmptyFeature((int)options.SrCode!);
        // check properties
        var attributes = GetPropertiesFromElement(osmProperty.Element, options);

        // if element don't contains in query create empty feature
        if (attributes is null)
            return feature;

        // create geometry and intersections with options
        var newGeom = GisUtility.IntersectionsGeometries(osmProperty.Geom, options.Geom!, osmProperty.IsPolygon ? 0 : options.Buffer);
        if (newGeom is null)
            return feature;

        // create new Feature
        feature.Geometry = newGeom;

        // add properties
        feature.Attributes = new AttributesTable(attributes);
            
        // delete null values
        GisUtility.DeleteNullProperties(ref feature);

        // invoke all delegate options
        options.OnCreatedFeature?.Invoke(feature);

        return feature;
    }

    /// <summary>
    /// Fetches OverPass data based on the provided payload.
    /// </summary>
    /// <param name="payload">The payload for the OverPass query.</param>
    /// <returns>The OverPass response data.</returns>
    /// <exception cref="ArgumentException">Thrown when the payload is null or empty.</exception>
    private Task<OverPassResponseDto?> FetchOverpassData(string payload)
    {
        if (string.IsNullOrEmpty(payload))
            throw new ArgumentException("Payload is required to execute overpass queries");
        
        const string queryStart = "[out:json][timeout:25];(";
        const string queryEnd = ");out geom;>;out skel qt;";
        var q = $"{queryStart}{payload}{queryEnd}";
        return FetchFeaturesFromOsm<OverPassResponseDto>($"{_httpClient.BaseAddress?.ToString()}/api/interpreter", q);
    }

    /// <summary>
    /// Fetches features from OpenStreetMap based on the provided URL and query.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="url">The URL to send the request to.</param>
    /// <param name="query">The query to send in the request.</param>
    /// <returns>The fetched features.</returns>
    /// <exception cref="Exception">Thrown when the request is not successful.</exception>
    private async Task<T?> FetchFeaturesFromOsm<T>(string url, string query) where T : class
    {
        var body = new Dictionary<string, string>
        {
            {"data", $"{query}"}
        };

        using var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(body));
        if (!response.IsSuccessStatusCode)
            throw new Exception("I couldn't read geometry from OpenStreetMap");
        
        var responseStr = await response.Content.ReadAsStringAsync();
        return NetCore.DeserializeString<T>(responseStr);
    }

    /// <summary>
    /// Finds geometry from OpenStreetMap by intersecting with the provided geometry.
    /// </summary>
    /// <param name="options">The options for the OverPass query.</param>
    /// <returns>A collection of features that intersect with the provided geometry.</returns>
    /// <exception cref="ArgumentException">Thrown when the geometry is not valid.</exception>
    /// <exception cref="Exception">Thrown when an error occurs during the query.</exception>
    public virtual async Task<FeatureCollection?> Intersects(OverPassOptions options)
    {
        // get query;
        if (options.OnQuery is not null)
            options.Query = await options.OnQuery.Invoke();
        
        SetQuery(options.Query);

        if (options.Geom is null || options.Geom.IsEmpty)
            throw new ArgumentException("Geometry is not valid to execute overpass queries");
        
        // read feature by query overpass
        var featuresOsm = await FetchOverpassData(Payload(options.Geom!));

        if (featuresOsm is null)
            return null;

        List<OsmDto> elements = new();

        // get all points
        var nodes = Nodes((int)options.SrCode!, featuresOsm);
        if (nodes is not null)
            elements.AddRange(nodes.ToList()!);
        
        // get all polygons
        var polygons = CreateOsmPolygons((int)options.SrCode!, featuresOsm);
        if (polygons is not null)
            elements.AddRange(polygons.ToList()!);

        var listFeatures = elements
            .AsParallel()
            .Select(e => CreateFeature(options, e))
            .Where(f => !f.Geometry.IsEmpty)
            .ToArray();

        listFeatures = CalculateFeatureDifference(listFeatures.ToList(), options)?.ToArray();
        return GisUtility.CreateFeatureCollection(listFeatures!);
    }

    /// <summary>
    /// Gets the coordinates from the provided element.
    /// </summary>
    /// <param name="e">The element to get the coordinates from.</param>
    /// <returns>A list of coordinates.</returns>
    private static List<double[]>? GetCoordinates(Element? e) 
        => e?.Geometry?.Select(g => new[] { (double)g.Lon!, (double)g.Lat! }).ToList();

    /// <summary>
    /// Gets the nodes from the OverPass response.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="features">The OverPass response data.</param>
    /// <returns>A collection of OSM DTOs representing the nodes.</returns>
    private static IEnumerable<OsmDto?>? Nodes(int srCode, OverPassResponseDto features)
    {
        var result = features.Elements!.Where(e => e?.Type?.ToUpper() == "NODE")
            .Where(e => e is not null)
            .Select(e => new OsmDto(e!,
                GisUtility.CreatePoint(srCode, new[]
                {
                    Convert.ToDouble(e?.Lon), 
                    Convert.ToDouble(e?.Lat)
                })));
        return FilterElements(result);
    }

    /// <summary>
    /// Gets the ways from the OverPass response.
    /// </summary>
    /// <param name="features">The OverPass response data.</param>
    /// <returns>A collection of elements representing the ways.</returns>
    private static IEnumerable<Element?>? Ways(OverPassResponseDto features) =>
        features.Elements?
            .AsParallel()
            .Where(e => e?.Type is not null && e.Type.ToUpper().Equals("WAY"))
            .AsEnumerable();

    /// <summary>
    /// Filters the provided elements to remove null or empty geometries.
    /// </summary>
    /// <param name="elements">The elements to filter.</param>
    /// <returns>A collection of filtered elements.</returns>
    private static IEnumerable<OsmDto?>? FilterElements(IEnumerable<OsmDto?>? elements) 
        => elements?.AsParallel()
                    .Where(l => l is not null)
                    .Where(l => l is { Geom.IsEmpty: false })
                    .AsEnumerable();

    /// <summary>
    /// Creates OSM polygons from the OverPass response.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="features">The OverPass response data.</param>
    /// <param name="toWebMercator">Indicates whether to convert to Web Mercator.</param>
    /// <returns>A collection of OSM DTOs representing the polygons.</returns>
    private static IEnumerable<OsmDto?>? CreateOsmPolygons(int srCode, OverPassResponseDto features, bool toWebMercator = false)
    {
        var elements = Ways(features);
        var result = elements?
            .Select(e =>
            {
                // var coordinates = GetCoordinates(e);
                var coordinates = e?.Geometry?.Select(g => new Coordinate((double)g.Lon!, (double)g.Lat!)).ToArray();
                if (coordinates is null) 
                    return null;
                var line = GisUtility.CreateLine(srCode, coordinates);
                return e is not null
                    ? new OsmDto(e, line.IsClosed ? GisUtility.CreatePolygon(srCode, coordinates) : line, line.IsClosed)
                    : null;
            })
            .AsParallel()
            .Where(l => l is not null)
            .Where(l => !l!.Geom.IsEmpty)
            .AsEnumerable();
        return FilterElements(result);
    }

    /// <summary>
    /// Creates OSM lines from the OverPass response.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="features">The OverPass response data.</param>
    /// <returns>A collection of OSM DTOs representing the lines.</returns>
    private static IEnumerable<OsmDto?>? Lines(int srCode, OverPassResponseDto features)
    {
        var elements = Ways(features);
        var result = elements?
            .Select(e =>
            {
                var coordinates = GetCoordinates(e);
                if (coordinates is null) return null;
                var line = GisUtility.CreateLine(srCode, coordinates);
                return !line.IsClosed && e is not null ? new OsmDto(e, line) : null;
            });
        return FilterElements(result);
    }

    /// <summary>
    /// Creates OSM line rings from the OverPass response.
    /// </summary>
    /// <param name="srCode">The spatial reference code.</param>
    /// <param name="features">The OverPass response data.</param>
    /// <returns>A collection of OSM DTOs representing the line rings.</returns>
    private static IEnumerable<OsmDto?>? LinesRing(int srCode, OverPassResponseDto features)
    {
        var elements = Ways(features);
        var result = elements?
            .Select(e =>
            {
                var coordinates = GetCoordinates(e);
                if (coordinates is null) return null;
                var lineRing = GisUtility.CreateLineRing(srCode, coordinates);
                return e is not null 
                    ? new OsmDto(e, lineRing) 
                    : null;
            });
        return FilterElements(result);
    }

    /// <summary>
    /// Calculates the difference between the provided features and the options geometry.
    /// </summary>
    /// <param name="features">The features to calculate the difference for.</param>
    /// <param name="options">The options for the OverPass query.</param>
    /// <returns>A list of features with the calculated difference.</returns>
    private static List<Feature>? CalculateFeatureDifference(List<Feature>? features, OverPassOptions options)
    {
        // add difference feature
        var geomDiff =
            GisUtility.CalculateGeometryDifference(features!.AsEnumerable(), options.Geom!);
        
        if (geomDiff is null)
            return features;

        var featureDiff = GisUtility.CreateEmptyFeature((int)options.SrCode!, geomDiff, 0);
        GisUtility.AddProperty(ref featureDiff, "Name", "Difference");
        options.OnCreatedFeature?.Invoke(featureDiff);
        features?.Add(featureDiff);
        return features;
    }

    /// <summary>
    /// Gets the properties from the provided element based on the options.
    /// </summary>
    /// <param name="e">The element to get the properties from.</param>
    /// <param name="options">The options for the OverPass query.</param>
    /// <returns>A dictionary of properties.</returns>
    private static Dictionary<string, object?>? GetPropertiesFromElement(Element? e, OverPassOptions options)
    {
        Dictionary<string, object?> attributes = new();
            
        var properties = e?.Tags;
            
        if (properties is null) return null;

        var propertyValue = properties.ToString()!;
        var listAttributes = OverPassTags.ParseTags(propertyValue, options)?.ToList();

        attributes.TryAdd("Name", "Osm");
            
        if (listAttributes?.Count > 0)
            attributes.TryAdd("Query", listAttributes);
            
        return attributes;
    }

    /// <summary>
    /// Gets the properties from the provided element.
    /// </summary>
    /// <param name="e">The element to get the properties from.</param>
    /// <returns>An attributes table containing the properties.</returns>
    private static AttributesTable GetPropertiesFromElement(Element e)
    {
        AttributesTable attributes = new ();
        if (e.Id is not null)
            attributes.Add("id", e.Id);
        if (e.Tags is null) return attributes;
        foreach (var p in e.Tags.GetType().GetProperties())
        {
            var value = p.GetValue(e.Tags, null);
            if (value is not null)
                attributes.Add(p.Name, value);
        }

        return attributes;
    }
}