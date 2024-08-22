using Gis.Net.OpenMeteo.AirQuality;
using Gis.Net.OpenMeteo.Elevation;
using Gis.Net.OpenMeteo.Rain;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.OpenMeteo;

/// <summary>
/// Provides methods for managing OpenMeteo services.
/// </summary>
public static class OpenMeteoManager
{

    /// <summary>
    /// Adds the RainService to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the RainService to.</param>
    /// <param name="baseUrl">The base URL for the RainService. If not provided, the default base URL will be used.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddRain(this IServiceCollection services, string? baseUrl)
    {
        services.AddHttpClient<IRainService, RainService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl ?? "https://api.open-meteo.com/v1");
            client.Timeout = TimeSpan.FromMinutes(10);
        });
        
        return services;
    }

    /// <summary>
    /// Adds an elevation service to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the elevation service to.</param>
    /// <param name="baseUrl">The base URL of the OpenMeteo API. If not provided, the default base URL will be used.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddElevation(this IServiceCollection services, string? baseUrl)
    {
        services.AddHttpClient<IElevationService, ElevationService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl ?? "https://api.open-meteo.com/v1");
            client.Timeout = TimeSpan.FromMinutes(10);
        });
        
        return services;
    }

    /// <summary>
    /// Adds a service for retrieving air quality data to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="baseUrl">The base URL for the air quality service. If null, the default base URL 'https://air-quality-api.open-meteo.com/v1' will be used.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddAirQuality(this IServiceCollection services, string? baseUrl)
    {
        services.AddHttpClient<IAirQualityService, AirQualityService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl ?? "https://air-quality-api.open-meteo.com/v1");
            client.Timeout = TimeSpan.FromMinutes(10);
        });
        
        return services;
    }
}