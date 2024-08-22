namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// Represents the base interface for Nominatim requests.
/// </summary>
public interface INominatimBase
{
    /// <summary>
    /// Represents a city in the Nominatim system.
    /// </summary>
    string? City { get; set; }

    /// <summary>
    /// Represents the latitude coordinate for a Nominatim request.
    /// </summary>
    double Lat { get; set; }

    /// <summary>
    /// Represents the longitude property.
    /// </summary>
    double Lon { get; set; }
}