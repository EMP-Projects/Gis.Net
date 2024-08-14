using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.OsmPg.Models;

[Table("osm2pgsql_properties")]
public partial class Osm2PgsqlProperty
{
    [Key]
    [Column("property")]
    public string Property { get; set; } = null!;

    [Column("value")]
    public string Value { get; set; } = null!;
}
