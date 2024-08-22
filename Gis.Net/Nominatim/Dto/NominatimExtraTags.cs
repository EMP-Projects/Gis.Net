using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents additional tags for a Nominatim response.
/// </summary>
public abstract class NominatimExtraTags
{
    /// <summary>
    /// Represents the Image property of a NominatimExtraTags object.
    /// </summary>
    [JsonPropertyName("image")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Image { get; set; }

    /// <summary>
    /// Represents the heritage information related to a location.
    /// </summary>
    [JsonPropertyName("heritage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Heritage { get; set; }

    /// <summary>
    /// Gets or sets the Wikidata ID associated with the location.
    /// </summary>
    [JsonPropertyName("wikidata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Wikidata { get; set; }

    /// <summary>
    /// Represents additional tags related to an architect in the Nominatim API response.
    /// </summary>
    [JsonPropertyName("architect")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Architect { get; set; }

    /// <summary>
    /// Gets or sets the Wikipedia URL of the location.
    /// </summary>
    /// <value>
    /// The Wikipedia URL associated with the location.
    /// </value>
    [JsonPropertyName("wikipedia")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Wikipedia { get; set; }

    /// <summary>
    /// Represents additional tags associated with a wheelchair.
    /// </summary>
    [JsonPropertyName("wheelchair")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Wheelchair { get; set; }

    /// <summary>
    /// Represents the additional tags associated with a location in the Nominatim service.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Represents additional heritage information for a website.
    /// </summary>
    [JsonPropertyName("heritage:website")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HeritageWebsite { get; set; }

    /// <summary>
    /// Class representing the extra tags related to heritage information.
    /// </summary>
    [JsonPropertyName("heritage:operator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HeritageOperator { get; set; }

    /// <summary>
    /// Represents the Wikidata identifier of the architect associated with a location.
    /// </summary>
    [JsonPropertyName("architect:wikidata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ArchitectWikidata { get; set; }

    /// <summary>
    /// Gets or sets the year of construction.
    /// </summary>
    [JsonPropertyName("year_of_construction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YearOfConstruction { get; set; }
}