using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.Osm.OsmPg.Models;

[Table("planet_osm_rels")]
public partial class PlanetOsmRels : IOsmPgGenericModel
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("way_off")]
    public short? WayOff { get; set; }

    [Column("rel_off")]
    public short? RelOff { get; set; }

    [Column("parts")]
    public List<long>? Parts { get; set; }

    [Column("members")]
    public List<string>? Members { get; set; }

    [Column("tags")]
    public List<string>? Tags { get; set; }
}
