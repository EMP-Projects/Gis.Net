using System.Text.Json.Serialization;

namespace Gis.Net.GeoJsonImport;

/// <summary>
/// Represents a geometry import object in GeoJSON format.
/// </summary>
public class GeometryImport
{
    /// <summary>
    /// Represents the type of a geometry import.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Represents the coordinates of a geometry.
    /// </summary>
    [JsonPropertyName("coordinates")]
    public List<List<List<double>>> Coordinates { get; set; }
}