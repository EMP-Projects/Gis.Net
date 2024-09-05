using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Istat;

/// <inheritdoc />
public class Limits<TContext, TModel> : ILimits<TContext, TModel>
    where TContext : DbContext, IStatDbContext 
    where TModel : class, ILimitsModelBase
{
    private readonly TContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Limits{TContext, TModel}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Limits(TContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public virtual DbSet<TModel> Entity => Context.Set<TModel>();
    
    /// <inheritdoc />
    public virtual DbContext Context => _context;

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TModel>> List(IstatLimitOptions<TModel>? options)
    {
        var entities = Entity.AsNoTracking();
        entities = options?.OnBeforeQuery?.Invoke(entities);
        if (entities == null) return [];
        return await entities.ToListAsync();
    }
}