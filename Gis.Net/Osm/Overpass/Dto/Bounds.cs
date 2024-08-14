using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

public class Bounds
{

    [JsonPropertyName("minlat")]
    public double MinLat;

    [JsonPropertyName("minlon")]
    public double MinLon;

    [JsonPropertyName("maxlat")]
    public double MaxLat;
    
    [JsonPropertyName("maxlon")]
    public double MaxLon;

    public Bounds(double minLat, double maxLon, double maxLat, double minLon)
    {
        MinLat = minLat;
        MaxLon = maxLon;
        MaxLat = maxLat;
        MinLon = minLon;
    }
}