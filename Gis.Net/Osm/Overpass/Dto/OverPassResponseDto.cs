using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents the response from the Overpass API.
/// </summary>
public class OverPassResponseDto
{
    /// <summary>
    /// Represents the version number of the OverPassResponseDto.
    /// </summary>
    [JsonPropertyName("version")]
    public double? Version;

    /// <summary>
    /// Represents the generator of the Overpass API response.
    /// </summary>
    /// <remarks>
    /// The <see cref="Generator"/> class is used to store the information about the generator
    /// of the Overpass API response.
    /// </remarks>
    [JsonPropertyName("generator")]
    public string? Generator;

    /// <summary>
    /// Represents the metadata for the Osm3S object in the Overpass API response.
    /// </summary>
    [JsonPropertyName("osm3s")]
    public Osm3S? Osm3S;

    /// <summary>
    /// Represents a collection of elements in the Overpass API response.
    /// </summary>
    [JsonPropertyName("elements")]
    public List<Element?>? Elements;
}