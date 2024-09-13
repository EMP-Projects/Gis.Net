using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Core.Entities;

/// <summary>
/// Interface for application database context.
/// </summary>
/// <typeparam name="T">The type of the DbContext.</typeparam>
public interface IApplicationDbContext<T> where T : DbContext
{
    /// <summary>
    /// Runs the database migrations asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RunMigrations();
    
    /// <summary>
    /// Gets the database context.
    /// </summary>
    T Context { get; }
}