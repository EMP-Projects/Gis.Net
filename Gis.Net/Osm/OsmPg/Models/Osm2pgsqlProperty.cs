using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a property used by Osm2Pgsql.
/// </summary>
[Table("osm2pgsql_properties")]
public partial class Osm2PgsqlProperty
{
    /// <summary>
    /// Represents a property in the osm2pgsql_properties table.
    /// </summary>
    [Key]
    [Column("property")]
    public string Property { get; set; } = null!;

    /// <summary>
    /// Represents a property value used in Osm2pgsql.
    /// </summary>
    [Column("value")]
    public string Value { get; set; } = null!;
}
