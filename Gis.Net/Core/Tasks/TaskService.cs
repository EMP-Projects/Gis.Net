using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Base abstract class for implementing task services.
/// </summary>
public abstract class TaskService : BackgroundTaskService, IHostedService, IDisposable
{
    /// <summary>
    /// Base class for task services that are executed as background tasks in a hosted environment.
    /// </summary>
    protected TaskService(ILogger logger,
        IServiceProvider serviceProvider) : base(logger, serviceProvider)
    {
    }

    /// <summary>
    /// Represents a timer used for scheduling tasks.
    /// </summary>
    private Timer? _timer;

    /// <summary>
    /// Represents the period at which a task service is executed.
    /// </summary>
    protected virtual TimeSpan Period { get; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Gets the amount of time to delay before the Job method is first executed.
    /// </summary>
    protected virtual TimeSpan DueTime { get; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Executes the job associated with the task.
    /// </summary>
    /// <param name="state">The state associated with the task. It can be null.</param>
    protected abstract void ExecuteJob(object? state);

    /// <summary>
    /// This method checks if the job should be executed.
    /// </summary>
    /// <param name="state">Optional state object passed to the job.</param>
    /// <returns>Returns true if the job should be executed, otherwise false.</returns>
    protected virtual bool CheckIfExecuteJob(object? state) => true;

    /// <summary>
    /// Job method that is executed periodically by a TaskService.
    /// </summary>
    /// <param name="state">The optional state object that can be passed to the job.</param>
    protected virtual void Job(object? state)
    {
        if (CheckIfExecuteJob(state)) ExecuteJob(state);
        else Logger.LogWarning($"Skipping task {GetType().Name}");
    }

    /// <summary>
    /// Retrieves the current state of the task.
    /// </summary>
    /// <returns>The current state of the task.</returns>
    protected virtual object? GetState() => null;

    /// <summary>
    /// Start the background task and execute it periodically.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(Job, GetState(), DueTime, Period);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops the execution of the task.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to stop the task.</param>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    public async Task StopAsync(CancellationToken cancellationToken) 
        => await Task.Run(() => { _timer?.Dispose(); }, cancellationToken);

    /// <summary>
    /// Disposes the resources used by the TaskService.
    /// </summary>
    public void Dispose()
    {
        _timer?.Dispose();
        DisposeScope();
    }
}