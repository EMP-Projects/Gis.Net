using Gis.Net.OpenMeteo.AirQuality;
using Gis.Net.OpenMeteo.Elevation;
using Gis.Net.OpenMeteo.Rain;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.OpenMeteo;

public static class OpenMeteoManager
{
    
    public static IServiceCollection AddRain(this IServiceCollection services, string? baseUrl)
    {
        services.AddHttpClient<IRainService, RainService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl ?? "https://api.open-meteo.com/v1");
            client.Timeout = TimeSpan.FromMinutes(10);
        });
        
        return services;
    }
    
    public static IServiceCollection AddElevation(this IServiceCollection services, string? baseUrl)
    {
        services.AddHttpClient<IElevationService, ElevationService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl ?? "https://api.open-meteo.com/v1");
            client.Timeout = TimeSpan.FromMinutes(10);
        });
        
        return services;
    }
    
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