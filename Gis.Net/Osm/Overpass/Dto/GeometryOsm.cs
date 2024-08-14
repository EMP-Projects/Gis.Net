using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

public class GeometryOsm
{
    [JsonPropertyName("lat")]
    public double? Lat;

    [JsonPropertyName("lon")]
    public double? Lon;
}