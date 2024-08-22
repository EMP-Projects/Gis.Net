
namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents an error response from the Nominatim service.
/// </summary>
public class NominatimError
{
    /// <summary>
    /// Represents an error response from the Nominatim service.
    /// </summary>
    public NominatimError(string error)
    {
        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether an error occurred.
    /// </summary>
    /// <returns>
    /// <c>true</c> if an error occurred; otherwise, <c>false</c>.
    /// </returns>
    public bool HasError => !string.IsNullOrEmpty(Error);

    /// <summary>
    /// Represents an error from the Nominatim API.
    /// </summary>
    public string Error { get; set; }
    
}