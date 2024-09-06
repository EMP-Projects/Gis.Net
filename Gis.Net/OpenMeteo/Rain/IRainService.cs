using Gis.Net.OpenMeteo.Weather;

namespace Gis.Net.OpenMeteo.Rain;

/// <inheritdoc />
public interface IRainService : IOpenMeteoService
{
    /// <summary>
    /// Asynchronously retrieves weather forecast data based on the specified options.
    /// </summary>
    /// <param name="options">The configuration options for the weather data request.</param>
    /// <returns>
    /// A task that represents the asynchronous operation of retrieving weather forecast data.
    /// The task result contains a list of <see cref="OpenMeteoData"/> objects or null if no data could be retrieved.
    /// </returns>
    Task<OpenMeteoData?> Forecast(OpenMeteoOptions options);
}