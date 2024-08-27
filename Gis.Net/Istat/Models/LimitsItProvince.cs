using System.ComponentModel.DataAnnotations.Schema;

namespace Gis.Net.Istat.Models;

/// <inheritdoc cref="ILimitsProvince" />
[Table("limits_it_province")]
public partial class LimitsItProvince : LimitsItRegion, ILimitsProvince
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
}
