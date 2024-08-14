namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public interface INominatimBase
{
    /// <summary>
    /// 
    /// </summary>
    string? City { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    double Lat { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    double Lon { get; set; }
    
}