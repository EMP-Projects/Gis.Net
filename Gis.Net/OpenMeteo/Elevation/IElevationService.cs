using Gis.Net.OpenMeteo.Weather;

namespace Gis.Net.OpenMeteo.Elevation;

public interface IElevationService : IOpenMeteoService
{
    Task<List<GeoCodingResponse>?> GetElevation(ICoordinatesOptions options);
}