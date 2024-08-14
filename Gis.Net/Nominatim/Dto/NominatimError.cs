
namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public class NominatimError
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    public NominatimError(string error)
    {
        Error = error;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool HasError => !string.IsNullOrEmpty(Error);

    /// <summary>
    /// 
    /// </summary>
    public string Error { get; set; }
    
}