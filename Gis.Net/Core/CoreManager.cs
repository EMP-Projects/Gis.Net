using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core;

/// <summary>
/// The CoreManager class provides methods for managing the core functionality.
/// </summary>
public static class CoreManager
{
    /// <summary>
    /// Adds API versioning to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the API versioning to.</param>
    /// <param name="major">The major version number.</param>
    /// <param name="minor">The minor version number.</param>
    /// <param name="headerName">The name of the header to read the API version from. If not specified, the default header name "X-API-Version" will be used.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with API versioning configured.</returns>
    /// <remarks>
    /// This method adds API versioning to the specified <see cref="IServiceCollection"/> using the provided major and minor version numbers.
    /// It also allows specifying a custom header name to read the API version from.
    /// The default header name is "X-API-Version".
    /// </remarks>
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