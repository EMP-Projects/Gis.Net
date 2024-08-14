using Gis.Net.Nominatim.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Nominatim;

public static class NominatimManager
{
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