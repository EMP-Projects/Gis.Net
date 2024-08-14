using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.OsmPg.Models;

[Table("planet_osm_ways")]
public partial class PlanetOsmWays : IOsmPgGenericModel
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("nodes")]
    public List<long> Nodes { get; set; } = null!;

    [Column("tags")]
    public List<string>? Tags { get; set; }
}
