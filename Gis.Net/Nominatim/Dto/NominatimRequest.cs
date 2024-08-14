using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public class NominatimRequest : NominatimAddressRequest, INominatimRequest
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("node")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Node { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("relation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Relation { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("way")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Way { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="city"></param>
    public NominatimRequest(string? city) : base(city)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    public NominatimRequest(double lat, double lon) : base(lat, lon)
    {
    }
}