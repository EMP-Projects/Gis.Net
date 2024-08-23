using System.Text.Json.Serialization;

namespace Gis.Net.GeoJsonImport;

/// <summary>
/// Represents a feature import object in GeoJSON format.
/// </summary>
public class FeatureImport
{
    /// <summary>
    /// Represents a feature import.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Represents a geometry object in the GeoJSON import.
    /// </summary>
    [JsonPropertyName("geometryImport")]
    public GeometryImport GeometryImport { get; set; }

    /// <summary>
    /// Represents the properties of a feature import.
    /// </summary>
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; }
}