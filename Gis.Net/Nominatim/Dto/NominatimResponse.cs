using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents a response from the Nominatim API.
/// </summary>
public abstract class NominatimResponse
{
    /// <summary>
    /// Represents the bounding box of a location in the Nominatim response.
    /// </summary>
    [JsonPropertyName("boundingbox")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? BoundingBox { get; set; }

    /// <summary>
    /// Represents a response from the Nominatim API.
    /// </summary>
    [JsonPropertyName("class")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Class { get; set; }

    /// <summary>
    /// Gets or sets the display name of the location.
    /// </summary>
    [JsonPropertyName("display_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the importance of the location.
    /// </summary>
    [JsonPropertyName("importance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Importance { get; set; }
    
    [JsonPropertyName("lat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lat { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate value.
    /// </summary>
    /// <remarks>
    /// The longitude coordinate provides the east-west location of a point on the Earth's surface.
    /// </remarks>
    /// <value>The longitude coordinate as a string.</value>
    [JsonPropertyName("lon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lon { get; set; }

    /// <summary>
    /// Represents a license associated with a Nominatim response.
    /// </summary>
    [JsonPropertyName("license")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? License { get; set; }

    /// <summary>
    /// Represents the OSM ID property in a Nominatim response.
    /// </summary>
    [JsonPropertyName("osm_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OsmId { get; set; }

    /// <summary>
    /// Represents the type of the OpenStreetMap (OSM) object.
    /// </summary>
    [JsonPropertyName("osm_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OsmType { get; set; }

    /// <summary>
    /// Represents the place ID of a location.
    /// </summary>
    [JsonPropertyName("place_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PlaceId { get; set; }

    /// <summary>
    /// Represents the SVG property of a NominatimResponse.
    /// </summary>
    [JsonPropertyName("svg")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Svg { get; set; }

    /// <summary>
    /// Gets or sets the type of the result.
    /// </summary>
    /// <value>The type of the result.</value>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// Represents the PlaceRank property of a NominatimResponse object.
    /// </summary>
    [JsonPropertyName("place_rank")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PlaceRank { get; set; }

    /// <summary>
    /// Represents a category of a location in the Nominatim response.
    /// </summary>
    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Category { get; set; }

    /// <summary>
    /// Represents the type of address extracted from a Nominatim response.
    /// </summary>
    [JsonPropertyName("addresstype")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AddressType { get; set; }

    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the address information.
    /// </summary>
    /// <remarks>
    /// This property represents the address information associated with a location.
    /// </remarks>
    /// <value>The address information.</value>
    [JsonPropertyName("address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NominatimAddressResponse? Address { get; set; }

    /// <summary>
    /// Gets or sets the additional tags associated with the location.
    /// </summary>
    /// <value>
    /// The additional tags associated with the location.
    /// </value>
    [JsonPropertyName("extratags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NominatimExtraTags? ExtraTags { get; set; }
}