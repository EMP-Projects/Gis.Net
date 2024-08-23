using System.Text.Json.Serialization;

namespace Gis.Net.GeoJsonImport;

/// <summary>
/// Represents a GeoJSON import object.
/// </summary>
public class GeoJsonImport
{
    /// <summary>
    /// Represents a GeoJSON import object.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Represents the bounding box (bbox) property of a GeoJSON import.
    /// </summary>
    [JsonPropertyName("bbox")]
    public List<double?> Bbox { get; set; }

    /// <summary>
    /// Represents a GeoJSON import object.
    /// </summary>
    [JsonPropertyName("features")]
    public List<FeatureImport> Features { get; set; }
}