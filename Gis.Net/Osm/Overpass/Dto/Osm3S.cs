using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents the metadata for the Osm3S object in the Overpass API response.
/// </summary>
public class Osm3S
{
    /// <summary>
    /// Represents the timestamp of the OSM base in the Osm3S class.
    /// </summary>
    [JsonPropertyName("timestamp_osm_base")]
    public DateTime? TimestampOsmBase;

    /// <summary>
    /// Represents the copyright information for OSM data.
    /// </summary>
    [JsonPropertyName("copyright")]
    public string? Copyright;
}