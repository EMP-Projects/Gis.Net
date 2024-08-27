using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Istat;

/// <summary>
/// Represents an interface for managing limits with a specific context and model.
/// </summary>
/// <typeparam name="TContext">The type of the database context.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
public interface ILimits<TContext, TModel> 
    where TContext : DbContext 
    where TModel : class, ILimitsModelBase
{
    /// <summary>
    /// Lists the models based on the specified options.
    /// </summary>
    /// <param name="options">The options to filter the models.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of models.</returns>
    Task<IEnumerable<TModel>> List(IstatLimitOptions<TModel>? options);

    /// <summary>
    /// Gets the DbSet for the model.
    /// </summary>
    DbSet<TModel> Entity { get; }

    /// <summary>
    /// Gets the database context.
    /// </summary>
    DbContext Context { get; }
}