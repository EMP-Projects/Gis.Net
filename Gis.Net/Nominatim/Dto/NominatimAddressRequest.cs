using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents a request to retrieve address information from Nominatim service.
/// </summary>
public class NominatimAddressRequest : NominatimBaseRequest, INominatimAddressRequest
{
    /// <summary>
    /// Represents a street in the address.
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    /// <summary>
    /// Represents a request to retrieve address information from Nominatim service.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Represents a county in an address request for the Nominatim service.
    /// </summary>
    [JsonPropertyName("county")]
    public string? County { get; set; }

    /// <summary>
    /// Represents a request to retrieve address information from Nominatim service.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Represents a postal code in an address request for Nominatim service.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? Postalcode { get; set; }

    /// <summary>
    /// Represents a request to retrieve address information from the Nominatim service.
    /// </summary>
    public NominatimAddressRequest(string? city) : base(city)
    {
    }

    /// <summary>
    /// Represents a request for retrieving address information from the Nominatim service.
    /// </summary>
    public NominatimAddressRequest(double lat, double lon) : base(lat, lon)
    {
    }
}