using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public class NominatimBaseRequest : INominatimBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="city"></param>
    public NominatimBaseRequest(string? city)
    {
        City = city;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    public NominatimBaseRequest(double lat, double lon)
    {
        Lat = lat;
        Lon = lon;
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}