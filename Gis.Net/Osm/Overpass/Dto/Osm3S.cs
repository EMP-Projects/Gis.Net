using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

public class Osm3S
{
    [JsonPropertyName("timestamp_osm_base")]
    public DateTime? TimestampOsmBase;

    [JsonPropertyName("copyright")]
    public string? Copyright;
}