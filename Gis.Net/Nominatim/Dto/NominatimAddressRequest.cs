using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public class NominatimAddressRequest : NominatimBaseRequest, INominatimAddressRequest
{
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("county")]
    public string? County { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? Postalcode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="city"></param>
    public NominatimAddressRequest(string? city) : base(city)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    public NominatimAddressRequest(double lat, double lon) : base(lat, lon)
    {
    }
}