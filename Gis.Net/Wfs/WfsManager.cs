using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Wfs;

/// <summary>
/// Provides extension methods for IServiceCollection to support adding WFS (Web Feature Service) clients.
/// </summary>
public static class WfsManager
{
    /// <summary>
    /// Adds a singleton WFS service client to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the WFS service client to.</param>
    /// <param name="url">The base address URL of the WFS service.</param>
    /// <param name="name">The name of the HttpClient configuration. This can be used to retrieve the client from IHttpClientFactory.</param>
    /// <param name="timeOut">The timeout in minutes for the HTTP client requests. If not specified, defaults to 10 minutes.</param>
    /// <returns>The original IServiceCollection, with the WFS service client added.</returns>
    public static IServiceCollection AddWfs(this IServiceCollection services, string url, string name, int? timeOut)
    {
        services.AddHttpClient<IWfsService, WfsService>(name, client =>
        {
            client.BaseAddress = new Uri(url);
            client.Timeout = TimeSpan.FromMinutes(timeOut ?? 10);
        });
        
        return services;
    }
}