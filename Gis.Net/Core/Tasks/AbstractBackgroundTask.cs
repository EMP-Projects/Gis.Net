using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Abstract base class for creating background tasks.
/// </summary>
public abstract class AbstractBackgroundTask : IBackgroundTask
{
    /// <summary>
    /// Logger instance for logging.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Initializes a new instance of the AbstractBackgroundTask class.
    /// </summary>
    /// <param name="logger">The logger to use for logging information and errors.</param>
    /// <param name="serviceProvider">The service provider for creating a new scope.</param>
    protected AbstractBackgroundTask(ILogger logger, IServiceProvider serviceProvider)
    {
        Logger = logger;
        Scope = serviceProvider.CreateScope();
    }

    /// <inheritdoc />
    public virtual TimeSpan? Period { get; set; }

    /// <inheritdoc />
    public virtual TimeSpan? DueTime { get; set; }

    /// <inheritdoc />
    public virtual TimeSpan? DelayOnError { get; set; }

    /// <summary>
    /// Service scope for creating scoped services.
    /// </summary>
    private IServiceScope Scope { get; set; }
    
    /// <summary>
    /// Creates a scoped service of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of service to create.</typeparam>
    /// <returns>The service instance or null if it cannot be created.</returns>
    protected T? CreateScopedService<T>() where T : class
    {
        var s = Scope.ServiceProvider.GetService<T>();
        if (s is not null) return s;
        Logger.LogError($"Cannot get service {typeof(T)} to run background task");
        return null;
    }
}