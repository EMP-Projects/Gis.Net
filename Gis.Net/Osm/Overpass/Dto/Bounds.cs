using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents the bounds of a geographical area.
/// </summary>
public class Bounds
{

    /// <summary>
    /// Represents the minimum latitude of a geographic bounds.
    /// </summary>
    [JsonPropertyName("minlat")]
    public double MinLat;

    /// <summary>
    /// Represents the minimum longitude of a geographic bounds.
    /// </summary>
    [JsonPropertyName("minlon")]
    public double MinLon;

    /// <summary>
    /// Represents the maximum latitude of a geographic bounds.
    /// </summary>
    [JsonPropertyName("maxlat")]
    public double MaxLat;

    /// <summary>
    /// The maximum longitude value of a boundary.
    /// </summary>
    [JsonPropertyName("maxlon")]
    public double MaxLon;

    /// <summary>
    /// Represents the bounds of a geographic area.
    /// </summary>
    public Bounds(double minLat, double maxLon, double maxLat, double minLon)
    {
        MinLat = minLat;
        MaxLon = maxLon;
        MaxLat = maxLat;
        MinLon = minLon;
    }
}