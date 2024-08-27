using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Gis.Net.Istat.Models;

/// <inheritdoc />
[Table("limits_it_region")]
public partial class LimitsItRegion : ILimitsRegion
{
    /// <inheritdoc />
    [Column("ogc_fid")]
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

    /// <inheritdoc />
    [Column("wkb_geometry")]
    public Geometry? WkbGeometry { get; set; }
}
