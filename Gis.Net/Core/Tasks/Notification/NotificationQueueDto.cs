// ---------------------------------------------
// Project: netcorefw
// Created at: 16:44
// Company: Hextra srl
// Dev: Mauro Revil
// ---------------------------------------------

namespace Gis.Net.Core.Tasks.Notification;

/// <summary>
/// Oggetto che lega un <see cref="INotificationHandler"/> con la data di prossima esecuzione
/// </summary>
public class NotificationQueueDto
{
    /// <summary>
    /// La data in cui il task deve essere eseguito
    /// </summary>
    public required DateTime NextExecutionTime { get; set; }
    /// <summary>
    /// L'handler che processa il task
    /// </summary>
    public required INotificationHandler Handler { get; set; }

    /// <summary>
    /// Se è true, l'handler non deve essere più eseguito
    /// </summary>
    public bool Executed { get; set; } = false;

    /// <summary>
    /// Numero di tentativi consecutivi falliti
    /// </summary>
    public int FailedAttempts { get; set; } = 0;
}