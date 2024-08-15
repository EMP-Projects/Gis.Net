# Tasks
Libreria per gestire i processi in tasks

## Data Processing System Task

Considera un'applicazione che richiede l'elaborazione notturna dei dati.
Questa elaborazione dei dati comporta l'interrogazione di un database,
l'esecuzione di calcoli complessi e l'aggiornamento del database con nuovi valori.
L'implementazione di questa operazione come operazione sincrona potrebbe bloccare
il thread principale, determinando un'esperienza utente scadente o timeout del sistema.

La classe del servizio deve implementare il metodo **ProcessAsync()**, che viene sovrascritto dalla classe base **AbstractDataProcessing**.

```C#
public class MyDataProcessiongTask : AbstractDataProcessing
{
    public override Task ProcessAsync()
    {
        Console.WriteLine("MyDataProcessiongTask");
        return Task.CompletedTask;
   }
}
```
Registrare **MyScheduleTasks** come come implementazione singleton dell'interfaccia IDataProcessor, 
ciò significa che MyDataProcessiongTask verrà utilizzato ogni volta che viene richiesto IDataProcessor.
AddBackgroundTasks registra i servizi delle attività in background.

```C#
    using TeamSviluppo.Tasks

    builder.Services.AddSingleton<IDataProcessor, MyDataProcessiongTask>();
    builder.Services.AddDataProcessingTasks();
```

## Notification System Task

Immagina un'applicazione che deve inviare notifiche in tempo reale agli utenti in base a trigger specifici o eventi pianificati.
Questa potrebbe essere una funzionalità fondamentale per app come piattaforme di e-commerce, social network o sistemi di gestione delle attività, 
dove aggiornamenti tempestivi possono migliorare significativamente l'esperienza dell'utente.

La classe del servizio deve implementare il metodo **HandleNotificationAsync()**, che viene sovrascritto dalla classe base **AbstractNotificationTask**.

```C#
public class MyNotificationTask : AbstractNotificationTask
{
    public override Task HandleNotificationAsync()
    {
        Console.WriteLine("MyNotificationTask");
        return Task.CompletedTask;
   }
}
```

Infine registrare il servizio:

```C#
    using TeamSviluppo.Tasks

    builder.Services.AddSingleton<INotificationHandler, MyNotificationTask>();
    builder.Services.AddNotificationTasks();
```

## Real-Time Notification System Task

Adatto in un sistema di notifica in tempo reale in un'applicazione web, in cui le notifiche devono essere inviate 
agli utenti in base a determinati eventi (ad esempio, nuovi messaggi, aggiornamenti di sistema). 
L'implementazione di questa funzionalità in modo sincrono potrebbe avere un impatto significativo sulle prestazioni, 
soprattutto con una base di utenti ampia.

```C#
public class MyRealtimeBaseTask : AbstractRealtimeBaseTask
{
    // specificando il numero iniziale di richieste che possono essere concesse contemporaneamente
    public override InitialNumberRequests { get; set; } = 0;
    
    // specificando il numero iniziale e massimo di richieste che possono essere concesse contemporaneamente
    public override MaxNumberRequests { get; set; } = 3;
    
    public override Task HandleNotificationAsync()
    {
        // Send email logic here
        Console.WriteLine($"Email sent to {notification.UserId}: {notification.Message}");
        return Task.CompletedTask;
   }
}
```
Infine registrare il servizio:

```C#
    using TeamSviluppo.Tasks

    builder.Services.AddSingleton<INotificationHandler, MyNotificationTask>();
    builder.Services.AddRealtimeTasks();
```
Per eseguire azioni in coda manualmente è necessario eseguire il metodo in qualsiasi punto dell'applicazione:

```C#
public void NotifyUser(string userId, string message)
{
    var notificationService = app.ApplicationServices.GetService<MyRealtimeBaseTask>();
    notificationService.EnqueueNotification(new Notification { UserId = userId, Message = message });
}
```
Oppure è possibile personalizzare esecuzione delle azioni utilizzando la classe astratta **AbstractRealtimeTask**, in questo caso non è necessario eseguire il metodo
EnqueueNotification ma basterà solamente registrare il servizio.

Ad esempio per creare il proprio servizio di invio mail:

```C#
public class MyRealtimeTask : AbstractRealtimeTask
{
    public override Task HandleAsync()
    {
        // Send email logic here
        Console.WriteLine($"Email sent to {notification.UserId}: {notification.Message}");
        return Task.CompletedTask;
   }
}
```

## Scheduler Jobs

**Scheduler Job** è adatto ad uno scenario in cui un'applicazione deve pianificare ed eseguire dinamicamente lavori in base agli input dell'utente o ai trigger esterni. 
Questi lavori potrebbero includere la generazione di report, l'esecuzione della sincronizzazione dei dati o l'esecuzione di operazioni batch. 
La sfida è gestire queste attività in modo efficiente, garantendo che non interferiscano con le prestazioni dell’applicazione o con l’esperienza dell’utente.

```C#
public class MyJobSchedulerTask : AbstractJobSchedulerTask
{
    public override Task CreateJobs()
    {
        ScheduleJob(new ScheduledJob
        {
            NextRunTime = DateTime.UtcNow.AddHours(1), // Run 1 hour from now
            Task = async cancellationToken =>
            {
                // Your data sync logic here
                Console.WriteLine("Data synchronization task executed.");
            },
            
            // Per migliorare ulteriormente il nostro sistema di pianificazione dei lavori, 
            // possiamo implementare una coda di priorità per garantire che i lavori con priorità più elevata vengano eseguiti per primi, 
            // soprattutto quando il sistema è sotto carico pesante.
            // E' necessario impostare la proprieta HasPriority = true (Default), altrimenti la priorità sarà data da NextRunTime
            Priority = 1
        });
   }
}
```

Infine registrare il servizio:

```C#
    using TeamSviluppo.Tasks

    builder.Services.AddSingleton<IJobSchedulerTask, MyJobSchedulerTask>();
    builder.Services.AddJobSchedulerTasks();
```

## File Processing
File Processing sono usati in uno scenario in cui un'applicazione deve elaborare in modo sicuro i file caricati dagli utenti 
(ad esempio, per cercare informazioni sensibili, convertire formati di file o estrarre dati), 
gestire questo processo in modo efficiente e sicuro, senza influire sull'esperienza dell'utente.

```C#
    using TeamSviluppo.Tasks

    builder.Service.AddFileProcessTasks();
```
Esempio di accodamento di un'attività di elaborazione di file in un punto qualsiasi dell'applicazione:

```C#
public void ProcessFile(IServiceProvider serviceProvider, string filePath)
{
    var fileProcessingService = serviceProvider.GetService<FileProcessingService>();
    fileProcessingService.EnqueueFileTask(new FileTask
    {
        FilePath = filePath,
        Process = async (path, cancellationToken) =>
        {
            // Implement secure file processing logic here
            Console.WriteLine($"Processing file: {path}");
        }
    });
}
```

## Tempo di esecuzione
I task hanno due proprietà **Period** (_Default: 1 giorno_) per impostare il periodo di tempo tra ogni chiamata dell'attività 
e **DueTime** (_Default: 5 minuti_) per il tempo di ritardo prima che l'attività venga richiamata per la prima volta.
Entrambi è necessario eseguire override della proprietà se si vuole cambiare i valori di default :

```C#
public override TimeSpan DueTime { get; set; } = TimeSpan.FromMinutes(5);
public override TimeSpan Period { get; set; } = TimeSpan.FromDays(3);
```