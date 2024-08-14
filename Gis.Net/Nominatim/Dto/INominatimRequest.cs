namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public interface INominatimRequest : INominatimAddressRequest
{
    /// <summary>
    /// 
    /// </summary>
    int? Node { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    int? Relation { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    int? Way { get; set; }
}