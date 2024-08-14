using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// 
/// </summary>
public abstract class NominatimExtraTags
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("image")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Image { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("heritage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Heritage { get; set; }
        
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("wikidata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Wikidata { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("architect")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Architect { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("wikipedia")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Wikipedia { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("wheelchair")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Wheelchair { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("heritage:website")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HeritageWebsite { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("heritage:operator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HeritageOperator { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("architect:wikidata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ArchitectWikidata { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("year_of_construction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YearOfConstruction { get; set; }
}