using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Gis.Net.Istat.Models;

/// <inheritdoc cref="ILimitsProvince" />
[Table("limits_it_province")]
[Index(nameof(ProvName))]
public class LimitsItProvince : ILimitsProvince, ILimitsRegion
{
    /// <inheritdoc />
    [Column("prov_name")]
    public string? ProvName { get; set; }

    /// <inheritdoc />
    [Column("prov_istat_code_num")]
    public int? ProvIstatCodeNum { get; set; }

    /// <inheritdoc />
    [Column("prov_acr")]
    public string? ProvAcr { get; set; }

    /// <inheritdoc />
    [Column("prov_istat_code")]
    public string? ProvIstatCode { get; set; }
    
    [Column("ogc_fid"), Key]
    public int OgcFid { get; set; }

    /// <inheritdoc />
    [Column("reg_name")]
    public string? RegName { get; set; }

    /// <inheritdoc />
    [Column("reg_istat_code_num")]
    public int? RegIstatCodeNum { get; set; }

    /// <inheritdoc />
    [Column("reg_istat_code")]
    public string? RegIstatCode { get; set; }

    [Column("wkb_geometry")]
    public Geometry? WkbGeometry { get; set; }
}
