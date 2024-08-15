using Gis.Net.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Background task abstraction
/// </summary>
public abstract class BackgroundTaskService
{

    protected readonly ILogger Logger;

    /// <summary>
    /// The scope used to get DI services for this task
    /// </summary>
    protected IServiceScope Scope { get; set; }

    /// <summary>
    /// This function is called by TaskService to dispose scope
    /// </summary>
    protected void DisposeScope() => Scope.Dispose();

    protected BackgroundTaskService(
        ILogger logger,
        IServiceProvider serviceProvider
        )
    {
        Logger = logger;
        Scope = serviceProvider.CreateScope();
    }

    /// <summary>
    /// Returns the instance for the service T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T? CreateScopedService<T>() where T : class
    {
        var s = Scope.ServiceProvider.GetService<T>();
        if (s is not null) return s;
        Logger.LogError($"Cannot get service ${typeof(T)} to run background task");
        return null;
    }

    protected TResult? JobAwaiter<TResult>(Task<TResult?> task, int timeoutInSeconds, string taskName)
        where TResult : class
    {
        
        TResult? result = null;
        var msgLog = $"Task [{taskName}]";

        try
        {
            Logger.LogDebug($"Ready to wait task [{msgLog}]");

            if (timeoutInSeconds <= 0)
            {
                task.Wait();
                Logger.LogInformation($"Task [{msgLog}] completed");
                result = task.Result;
                return result;
            }
            
            var waitDelay = TimeSpan.FromSeconds(timeoutInSeconds);
            if (task.Wait(waitDelay))
            {
                Logger.LogInformation($"Task [{msgLog}] completed");
                result = task.Result;
                return result;
            }
            
            Logger.LogError($"[{taskName}]: {waitDelay}s timeout expired!");
        }
        catch (AggregateException e)
        {
            Logger.LogError($"[{taskName}]: exception(s)");
            LogExceptions(e);
        }

        return result;
    }
    
    protected bool JobAwaiter(Task task, int timeoutInSeconds, string taskName)
    {
        
        var msgLog = $"Task [{taskName}]";

        try
        {
            if (timeoutInSeconds <= 0)
            {
                task.Wait();
                Logger.LogInformation($"{msgLog} completed");
                return true;
            }

            var waitDelay = TimeSpan.FromSeconds(timeoutInSeconds);
            if (task.Wait(waitDelay))
            {
                Logger.LogInformation($"{msgLog} completed");
                return true;
            }

            Logger.LogError($"[{taskName}]: timeout!");
        }
        catch (AggregateException e)
        {
            Logger.LogError($"[{taskName}]: exception(s)");
            LogExceptions(e);
        }

        return false;
    }
    
    protected int? JobAwaiter(Task<int> task, int timeoutInSeconds, string taskName)
    {
        int? result = null;
        var msgLog = $"Task [{taskName}]";

        try
        {
            if (timeoutInSeconds <= 0)
            {
                task.Wait();
                Logger.LogInformation($"{msgLog} completed");
                result = task.Result;
                return result;
            }
            
            var waitDelay = TimeSpan.FromSeconds(timeoutInSeconds);
            if (task.Wait(waitDelay))
            {
                Logger.LogInformation($"{msgLog} completed");
                result = task.Result;
                return result;
            }

            Logger.LogError($"[{taskName}]: timeout!");
        }
        catch (AggregateException e)
        {
            Logger.LogError($"[{taskName}]: exception(s)");
            LogExceptions(e);
        }

        return result;
    }

    protected void LogExceptions(AggregateException e)
    {
        foreach (var item in e.InnerExceptions)
            LogException(item);
    }

    protected void LogException(Exception e, int level = 0)
    {
        while (true)
        {
            var indentation = new string('-', (level + 1));
            Logger.LogError($"{indentation} {e.GetType().Name} => {e.Message}");

            if (e is ModelValidationException mve)
            {
                var i = 1;
                foreach (var error in mve.Errors)
                    Logger.LogError($"{indentation} ({i++}) => {error}");
            }

            if (e.InnerException != null)
            {
                e = e.InnerException;
                level += 1;
                continue;
            }

            break;
        }
    }
}