using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Classe di estensione per IServiceCollection per registrare DbMigrationsTask come HostedService.
/// </summary>
public static class DbMigrationsTaskExtension
{
    /// <summary>
    /// Estende IServiceCollection per aggiungere DbMigrationsTask come servizio ospitato.
    /// </summary>
    /// <param name="services">La collezione di servizi a cui aggiungere il task di migrazione del database.</param>
    public static void WithDbMigrations<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddHostedService<DbMigrationsTask<T>>();
    }
}