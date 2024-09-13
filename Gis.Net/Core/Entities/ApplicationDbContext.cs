using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Core.Entities;

/// <inheritdoc />
public abstract class ApplicationDbContext<T> : IApplicationDbContext<T> where T : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext{T}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    protected ApplicationDbContext(T context)
    {
        Context = context;
    }

    /// <inheritdoc />
    public T Context { get; }

    /// <inheritdoc />
    public async Task RunMigrations()
    {
        await Context.Database.MigrateAsync();
    }
}