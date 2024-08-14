using System.Net.Http.Headers;
using System.Xml.Serialization;
using Gis.Net.Nominatim;
using Gis.Net.Nominatim.Dto;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace Gis.Net.Nominatim.Service;

public abstract class Nominatim<T>: INominatim<T> where T : class
{
    protected readonly HttpClient HttpClient;
    protected Nominatim(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public string Path { get; set; } = "";
    
    protected IResultLimitations? Limitations { get; set; }

    protected INominatimRequest? Request { get; set; }


    private static T? DeSerializeToXml(string xml)
    {
        var serializer = new XmlSerializer(typeof(T));
        using TextReader reader = new StringReader(xml);
        var result = (T?)serializer.Deserialize(reader);
        return result;
    }
    
    private static FeatureCollection? DeSerializeFeatureCollection(string? features)
    {
        if (features is null) return null;
        var serializer = GeoJsonSerializer.Create();
        using var stringReader = new StringReader(features);
        using var jsonReader = new JsonTextReader(stringReader);
        return serializer.Deserialize<FeatureCollection>(jsonReader);
    }

    private static NominatimResponse[]? DeSerializeToJsonArray(string? features)
    {
        if (features is null) return null;
        var serializer = GeoJsonSerializer.Create();
        using var stringReader = new StringReader(features);
        using var jsonReader = new JsonTextReader(stringReader);
        return serializer.Deserialize<NominatimResponse[]>(jsonReader);
    }
    
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

    protected abstract List<string> QueryParams();

    public abstract void SetResultLimitations(ref List<string> qList);
    
    private static void SetPolygonOutput(ref List<string> qList) => qList.Add("polygon_svg=1");
    
    private static void SetOutputDetails(ref List<string> qList)
    {
        qList.Add("addressdetails=1");
        qList.Add("extratags=1");
        qList.Add("namedetails=1");
    }
    
    private static void SetFormatOutput(ref List<string> qList, string output) 
        => qList.Add($"format={output}");
    
    public virtual async Task<FeatureCollection?> ToGeoJson(IResultLimitations limitations, INominatimRequest request) 
        => DeSerializeFeatureCollection(await GetData(NominatimOutputFormats.GeoJson, limitations, request));
    
    public virtual async Task<NominatimResponse[]?> ToJson(IResultLimitations limitations, INominatimRequest request) 
        => DeSerializeToJsonArray(await GetData(NominatimOutputFormats.Json, limitations, request));
    
    public async Task<T?> ToXml(IResultLimitations limitations, INominatimRequest request) 
        => DeSerializeToXml(await GetData(NominatimOutputFormats.Xml, limitations, request));
}