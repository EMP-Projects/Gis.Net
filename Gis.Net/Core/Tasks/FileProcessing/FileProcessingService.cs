using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;

namespace Gis.Net.Core.Tasks.FileProcessing;

/// <inheritdoc />
public class FileProcessingService : BackgroundService
{
    private readonly ConcurrentQueue<FileTask> _fileTasks = new();
    private readonly SemaphoreSlim _signal = new(0);

    /// <summary>
    /// Enqueues a file task to be processed.
    /// </summary>
    /// <param name="task">The file task to enqueue.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided file task is null.</exception>
    public void EnqueueFileTask(FileTask task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));
        _fileTasks.Enqueue(task);
        _signal.Release();
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _signal.WaitAsync(stoppingToken);
            if (_fileTasks.TryDequeue(out var task))
                // Execute the file processing task
                await task.Process(task.FilePath, stoppingToken);
        }
    }
}