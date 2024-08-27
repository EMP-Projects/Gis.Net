using Gis.Net.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gis.Net.Istat;

/// <summary>
/// Provides extension methods for configuring ISTAT GIS services.
/// </summary>
public static class IstatManager
{
    /// <summary>
    /// Adds ISTAT GIS services to the specified <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to configure.</param>
    /// <param name="connection">The PostgreSQL connection settings.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/>.</returns>
    public static WebApplicationBuilder AddIstatGis(
        this WebApplicationBuilder builder, 
        ConnectionPgSql connection)
    {
        // Adds PostGIS services to the specified context.
        builder.Services.AddPostGis<IstatContext>(connection, 
            builder.Environment.ApplicationName,
            builder.Environment.IsDevelopment());
        
        // Registers the ILimits service with scoped lifetime.
        builder.Services.AddScoped(typeof(ILimits<,>), typeof(Limits<,>));
        
        // Registers the IIStatService with scoped lifetime.
        builder.Services.AddScoped<IIStatService<IstatContext>, IstatService<IstatContext>>();
        
        return builder;
    }
}