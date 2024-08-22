using Gis.Net.Nominatim.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Nominatim;

/// <summary>
/// Provides methods for managing Nominatim service.
/// </summary>
public static class NominatimManager
{
    /// <summary>
    /// Adds Nominatim services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="url">The base URL of the Nominatim service to use (optional).</param>
    /// <param name="name">The name of the Nominatim service (optional).</param>
    /// <param name="timeOut">The timeout duration for HTTP requests in minutes (optional).</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddNominatim(this IServiceCollection services, string? url, string? name, int? timeOut)
    {

        var baseDefaultUrl = "https://nominatim.openstreetmap.org/lookup";
        
        services.AddHttpClient<NominatimSearch>(name ?? "NominatimSearch", client =>
        {
            client.BaseAddress = new Uri($"{url ?? baseDefaultUrl}/search");
            client.Timeout = TimeSpan.FromMinutes(timeOut ?? 10);
        });
        
        services.AddHttpClient<NominatimReverse>(name ?? "NominatimReverse", client =>
        {
            client.BaseAddress = new Uri($"{url ?? baseDefaultUrl}/reverse");
            client.Timeout = TimeSpan.FromMinutes(timeOut ?? 10);
        });
        
        services.AddHttpClient<NominatimLookup>(name ?? "NominatimLookup", client =>
        {
            client.BaseAddress = new Uri($"{url ?? baseDefaultUrl}/lookup");
            client.Timeout = TimeSpan.FromMinutes(timeOut ?? 10);
        });
            
        return services;
    }
}