using Gis.Net.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Background task abstraction
/// </summary>
public abstract class BackgroundTaskService
{

    /// <summary>
    /// Logger used for logging messages and errors in the BackgroundTaskService class.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// The scope used to get DI services for this task
    /// </summary>
    protected IServiceScope Scope { get; set; }

    /// <summary>
    /// This function is called by TaskService to dispose scope
    /// </summary>
    protected void DisposeScope() => Scope.Dispose();

    /// <summary>
    /// Base abstract class for implementing task services.
    /// </summary>
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

    /// <summary>
    /// Waits for the completion of a task with a specified timeout and returns the result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="task">The task to wait for.</param>
    /// <param name="timeoutInSeconds">The timeout in seconds.</param>
    /// <param name="taskName">The name of the task.</param>
    /// <returns>The result of the task if it completes within the timeout; otherwise, <c>null</c>.</returns>
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

    /// <summary>
    /// Waits for the completion of a task and returns the result.
    /// </summary>
    /// <param name="task">The task to await.</param>
    /// <param name="timeoutInSeconds">The timeout for waiting for the task completion, in seconds.</param>
    /// <param name="taskName">The name of the task.</param>
    /// <returns>The result of the task. Null if the task did not complete within the specified timeout.</returns>
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

    /// <summary>
    /// Generic method for awaiting a task and returning its result.
    /// </summary>
    /// <param name="task">The task to wait for.</param>
    /// <param name="timeoutInSeconds">The timeout duration in seconds.</param>
    /// <param name="taskName">The name of the task.</param>
    /// <returns>The result of the task if it completes within the timeout, otherwise null.</returns>
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

    /// <summary>
    /// Logs the exceptions contained in the given AggregateException.
    /// </summary>
    /// <param name="e">The AggregateException containing the exceptions to be logged.</param>
    protected void LogExceptions(AggregateException e)
    {
        foreach (var item in e.InnerExceptions)
            LogException(item);
    }

    /// <summary>
    /// This method logs an exception, including any inner exceptions and specific information related to the exception.
    /// </summary>
    /// <param name="e">The exception to be logged.</param>
    /// <param name="level">The level of indentation for the log message. Default is 0.</param>
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