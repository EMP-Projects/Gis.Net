using Gis.Net.Core.Tasks.FileProcessing;
using Gis.Net.Core.Tasks.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Static class for managing the addition of various background tasks.
/// </summary>
public static class TasksManager
{
    /// <summary>
    /// Adds notification tasks as a hosted service.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddNotificationService(this IServiceCollection services)
    {
        services.AddSingleton<NotificationService>();
        services.AddHostedService<NotificationService>(provider => provider.GetService<NotificationService>()!);
        return services;
    }

    /// <summary>
    /// Adds file processing tasks as a hosted service.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddFileProcessTasks(this IServiceCollection services)
    {
        services.AddHostedService<FileProcessingService>();
        return services;
    }
}