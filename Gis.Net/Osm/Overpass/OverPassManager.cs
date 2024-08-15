using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Osm.Overpass;

public static class OverPassManager
{
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