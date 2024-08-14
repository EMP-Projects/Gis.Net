using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public abstract class NominatimResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("boundingbox")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? BoundingBox { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("class")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Class { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("display_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("importance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Importance { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("lat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lat { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("lon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lon { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("license")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? License { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("osm_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OsmId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("osm_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OsmType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("place_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PlaceId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("svg")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Svg { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("place_rank")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PlaceRank { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Category { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("addresstype")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AddressType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NominatimAddressResponse? Address { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("extratags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NominatimExtraTags? ExtraTags { get; set; }
}