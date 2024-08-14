using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.OsmPg.Models;

[Table("planet_osm_nodes")]
public partial class PlanetOsmNode : IOsmPgGenericModel
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("lat")]
    public int Lat { get; set; }

    [Column("lon")]
    public int Lon { get; set; }
}
