using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents the geometry information of an OSM (OpenStreetMap) object.
/// </summary>
public class GeometryOsm
{
    /// <summary>
    /// Represents the latitude coordinate of an OSM (OpenStreetMap) object.
    /// </summary>
    [JsonPropertyName("lat")]
    public double? Lat;

    /// <summary>
    /// Gets or sets the longitude coordinate of the element.
    /// </summary>
    [JsonPropertyName("lon")]
    public double? Lon;
}