using Gis.Net.Istat.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Istat;

/// <summary>
/// Represents the database context for ISTAT data.
/// </summary>
public interface IStatDbContext
{
    /// <summary>
    /// Gets or sets the DbSet for Italian municipalities.
    /// </summary>
    DbSet<LimitsItMunicipality>? LimitsItMunicipalities { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Italian provinces.
    /// </summary>
    DbSet<LimitsItProvince>? LimitsItProvinces { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for Italian regions.
    /// </summary>
    DbSet<LimitsItRegion>? LimitsItRegions { get; set; }
}