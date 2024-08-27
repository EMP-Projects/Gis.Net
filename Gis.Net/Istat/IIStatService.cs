using Gis.Net.Istat.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Istat;

/// <summary>
/// Represents a service for accessing ISTAT data.
/// </summary>
/// <typeparam name="TContext">The type of the database context.</typeparam>
public interface IIStatService<TContext>
    where TContext : DbContext, IStatDbContext
{
    /// <summary>
    /// Retrieves a collection of regions based on the specified query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters to filter the regions.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of regions.</returns>
    Task<IEnumerable<LimitsItRegion>> GetRegions(LimitsItRegion? queryParams);

    /// <summary>
    /// Retrieves a collection of provinces based on the specified query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters to filter the provinces.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of provinces.</returns>
    Task<IEnumerable<LimitsItProvince>> GetProvince(LimitsItProvince? queryParams);

    /// <summary>
    /// Retrieves a collection of municipalities based on the specified query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters to filter the municipalities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of municipalities.</returns>
    Task<IEnumerable<LimitsItMunicipality>> GetMunicipalities(LimitsItMunicipality? queryParams);
}