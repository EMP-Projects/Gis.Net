namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents a request to retrieve address information from Nominatim service.
/// </summary>
public interface INominatimAddressRequest : INominatimBase
{
    /// <summary>
    /// Represents a street in a Nominatim address request.
    /// </summary>
    string? Street { get; set; }

    /// <summary>
    /// Represents a country in Nominatim address request.
    /// </summary>
    string? Country { get; set; }

    /// <summary>
    /// Represents a county in Nominatim address request.
    /// </summary>
    string? County { get; set; }

    /// <summary>
    /// Represents a state in Nominatim address request.
    /// </summary>
    string? State { get; set; }

    /// <summary>
    /// Represents a postal code in an address request to Nominatim service.
    /// </summary>
    /// <remarks>
    /// The Postalcode property should be set to the postal code of the location being requested in the address.
    /// </remarks>
    string? Postalcode { get; set; }
    
}