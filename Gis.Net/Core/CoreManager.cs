using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core;

public static class CoreManager
{
    public static IServiceCollection AddVersion(this IServiceCollection services, int major, int minor, string? headerName = null)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(major, minor);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
            setup.ApiVersionReader = new HeaderApiVersionReader(headerName ?? "X-API-Version");
            setup.ReportApiVersions = true; 
        });

        return services;
    }
}