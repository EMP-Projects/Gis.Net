using Gis.Net.Core;
using Gis.Net.Vector;
using NetTopologySuite.Features;

namespace Gis.Net.Wfs;

/// <inheritdoc />
public class WfsService : IWfsService
{
    private readonly HttpClient _httpClient;
    private const string Version = "version=2.0.0";

    /// <summary>
    /// Represents a service that interacts with a Web Feature Service (WFS).
    /// </summary>
    public WfsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<string> GetCapabilities(string version)
    {
        var uri = $"&{version}&request=GetCapabilities";
        return await _httpClient.GetStringAsync(uri);
    }

    /// <inheritdoc />
    public async Task<string> GetCapabilities() => await GetCapabilities(Version);

    private string GetWgsUri(WfsOptions options)
    {
        if (string.IsNullOrEmpty(options.Layer))
            throw new ArgumentException("Layer are required");

        options.Version ??= Version;
        var uri = $"{_httpClient.BaseAddress}&{options.Version}&request=GetFeature&typeName={options.Layer}&srs=EPSG:{(int)ESrCode.WebMercator}";

        if (options.BBox is not null && !Array.Empty<string>().Equals(options.BBox))
            uri += $"&bbox={string.Join(",", options.BBox)}";
        return uri;
    }

    /// <inheritdoc />
    public async Task<string> GetFeature(WfsOptions options)
    {
        var uri = GetWgsUri(options);
        return await _httpClient.GetStringAsync(uri);
    }

    /// <inheritdoc />
    public async Task<FeatureCollection?> GetFeatureCollection(WfsOptions options)
    {
        var uri = GetWgsUri(options);
        uri += "&outputFormat=application/json";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var featureCollection = NetCore.DeserializeString<FeatureCollection>(responseBody);
        return featureCollection;
    }
    
}