namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public interface INominatimAddressRequest : INominatimBase
{
    /// <summary>
    /// 
    /// </summary>
    string? Street { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    string? Country { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    string? County { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    string? State { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    string? Postalcode { get; set; }
    
}