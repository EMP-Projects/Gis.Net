using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core.Entities;

/// <summary>
/// Provides methods for managing the configuration and initialization of DbContext instances.
/// </summary>
public static class DbContextManager
{
    /// <summary>
    /// Check the validity of the connection string and prints it if in development mode.
    /// </summary>
    /// <param name="connection">The connection string to be checked</param>
    /// <param name="isDev">Indicates whether the application is in development mode</param>
    private static void CheckConnectionString(string connection, bool isDev = false)
    {
        if (string.IsNullOrEmpty(connection))
            throw new Exception("connectionString empty!");

        if (isDev)
            Console.WriteLine($"ConnectionString is:\r\n{connection}");
    }

    /// <summary>
    /// Adds PostgreSQL database context to the IServiceCollection using the specified connection.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext.</typeparam>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="connection">The PostgreSQL connection configuration.</param>
    /// <param name="isDev">A flag indicating if the application is running in development mode. Default is false.</param>
    /// <exception cref="Exception">Thrown when the connection string is empty.</exception>
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
    /// Adds a PostgreSQL database with PostGIS support to the IServiceCollection.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext to be added.</typeparam>
    /// <param name="services">The IServiceCollection to add the DbContext to.</param>
    /// <param name="connection">The PostgreSQL connection configuration.</param>
    /// <param name="project">The name of the project.</param>
    /// <param name="isDev">Indicates whether the application is running in development mode.</param>
    /// <exception cref="System.Exception">Thrown when the connection string is empty.</exception>
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
    /// Runs the database migrations for the specified DbContext.
    /// </summary>
    /// <typeparam name="T">The type of DbContext.</typeparam>
    /// <param name="dbContext">The instance of the DbContext.</param>
    /// <returns>The instance of the DbContext after the migrations have been run.</returns>
    public static async Task<T> RunMigrations<T>(this T dbContext) where T : DbContext
    {
        await dbContext.Database.MigrateAsync(cancellationToken: default);
        return dbContext;
    }
    
    /// <summary>
    /// Applies database migrations for the specified DbContext service.
    /// </summary>
    /// <typeparam name="TService">The type of the service that implements IApplicationDbContext&lt;TContext&gt;.</typeparam>
    /// <typeparam name="TContext">The type of the DbContext.</typeparam>
    /// <param name="app">The WebApplication instance.</param>
    /// <returns>The WebApplication instance after applying migrations.</returns>
    public static WebApplication ApplyMigrations<TService, TContext>(this WebApplication app) 
        where TService : IApplicationDbContext<TContext> 
        where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var service = services.GetRequiredService<TService>();
        service.RunMigrations();
        return app;
    }
}
