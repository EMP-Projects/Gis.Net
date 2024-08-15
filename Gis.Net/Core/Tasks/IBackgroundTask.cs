namespace Gis.Net.Core.Tasks;

/// <summary>
/// Interface for defining a background task with periodic execution.
/// </summary>
public interface IBackgroundTask
{
    /// <summary>
    /// Gets or sets the period of time between each invocation of the task.
    /// </summary>
    /// <value>
    /// The time interval between task invocations.
    /// </value>
    TimeSpan? Period { get; set; }

    /// <summary>
    /// Gets or sets the amount of time to delay before the task is first invoked.
    /// </summary>
    /// <value>
    /// The time to wait before the task's first invocation.
    /// </value>
    TimeSpan? DueTime { get; set; }
    
    /// <summary>
    /// The time the service wait before run an handler if an error occurs
    /// </summary>
    TimeSpan? DelayOnError { get; set; }
}