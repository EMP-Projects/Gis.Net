using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Represents a task that executes database migrations on a specified DbContext.
/// </summary>
/// <typeparam name="T">The type of the DbContext.</typeparam>
public class DbMigrationsTask<T> : TaskService where T : DbContext
{
    /// <summary>
    /// Represents a task that performs database migrations for a specific DbContext.
    /// </summary>
    /// <typeparam name="T">The type of DbContext.</typeparam>
    private readonly T _dbContext;

    /// <summary>
    /// Represents a task for running database migrations for a given DbContext.
    /// </summary>
    /// <typeparam name="T">The type of DbContext.</typeparam>
    public DbMigrationsTask(ILogger<DbMigrationsTask<T>> logger, IServiceProvider serviceProvider, T dbContext) : base(logger,
        serviceProvider)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Represents the due time for executing a task.
    /// </summary>
    protected override TimeSpan DueTime => TimeSpan.FromSeconds(5);

    /// <summary>
    /// Represents a period of time.
    /// </summary>
    protected override TimeSpan Period => Timeout.InfiniteTimeSpan;

    /// <summary>
    /// Executes the job associated with the task.
    /// </summary>
    /// <param name="state">The state associated with the task. It can be null.</param>
    protected override void ExecuteJob(object? state)
    {
        var job = _dbContext.RunMigrations();
        if (job.Wait(TimeSpan.FromMinutes(1)))
            Logger.LogInformation("Automatic migration launch task completed");
    }
}