using System.Net.Http.Headers;
using System.Xml.Serialization;
using Gis.Net.Nominatim;
using Gis.Net.Nominatim.Dto;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace Gis.Net.Nominatim.Service;

/// <inheritdoc />
public abstract class Nominatim<T>: INominatim<T> where T : class
{
    /// <summary>
    /// Represents an HttpClient used for making HTTP requests.
    /// </summary>
    protected readonly HttpClient HttpClient;
    
    protected Nominatim(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    /// <summary>
    /// Represents the property for the path in the Nominatim service.
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    /// Provides limitations for querying Nominatim service.
    /// </summary>
    protected IResultLimitations? Limitations { get; set; }

    /// <summary>
    /// Represents a request for retrieving location information from the Nominatim service.
    /// </summary>
    protected INominatimRequest? Request { get; set; }

    /// <summary>
    /// Deserialize the provided XML string into an instance of type T.
    /// </summary>
    /// <typeparam name="T">The type to deserialize.</typeparam>
    /// <param name="xml">The XML string to deserialize.</param>
    /// <returns>The deserialized instance of type T.</returns>
    private static T? DeSerializeToXml(string xml)
    {
        var serializer = new XmlSerializer(typeof(T));
        using TextReader reader = new StringReader(xml);
        var result = (T?)serializer.Deserialize(reader);
        return result;
    }

    /// <summary>
    /// Deserialize the given JSON string representing a FeatureCollection to a NetTopologySuite.Features.FeatureCollection object.
    /// </summary>
    /// <param name="features">The JSON string representing a FeatureCollection.</param>
    /// <returns>The deserialized FeatureCollection object. Returns null if the input string is null.</returns>
    private static FeatureCollection? DeSerializeFeatureCollection(string? features)
    {
        if (features is null) return null;
        var serializer = GeoJsonSerializer.Create();
        using var stringReader = new StringReader(features);
        using var jsonReader = new JsonTextReader(stringReader);
        return serializer.Deserialize<FeatureCollection>(jsonReader);
    }

    /// <summary>
    /// Deserializes a JSON array to an array of NominatimResponse objects.
    /// </summary>
    /// <param name="features">The JSON representation of the features to deserialize.</param>
    /// <returns>An array of NominatimResponse objects.</returns>
    private static NominatimResponse[]? DeSerializeToJsonArray(string? features)
    {
        if (features is null) return null;
        var serializer = GeoJsonSerializer.Create();
        using var stringReader = new StringReader(features);
        using var jsonReader = new JsonTextReader(stringReader);
        return serializer.Deserialize<NominatimResponse[]>(jsonReader);
    }

    /// <summary>
    /// Returns a query string from a list of strings.
    /// Each element in the list is concatenated with & character, and the resulting string is prefixed with "?".
    /// </summary>
    /// <param name="query">The list of strings to be concatenated.</param>
    /// <returns>The query string generated from the list.</returns>
    private static string GetQueryFromList(IEnumerable<string> query)
    {
        var result = "?";

        var enumerable = query.ToList();
        for (var i = 0; i < enumerable.Count; i++)
        {
            result += enumerable[i];
            if (i < enumerable.Count - 1)
                result += "&";
        }

        return result;
    }

    /// <summary>
    /// Retrieves data from the Nominatim service.
    /// </summary>
    /// <param name="format">The format in which the data should be returned.</param>
    /// <param name="limitations">The limitations of the result.</param>
    /// <param name="request">The request parameters.</param>
    /// <returns>The retrieved data as a string.</returns>
    private async Task<string> GetData(string format, IResultLimitations limitations, INominatimRequest request)
    {
        Limitations = limitations;
        Request = request;

        var queryList = QueryParams();

        // result limitation 
        SetResultLimitations(ref queryList);
        // set output details 
        SetOutputDetails(ref queryList);
        SetPolygonOutput(ref queryList);
        SetFormatOutput(ref queryList, format);

        var url = $"{HttpClient.BaseAddress}{GetQueryFromList(queryList)}";
        var productValue = new ProductInfoHeaderValue("Nominatim", "1.0");
        HttpClient.DefaultRequestHeaders.UserAgent.Add(productValue);
        using var response = await HttpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();
        throw new NominatimExceptions("I couldn't read the data");
    }

    /// <summary>
    /// Generates a list of query parameters based on the specific implementation of the QueryParams method in the derived class.
    /// </summary>
    /// <returns>A list of query parameters.</returns>
    protected abstract List<string> QueryParams();

    /// <summary>
    /// Sets the result limitations for the Nominatim query.
    /// </summary>
    /// <param name="qList">The list of query parameters.</param>
    public abstract void SetResultLimitations(ref List<string> qList);

    /// <summary>
    /// Sets the output to include polygon SVG data.
    /// </summary>
    /// <param name="qList">The list of query parameters.</param>
    private static void SetPolygonOutput(ref List<string> qList) => qList.Add("polygon_svg=1");

    /// <summary>
    /// Sets the output details for the Nominatim API request.
    /// </summary>
    /// <param name="qList">The list of query parameters.</param>
    private static void SetOutputDetails(ref List<string> qList)
    {
        qList.Add("addressdetails=1");
        qList.Add("extratags=1");
        qList.Add("namedetails=1");
    }

    /// <summary>
    /// Sets the format of the output data.
    /// </summary>
    /// <param name="qList">The query parameter list.</param>
    /// <param name="output">The format of the output data.</param>
    private static void SetFormatOutput(ref List<string> qList, string output) 
        => qList.Add($"format={output}");

    /// <inheritdoc />
    public virtual async Task<FeatureCollection?> ToGeoJson(IResultLimitations limitations, INominatimRequest request) 
        => DeSerializeFeatureCollection(await GetData(NominatimOutputFormats.GeoJson, limitations, request));

    /// <inheritdoc />
    public virtual async Task<NominatimResponse[]?> ToJson(IResultLimitations limitations, INominatimRequest request) 
        => DeSerializeToJsonArray(await GetData(NominatimOutputFormats.Json, limitations, request));

    /// <inheritdoc />
    public async Task<T?> ToXml(IResultLimitations limitations, INominatimRequest request) 
        => DeSerializeToXml(await GetData(NominatimOutputFormats.Xml, limitations, request));
}