using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

public abstract class TaskService : BackgroundTaskService, IHostedService, IDisposable
{
    protected TaskService(ILogger logger,
        IServiceProvider serviceProvider) : base(logger, serviceProvider)
    {
    }

    private Timer? _timer;

    protected virtual TimeSpan Period { get; } = TimeSpan.FromHours(1);

    protected virtual TimeSpan DueTime { get; } = TimeSpan.FromSeconds(30);

    protected abstract void ExecuteJob(object? state);

    protected virtual bool CheckIfExecuteJob(object? state) => true;
    
    protected virtual void Job(object? state)
    {
        if (CheckIfExecuteJob(state)) ExecuteJob(state);
        else Logger.LogWarning($"Skipping task {GetType().Name}");
    }

    protected virtual object? GetState() => null;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(Job, GetState(), DueTime, Period);
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken) 
        => await Task.Run(() => { _timer?.Dispose(); }, cancellationToken);

    public void Dispose()
    {
        _timer?.Dispose();
        DisposeScope();
    }
}