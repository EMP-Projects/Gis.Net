using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Gis.Net.Istat.Models;

/// <inheritdoc cref="ILimitsMunicipality" />
[Table("limits_it_municipality")]
[Index(nameof(Name))]
public partial class LimitsItMunicipality : ILimitsMunicipality, ILimitsRegion, ILimitsProvince
{
    /// <inheritdoc />
    [Column("name")]
    public string? Name { get; set; }

    /// <inheritdoc />
    [Column("op_id")]
    public string? OpId { get; set; }

    /// <inheritdoc />
    [Column("name_de")]
    public string? NameDe { get; set; }

    /// <inheritdoc />
    [Column("name_sl")]
    public string? NameSl { get; set; }

    /// <inheritdoc />
    [Column("minint_elettorale")]
    public string? MinintElettorale { get; set; }

    /// <inheritdoc />
    [Column("minint_finloc")]
    public string? MinintFinloc { get; set; }

    /// <inheritdoc />
    [Column("name_it")]
    public string? NameIt { get; set; }

    /// <inheritdoc />
    [Column("opdm_id")]
    public string? OpdmId { get; set; }

    /// <inheritdoc />
    [Column("com_catasto_code")]
    public string? ComCatastoCode { get; set; }

    /// <inheritdoc />
    [Column("com_istat_code")]
    public string? ComIstatCode { get; set; }

    /// <inheritdoc />
    [Column("com_istat_code_num")]
    public int? ComIstatCodeNum { get; set; }

    /// <inheritdoc />
    [Column("wkb_geometry")]  
    public MultiPolygon? WkbGeometry { get; set; }
    
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
    
    /// <summary>
    /// Represents the unique identifier for the OGC feature.
    /// </summary>
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
}
