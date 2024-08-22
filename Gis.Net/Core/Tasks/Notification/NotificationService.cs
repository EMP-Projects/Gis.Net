using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks.Notification;

/// <inheritdoc />
public partial class NotificationService : BackgroundService
{
    private readonly ILogger<NotificationService> _logger;

    private const string LogPrefix = $"[{nameof(NotificationService)}]";

    private static string WithPrefix(string message) => $"{LogPrefix} {message}";

    private readonly List<NotificationQueueDto> _queue = new();

    /// <inheritdoc />
    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Logs a message with the specified content.
    /// </summary>
    /// <param name="message">The message to log.</param>
    protected void LogMessage(string message) => _logger.LogInformation(WithPrefix(message));

    /// <summary>
    /// Logs an error message along with exception details.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="error">The error message.</param>
    protected void LogError(Exception ex, string error) => _logger.LogError(WithPrefix($"{error} \r\n {ex.Message}"));

    /// <summary>
    /// Adds a notification handler to the queue with a specified due time.
    /// </summary>
    /// <param name="handler">The INotificationHandler instance which contains the logic to be executed when the notification is due.</param>
    public void AddNotificationHandler(INotificationHandler handler)
    {
        // Calculate the first running time based on the DueTime of the handler
        var firstRunningTime = DateTime.Now;
        if (handler.DueTime.HasValue) firstRunningTime = firstRunningTime.Add(handler.DueTime.Value);

        // Log the action
        LogMessage($"Adding the notification handler \"{handler.Name}\" to the queue. First expected execution {firstRunningTime:g}");
    
        // Creates a new queue DTO with the handler and its next execution time
        var queueDto = new NotificationQueueDto
        {
            NextExecutionTime = firstRunningTime,
            Handler = handler
        };

        // Add to the queue
        _queue.Add(queueDto);

        // Log the current status of the queue
        LogMessage($"The queue now contains {_queue.Count} tasks");
    }

    /// <summary>
    /// this method runs right after the app goes up
    /// every 10 seconds I check if there is a task to process in the queue
    /// if I find it, I run the task and wait for it to finish
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));
        while (!stoppingToken.IsCancellationRequested)
        {
            var handlerToRun = _queue
                .Where(f => f.NextExecutionTime <= DateTime.Now && f.Executed == false)
                .MinBy(f => f.NextExecutionTime);
            if (handlerToRun is not null)
            {
                var handler = handlerToRun.Handler;
                // To prevent the flow from blocking, I make sure to trap any errors to avoid blocking the API
                try
                {
                    LogMessage($"pronto per eseguire l'handler {handler.Name}");
                    await handler.HandleNotificationsAsync();
                    LogMessage($"handler {handler.Name} concluso con successo");
                    // if periodic task, I modify the next run time otherwise, I mark it as executed to be ignored in subsequent rounds
                    if (handler.Period != null)
                    {
                        handlerToRun.NextExecutionTime = handlerToRun.NextExecutionTime.Add(handler.Period.Value);
                        handlerToRun.FailedAttempts = 0; // I reset the number of failed attempts
                        LogMessage($"next run for handler {handler.Name}: {handlerToRun.NextExecutionTime:g}");
                    }
                    else
                        handlerToRun.Executed = true;
                }
                catch (Exception e)
                {
                    LogError(e, $"Error executing NotificationHandler {handlerToRun.Handler.Name}");
                    // I'll change the next moment to run the handler
                    handlerToRun.NextExecutionTime = DateTime.Now.Add(handler.DelayOnError ?? TimeSpan.FromHours(1));
                    handlerToRun.FailedAttempts++;
                    if (handlerToRun.FailedAttempts >= 3)
                    {
                        LogMessage($"Once the maximum number of consecutive failed attempts has been reached, I stop the task {handlerToRun.Handler.Name}");
                        handlerToRun.Executed = true;
                    }
                }
            }

            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

    /// <summary>
    /// Calculates the time span until a specified due time.
    /// </summary>
    /// <param name="time">The time in HH:mm format for which to calculate the due time span.</param>
    /// <returns>A nullable TimeSpan representing the time until the due time, or null if the input is not in the correct format.</returns>
    /// <remarks>
    /// This method verifies that the input time is in the correct HH:mm format.
    /// If the due time has already passed, it is adjusted to be the next day.
    /// The resulting TimeSpan is the difference between the due time and the current time.
    /// </remarks>
    public static TimeSpan? CalculateDueTime(string time)
    {
        // I verify that hour is in the correct format HH:mm
        if (TaskRegex().IsMatch(time)) return null;
        var parseString = $"{DateTime.Today:yyyy-MM-dd}T{time}:00";
        var dueTime = DateTime.Parse(parseString);
        // if dueTime has passed, I change it to be the next day
        if (dueTime < DateTime.Now) dueTime = dueTime.AddDays(1);
        // at this point I calculate the TimeSpan between dueTime and now
        return dueTime - DateTime.Now;
    }

    [GeneratedRegex(@"^([01]\d|2[0-3]):([0-5]\d)$")]
    private static partial Regex TaskRegex();
}