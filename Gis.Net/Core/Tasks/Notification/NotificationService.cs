using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks.Notification;

/// <inheritdoc />
public class NotificationService : BackgroundService
{
    private readonly ILogger<NotificationService> _logger;

    /// <summary>
    /// Prefisso messo in cima a tutti i messaggi di log per riconoscere i log del NotificationService
    /// </summary>
    const string LogPrefix = $"[{nameof(NotificationService)}]";

    /// <summary>
    /// Aggiunge il prefisso al messaggio dei log
    /// </summary>
    /// <param name="message">il messaggio da loggare</param>
    /// <returns>il messaggio con prefisso</returns>
    private string WithPrefix(string message) => $"{LogPrefix} {message}";

    /// <summary>
    /// Elenco dei task che devo processare
    /// </summary>
    private readonly List<NotificationQueueDto> _queue = new();

    /// <inheritdoc />
    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Aggiunge un handler alla coda dei task
    /// </summary>
    /// <param name="handler"></param>
    public void AddNotificationHandler(INotificationHandler handler)
    {
        // aggiungo l'handler alla coda
        // prima determino il primo istante in cui deve essere eseguito
        var firstRunningTime = DateTime.Now;
        if (handler.DueTime.HasValue) firstRunningTime = firstRunningTime.Add(handler.DueTime.Value);
        _logger.LogInformation(WithPrefix(
            $"aggiungo il NotificationHandler \"{handler.Name}\" alla coda, prima esecuzione prevista {firstRunningTime:g}"));
        var queueDto = new NotificationQueueDto()
        {
            NextExecutionTime = firstRunningTime,
            Handler = handler
        };
        _queue.Add(queueDto);
        _logger.LogInformation(WithPrefix($"ora la coda contiene {_queue.Count} tasks"));
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // questo metodo viene eseguito subito dopo che l'app è salita
        // ogni 10 secondi verifico se c'è un task da processare nella coda
        // se lo trovo, eseguo il task e aspetto che questo finisca
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));
        while (!stoppingToken.IsCancellationRequested)
        {
            var handlerToRun = _queue
                .Where(f => f.NextExecutionTime <= DateTime.Now && f.Executed == false)
                .MinBy(f => f.NextExecutionTime);
            if (handlerToRun is not null)
            {
                var handler = handlerToRun.Handler;
                // per impedire che il flusso si blocchi, faccio in modo di intercettare eventuali errori
                // se non facessi così, l'esecuzione del programma si bloccherebbe e le API non sarebbero più disponibili
                try
                {
                    _logger.LogInformation(WithPrefix($"pronto per eseguire l'handler {handler.Name}"));
                    await handler.HandleNotificationsAsync();
                    _logger.LogInformation(WithPrefix($"handler {handler.Name} concluso con successo"));
                    // ora, se è un task periodico, modifico il next run time
                    // altrimenti, lo segno come eseguito per essere ignorato nei round successivi
                    if (handler.Period != null)
                    {
                        handlerToRun.NextExecutionTime = handlerToRun.NextExecutionTime.Add(handler.Period.Value);
                        handlerToRun.FailedAttempts = 0; // resetto il numero di tentativi falliti
                        _logger.LogInformation(WithPrefix(
                            $"prossima esecuzione per handler {handler.Name}: {handlerToRun.NextExecutionTime:g}"));
                    }
                    else
                    {
                        handlerToRun.Executed = true;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(
                        WithPrefix($"Errore durante l'esecuzione del NotificationHandler {handlerToRun.Handler.Name}"));
                    // modifico il prossimo istante per l'esecuzione dell'handler
                    handlerToRun.NextExecutionTime = DateTime.Now.Add(handler.DelayOnError ?? TimeSpan.FromHours(1));
                    handlerToRun.FailedAttempts++;
                    if (handlerToRun.FailedAttempts >= 3)
                    {
                        _logger.LogInformation($"Raggiunto il numero massimo di tentativi falliti consecutivi, fermo il task {handlerToRun.Handler.Name}");
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
        // verifico che hour sia nel formato corretto HH:mm
        if (Regex.IsMatch(time, @"^([01]\d|2[0-3]):([0-5]\d)$")) return null;
        
        var parseString = $"{DateTime.Today:yyyy-MM-dd}T{time}:00";
        var dueTime = DateTime.Parse(parseString);
        // se dueTime è passato, lo modifico per essere il giorno successivo
        if (dueTime < DateTime.Now) dueTime = dueTime.AddDays(1);
        // a questo punto calcolo il TimeSpan che intercorre fra dueTime e adesso
        return dueTime - DateTime.Now;
    }
}