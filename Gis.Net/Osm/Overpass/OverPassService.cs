using Gis.Net.Core;
using Gis.Net.Osm;
using Gis.Net.Osm.Overpass.Dto;
using Gis.Net.Vector;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using TeamSviluppo.Gis;
using TeamSviluppo.Gis.Overpass;

namespace Gis.Net.Osm.Overpass;

/// <inheritdoc />
public class OverPassService : IOverPass
{
    private readonly HttpClient _httpClient;
    public OverPassService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
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
    
    private void SetQuery(Dictionary<string, List<string>> value)
    {
        if (Elements is null) return;
        foreach (var element in Elements)
            element.Query = value;
    }

    private string Payload(Geometry geom)
    {
        return Elements is null 
            ? string.Empty 
            : Elements.Aggregate(string.Empty, (current, element) => current + element.GetPayload(geom));
    }
    
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
    
    private Task<OverPassResponseDto?> FetchOverpassData(string payload)
    {
        if (string.IsNullOrEmpty(payload))
            throw new ArgumentException("Payload is required to execute overpass queries");
        
        const string queryStart = "[out:json][timeout:25];(";
        const string queryEnd = ");out geom;>;out skel qt;";
        var q = $"{queryStart}{payload}{queryEnd}";
        return FetchFeaturesFromOsm<OverPassResponseDto>($"{_httpClient.BaseAddress?.ToString()}/api/interpreter", q);
    }
    
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
    /// Find geometry from Openstreetmap by intersect with geometry
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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
    
    private static List<double[]>? GetCoordinates(Element? e) 
        => e?.Geometry?.Select(g => new[] { (double)g.Lon!, (double)g.Lat! }).ToList();

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

    private static IEnumerable<Element?>? Ways(OverPassResponseDto features) =>
        features.Elements?
            .AsParallel()
            .Where(e => e?.Type is not null && e.Type.ToUpper().Equals("WAY"))
            .AsEnumerable();

    private static IEnumerable<OsmDto?>? FilterElements(IEnumerable<OsmDto?>? elements) 
        => elements?.AsParallel()
                    .Where(l => l is not null)
                    .Where(l => l is { Geom.IsEmpty: false })
                    .AsEnumerable();
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