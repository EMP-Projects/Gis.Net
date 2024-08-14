using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public abstract class NominatimAddressResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("city_district")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CityDistrict { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("construction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Construction { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("continent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Continent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("country_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CountryCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("house_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("neighbourhood")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Neighbourhood { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("postcode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Postcode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("public_building")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PublicBuilding { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("state")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("suburb")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Suburb { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("road")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Road { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("village")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Village { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("state_district")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StateDistrict { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("tourism")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Tourism { get; set; }
}