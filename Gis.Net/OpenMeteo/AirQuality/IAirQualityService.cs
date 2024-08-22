namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents a service for retrieving air quality data.
/// </summary>
public interface IAirQualityService
{
    /// <summary>
    /// Retrieves the air quality data based on the specified options.
    /// </summary>
    /// <param name="options">The options for retrieving air quality data.</param>
    /// <returns>The list of air quality data responses.</returns>
    Task<List<AirQualityApiResponse>?> AirQuality(AirQualityOptions options);

    /// <summary>
    /// Gets or sets the current air quality parameters.
    /// </summary>
    /// <remarks>
    /// This property is used to specify which air quality parameters are retrieved in the current air quality data.
    /// The available parameters include "european_aqi", "pm10", "pm2_5", "carbon_monoxide", "nitrogen_dioxide", "sulphur_dioxide", "ozone", and "dust".
    /// The default value includes all available parameters.
    /// </remarks>
    string[] Current { get; set; }
    /// <summary>
    /// Represents the hourly air quality parameters.
    /// </summary>
    string[] Hourly { get; set; }
}