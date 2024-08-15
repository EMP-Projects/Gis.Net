using System.Globalization;
using Gis.Net.OpenMeteo.Weather;

namespace Gis.Net.OpenMeteo.Elevation;

public class ElevationService : OpenMeteoService, IElevationService
{
    public ElevationService(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<List<GeoCodingResponse>?> GetElevation(ICoordinatesOptions options)
    {
        if (options.Lat is null || options.Lng is null)
            throw new ArgumentException("Missing Required Geographic Coordinates");

        var uri = $"{HttpClient.BaseAddress}/elevation?latitude={options.Lat?.ToString(CultureInfo.InvariantCulture)}" +
                  $"&longitude={options.Lng?.ToString(CultureInfo.InvariantCulture)}";

        return await ApiRequest<GeoCodingResponse>(uri);
    }

    /// <inheritdoc />
    protected override string UriParameters() => string.Empty;
}