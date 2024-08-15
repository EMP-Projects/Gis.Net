# Notification Service

## Abstract

`NotificationService` è un servizio che esegue task in bacground, che possono essere anche ripetitivi.

## Come funziona

Essendo un background service, il cuore del servizio è il metodo `ExecuteAsync`.

Il servizio ha un loop che si ferma solo con il CancellationToken. Pertanto, il corpo del loop viene ripetuto all'infinito.

Tra un loop ed un altro, vengono attesi 10 secondi, grazie alla presenza di un `PeriodicTimer`.

Nel loop, viene recuperato dalla coda il primo elemento da eseguire:

```csharp
var handlerToRun = _queue
    .Where(f => f.NextExecutionTime <= DateTime.Now && f.Executed == false)
    .MinBy(f => f.NextExecutionTime);
```

Se l'elemento esiste, viene eseguito il suo handler.
Quindi, se si tratta di un task ricorrente, viene aggiornata la data della prossima esecuzione.
Se al contrario non è ricorrente, l'handler viene segnalato come eseguito e non sarà più processato

```csharp
_logger.LogInformation(WithPrefix($"pronto per eseguire l'handler {handler.Name}"));
await handler.HandleNotificationsAsync();
_logger.LogInformation(WithPrefix($"handler {handler.Name} concluso con successo"));
// ora, se è un task periodico, modifico il next run time
// altrimenti, lo segno come eseguito per essere ignorato nei round successivi
if (handler.Period != null)
{
    handlerToRun.NextExecutionTime = handlerToRun.NextExecutionTime.Add(handler.Period.Value);
    _logger.LogInformation(WithPrefix(
        $"prossima esecuzione per handler {handler.Name}: {handlerToRun.NextExecutionTime:g}"));
}
else
{
    handlerToRun.Executed = true;
}
```

## Aggiungere gli handler da eseguire

`NotificationService` usa una coda di `INotificationHandler` per recuperare i task da eseguire durante il loop.

Per aggiungere gli handler alla coda, `NotificationService` espone un metodo `AddNotificationHandler`:

```csharp
public void AddNotificationHandler(INotificationHandler handler)
```

Questo metodo imposta la data di prossima esecuzione del task in base al valore di **DueTime** specificato nell'handler.

Per iniettare gli handler che devono essere eseguiti all'avvio dell'applicazione, occorre creare un ulteriore `BackgroundService` che aggiunge i task.

```csharp
// ---------------------------------------------
// Project: tensolutions
// Created at: 10:35
// Company: Hextra srl
// Dev: Mauro Revil
// ---------------------------------------------

using TeamSviluppo.Tasks.Notification;

namespace tensolutions.Tasks;

public class NotificationServiceConfigurator : BackgroundService
{
    private readonly NotificationService _notificationService;
    private readonly TenSolScheduleAlertTask _scheduleAlertTask;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _notificationService.AddNotificationHandler(_scheduleAlertTask);
        return Task.CompletedTask;
    }

    public NotificationServiceConfigurator(NotificationService notificationService,
        TenSolScheduleAlertTask scheduleAlertTask)
    {
        _notificationService = notificationService;
        _scheduleAlertTask = scheduleAlertTask;
    }
}
```

Se invece fosse necessario aggiungere un handler al servizio in un determinato momento, è sufficiente avere `NotificationService` e l'handler di esecuzione del task come dipendenze nel costruttore.

## Come scrivere un handler

La scrittura di un handler richiede alcune regole che è fondamentale rispettare affinché non ci siano problemi.

Nel seguito c'è il codice di un handler usato in TenSolutions:

```csharp
// ---------------------------------------------
// Project: timereport
// Created at: 9:42
// Company: Hextra srl
// Dev: Mauro Revil
// ---------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamSviluppo.Exceptions;
using TeamSviluppo.Tasks.Notification;
using timereport.Services;

namespace timereport.Tasks;

/// <summary>
/// Task per inviare la mail con le prossime scadenze al destinatario finale
/// </summary>
public class ScheduleExpiringNotificationHandler : INotificationHandler
{
    /// <summary>
    /// Ripetizione del task: valore predefinito 1 volta al giorno
    /// </summary>
    public virtual TimeSpan? Period { get; set; } = TimeSpan.FromDays(1);

    /// <summary>
    /// Partenza posticipata, di default il task parte subito
    /// </summary>
    public virtual TimeSpan? DueTime { get; set; }

    public virtual string Name => "Notifica prossime scadenze";

    public async Task HandleNotificationsAsync()
    {
        // inizializzo un nuovo scope, in modo che sia sempre "nuovo" ogni volta che il task viene eseguito
        using var scope = _serviceProvider.CreateScope();
        // recupero il service che mi interessa
        var service = scope.ServiceProvider.GetRequiredService<ScheduleService>();
        if (service is null)
        {
            _logger.LogError(
                $"GetRequiredService fallita in {nameof(HandleNotificationsAsync)} del servizio {nameof(ScheduleExpiringNotificationHandler)}");
            throw new InternalException($"GetRequiredService fallita per IScheduleService");
        }

        var schedulesCount = await service.ListNextAndNotification(null);
        if (schedulesCount > 0)
            _logger.LogInformation($"Mail di notifica inviata con N.{schedulesCount} eventi schedulati");
    }

    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;

    public ScheduleExpiringNotificationHandler(ILogger logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
}
```

1. occorre definire le proprietà **DueTime**: questo valore definisce il tempo che deve passare prima che il task venga processato. Se viene lasciato `null`, il task verrà eseguito immediatamente;
2. occorre definire la proprietà **Period**: questo valore definisce quanto tempo deve passare tra una esecuzione e quella successiva di questo handler. Nell'esempio, il task viene eseguito ogni 24h
3. costruttore: nel costruttore è necessario importare almeno `IServiceProvider`, necessario per creare un nuovo _scope_ ad ogni esecuzione del task
4. nel metodo `HandleNotificationsAsync`, per prima cosa occorre creare lo scope; con lo _using_, lo scope nasce e muore con l'esecuzione del metodo (l'idea è quella per cui tutti i servizi Scoped vengano ricreati ad ogni esecuzione del task, in particolare il **DbContext**)

```csharp
// inizializzo un nuovo scope, in modo che sia sempre "nuovo" ogni volta che il task viene eseguito
var scope = _serviceProvider.CreateScope();
```

Non uso la direttiva `using` in quanto il dispose viene gestito automaticamente dalla dependency injection.

5. i servizi necessari per l'esecuzione del task vengono recuperati con `GetRequiredService` dallo scope, e occorre verificare che il servizio sia diverso da `null`

```csharp
if (service is null)
{
    _logger.LogError(
        $"GetRequiredService fallita in {nameof(HandleNotificationsAsync)} del servizio {nameof(ScheduleExpiringNotificationHandler)}");
    throw new InternalException($"GetRequiredService fallita per IScheduleService");
}
```
