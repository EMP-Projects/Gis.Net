using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Extension class for configuring database migrations.
/// </summary>
public static class DbMigrationsTaskExtension
{
    /// <summary>
    /// Provides an extension method to add database migrations to the service collection.
    /// </summary>
    /// <typeparam name="T">The type of the DbContext.</typeparam>
    /// <param name="services">The service collection.</param>
    public static void WithDbMigrations<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddHostedService<DbMigrationsTask<T>>();
    }
}