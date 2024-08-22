namespace Gis.Net.Core.Tasks.Notification;

/// <summary>
/// Represents a notification handler in the notification queue.
/// </summary>
public class NotificationQueueDto
{
    /// <summary>
    /// The next execution time of the notification handler.
    /// </summary>
    public required DateTime NextExecutionTime { get; set; }

    /// <summary>
    /// Represents a DTO object for the notification queue.
    /// </summary>
    public required INotificationHandler Handler { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task associated with the <see cref="NotificationQueueDto"/> object has been executed.
    /// </summary>
    /// <value>
    /// <c>true</c> if the task has been executed; otherwise, <c>false</c>.
    /// </value>
    public bool Executed { get; set; } = false;

    /// <summary>
    /// Represents the number of failed attempts for a notification handler in the NotificationService.
    /// </summary>
    /// <remarks>
    /// The <c>FailedAttempts</c> property keeps track of the number of consecutive failed attempts made to execute a notification handler in the <see cref="NotificationService"/>.
    /// If a handler fails to complete successfully for a configurable number of times, it will be marked as executed and ignored in subsequent rounds.
    /// The <c>FailedAttempts</c> property is incremented every time a handler fails to execute and is reset to 0 when the handler completes successfully.
    /// </remarks>
    public int FailedAttempts { get; set; } = 0;
}