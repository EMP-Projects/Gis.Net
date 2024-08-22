using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents the OverpassManager, which provides methods for configuring and using the Overpass service.
/// </summary>
public static class OverPassManager
{
    /// <summary>
    /// Adds the Overpass service to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the Overpass service to.</param>
    /// <param name="url">The base URL for the Overpass API.</param>
    /// <param name="name">The name of the HTTP client to use for the Overpass service.</param>
    /// <param name="timeOut">The timeout value for the HTTP client, in minutes.</param>
    /// <returns>The modified IServiceCollection.</returns>
    /// <remarks>
    /// The AddOverPass method adds the Overpass service to the IServiceCollection by configuring an HTTP client with the specified base URL and timeout value.
    /// The name parameter is optional and defaults to "OverPass".
    /// The url parameter is optional and defaults to "https://overpass-api.de".
    /// The timeOut parameter is optional and defaults to 10 minutes.
    /// </remarks>
    public static IServiceCollection AddOverPass(this IServiceCollection services, string? url, string? name, int? timeOut)
    {
        services.AddHttpClient<IOverPass, OverPassService>(name ?? "OverPass", client =>
        {
            client.BaseAddress = new Uri(url ?? "https://overpass-api.de");
            client.Timeout = TimeSpan.FromMinutes(timeOut ?? 10);
        });
        
        return services;
    }
}