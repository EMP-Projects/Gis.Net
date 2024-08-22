using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents the response received from the Nominatim API when querying for address information.
/// </summary>
public abstract class NominatimAddressResponse
{
    /// <summary>
    /// Represents a city in an address response from Nominatim.
    /// </summary>
    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; set; }

    /// <summary>
    /// Represents the district within a city.
    /// </summary>
    [JsonPropertyName("city_district")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CityDistrict { get; set; }

    /// <summary>
    /// Represents the construction information of a Nominatim address response.
    /// </summary>
    [JsonPropertyName("construction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Construction { get; set; }

    /// <summary>
    /// Represents the continent property of a Nominatim address response.
    /// </summary>
    [JsonPropertyName("continent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Continent { get; set; }

    /// <summary>
    /// Represents the country property of a <see cref="NominatimAddressResponse"/> object.
    /// </summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; set; }

    /// <summary>
    /// Represents the country code of a location.
    /// </summary>
    [JsonPropertyName("country_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Represents the house number of an address in the Nominatim API response.
    /// </summary>
    [JsonPropertyName("house_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Represents a neighbourhood in a geographic location.
    /// </summary>
    [JsonPropertyName("neighbourhood")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Neighbourhood { get; set; }

    /// <summary>
    /// Represents a postal code.
    /// </summary>
    [JsonPropertyName("postcode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Postcode { get; set; }

    /// <summary>
    /// Represents a public building in the Nominatim address response.
    /// </summary>
    [JsonPropertyName("public_building")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PublicBuilding { get; set; }

    /// <summary>
    /// Represents the state property of a Nominatim address response.
    /// </summary>
    [JsonPropertyName("state")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; set; }

    /// <summary>
    /// Represents a suburb within an address response.
    /// </summary>
    [JsonPropertyName("suburb")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Suburb { get; set; }

    /// <summary>
    /// Represents a road address.
    /// </summary>
    [JsonPropertyName("road")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Road { get; set; }

    /// <summary>
    /// Represents a village in a Nominatim address response.
    /// </summary>
    [JsonPropertyName("village")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Village { get; set; }

    /// <summary>
    /// Represents the state district property in a Nominatim address response.
    /// </summary>
    [JsonPropertyName("state_district")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StateDistrict { get; set; }

    /// <summary>
    /// Represents a tourism property in a Nominatim address response.
    /// </summary>
    [JsonPropertyName("tourism")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Tourism { get; set; }
}