namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents a request for retrieving location information from the Nominatim service.
/// </summary>
public interface INominatimRequest : INominatimAddressRequest
{
    /// <summary>
    /// Represents a node in the NominatimRequest.
    /// </summary>
    int? Node { get; set; }

    /// <summary>
    /// Represents a relation in a Nominatim request.
    /// </summary>
    int? Relation { get; set; }

    /// Represents a request to retrieve address information from Nominatim service.
    /// /
    int? Way { get; set; }
}