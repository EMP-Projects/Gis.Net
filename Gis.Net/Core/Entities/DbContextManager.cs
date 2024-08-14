using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core.Entities;

public static class DbContextManager
{
    private static void CheckConnectionString(string connection, bool isDev = false)
    {
        if (string.IsNullOrEmpty(connection))
            throw new Exception("connectionString empty!");

        if (isDev)
            Console.WriteLine($"ConnectionString is:\r\n{connection}");
    }

    public static void AddPostGres<TContext>(
        this IServiceCollection services,
        ConnectionPgSql connection,
        bool isDev = false)
        where TContext : DbContext
    {
        CheckConnectionString(connection.ConnectionContext!, isDev);
        services.AddDbContext<TContext>(
            options => options.UseNpgsql(connection.ConnectionContext, x => { x.UseNodaTime(); })
        );
    }

    /// <summary>
    /// Crea un contesto per PostGis
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="connection"></param>
    /// <param name="project">
    /// GisDbContext si riferisce all'assembry di netcorefw, bisogna specificare il progetto per la classe parziale compilata.
    /// Per il Gis la classe parziale GisDbContext è già definita nel netcorefw, ogni progetto che eventualmente estende la classe
    /// deve specificare l'assembly di riferimento
    /// </param>
    /// <param name="isDev"></param>
    public static void AddPostGis<TContext>(
        this IServiceCollection services,
        ConnectionPgSql connection,
        string project,
        bool isDev = false
    )
        where TContext : DbContext
    {
        CheckConnectionString(connection.ConnectionContext!, isDev);
        services.AddDbContext<TContext>(
            options => options.UseNpgsql(connection.ConnectionContext, x =>
            {
                x.MigrationsAssembly(project);
                x.UseNodaTime().UseNetTopologySuite();
            })
        );
    }

    /// <summary>
    /// Apply migrations to db context
    /// </summary>
    /// <param name="dbContext"></param>
    public static async Task<T> RunMigrations<T>(this T dbContext) where T : DbContext
    {
        await dbContext.Database.MigrateAsync(cancellationToken: default);
        return dbContext;
    }
}
