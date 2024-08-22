using System.Text.Json.Serialization;

namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents a request to retrieve address information from Nominatim service.
/// </summary>
public class NominatimRequest : NominatimAddressRequest, INominatimRequest
{
    /// <summary>
    /// Represents a node in the Nominatim service.
    /// </summary>
    [JsonPropertyName("node")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Node { get; set; }

    /// <summary>
    /// Represents a request to retrieve information related to a relation from the Nominatim service.
    /// </summary>
    [JsonPropertyName("relation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Relation { get; set; }

    /// <summary>
    /// Represents a request for retrieving location information from the Nominatim service.
    /// </summary>
    [JsonPropertyName("way")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Way { get; set; }

    /// <summary>
    /// Represents a request for retrieving location information from the Nominatim service.
    /// </summary>
    public NominatimRequest(string? city) : base(city)
    {
    }

    /// <summary>
    /// Represents a request to retrieve location information from Nominatim service.
    /// </summary>
    public NominatimRequest(double lat, double lon) : base(lat, lon)
    {
    }
}