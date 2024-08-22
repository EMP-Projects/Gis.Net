namespace Gis.Net.Core.Tasks.Notification;

/// <summary>
/// Interface for a notification handler that extends the background task functionality.
/// </summary>
public interface INotificationHandler : IBackgroundTask
{
    /// <summary>
    /// The name of the handler
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Asynchronously handles notifications.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of handling notifications.</returns>
    Task HandleNotificationsAsync();
}