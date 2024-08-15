using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Tasks;

/// <summary>
/// Task di servizio che esegue le migrazioni del database all'avvio dell'applicazione.
/// </summary>
public class DbMigrationsTask<T> : TaskService where T : DbContext
{
    private readonly T _dbContext;

    /// <summary>
    /// Costruttore per DbMigrationsTask che inietta le dipendenze necessarie.
    /// </summary>
    /// <param name="logger">Il logger per registrare i messaggi di log.</param>
    /// <param name="serviceProvider">Il fornitore di servizi per la risoluzione delle dipendenze.</param>
    /// <param name="dbContext"></param>
    public DbMigrationsTask(ILogger<DbMigrationsTask<T>> logger, IServiceProvider serviceProvider, T dbContext) : base(logger,
        serviceProvider)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Il tempo di attesa iniziale prima di eseguire il task (5 secondi)
    /// </summary>
    protected override TimeSpan DueTime => TimeSpan.FromSeconds(5);

    /// <summary>
    /// Il periodo di esecuzione del task. In questo caso, il task viene eseguito una sola volta.
    /// </summary>
    protected override TimeSpan Period => Timeout.InfiniteTimeSpan;

    /// <summary>
    /// Esegue le operazioni di migrazione del database.
    /// </summary>
    /// <param name="state">Lo stato associato al task. Può essere null.</param>
    protected override void ExecuteJob(object? state)
    {
        var job = _dbContext.RunMigrations();
        if (job.Wait(TimeSpan.FromMinutes(1)))
            Logger.LogInformation("Task di lancio migrazioni automatiche concluso");
    }
}