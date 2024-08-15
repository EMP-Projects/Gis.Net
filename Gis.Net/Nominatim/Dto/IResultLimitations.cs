namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public abstract class IResultLimitations
{
    /// <summary>
    /// 
    /// </summary>
    public abstract int Zoom { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public abstract List<string>? CountryCodes { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public abstract List<string>? ExcludePlaceIds { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public abstract List<double>? ViewBox { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public abstract int Bounded { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public abstract int Limit { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public abstract string? Email { get; set; }
}