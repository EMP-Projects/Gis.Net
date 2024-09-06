using Gis.Net.OpenMeteo.Weather;

namespace Gis.Net.OpenMeteo.Elevation;

/// <summary>
/// Interface for accessing elevation data from the OpenMeteo API.
/// </summary>
public interface IElevationService : IOpenMeteoService
{
    /// <summary>
    /// Retrieves the elevation data for a given set of geographic coordinates.
    /// </summary>
    /// <param name="options">The options containing the latitude and longitude coordinates.</param>
    /// <returns>A list of <see cref="GeoCodingResponse"/> objects representing the elevation data for the specified coordinates.</returns>
    Task<GeoCodingResponse?> GetElevation(ICoordinatesOptions options);
}