using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents the base class for Nominatim requests.
/// </summary>
public class NominatimBaseRequest : INominatimBase
{
    /// <summary>
    /// Represents a base request for Nominatim service.
    /// </summary>
    public NominatimBaseRequest(string? city)
    {
        City = city;
    }

    /// <summary>
    /// Represents a base Nominatim request.
    /// </summary>
    public NominatimBaseRequest(double lat, double lon)
    {
        Lat = lat;
        Lon = lon;
    }

    /// <summary>
    /// Represents a city.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// Represents the latitude value of a location.
    /// </summary>
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    /// <summary>
    /// Gets or sets the longitude value of the location.
    /// </summary>
    /// <value>The longitude value.</value>
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}