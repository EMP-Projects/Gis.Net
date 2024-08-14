using Microsoft.EntityFrameworkCore;

namespace TeamSviluppo.Services;

/// <summary>
/// Interfaccia che descrive il servizio che ha il solo scopo di utilizzare il DbContext definito nel progetto padre all'interno del core
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Restituisce il DbContext di progetto
    /// </summary>
    /// <returns></returns>
    DbContext GetDbContext();
}