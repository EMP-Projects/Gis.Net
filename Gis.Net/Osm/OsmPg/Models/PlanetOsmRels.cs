using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents the PlanetOsmRels table in the OsmPg database.
/// </summary>
[Table("planet_osm_rels")]
public partial class PlanetOsmRels : IOsmPgGenericModel
{
    /// <summary>
    /// Represents the ID of a PlanetOsmRels object.
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Represents the WayOff property of the PlanetOsmRels class.
    /// </summary>
    /// <remarks>
    /// The WayOff property represents an offset for a way.
    /// </remarks>
    [Column("way_off")]
    public short? WayOff { get; set; }

    /// <summary>
    /// Represents a class that maps to the "planet_osm_rels" table in the database.
    /// </summary>
    [Column("rel_off")]
    public short? RelOff { get; set; }

    /// <summary>
    /// Represents a partial class for the 'planet_osm_rels' table in the OsmPg database.
    /// </summary>
    [Column("parts")]
    public List<long>? Parts { get; set; }

    /// <summary>
    /// Represents a list of members for a specific PlanetOsmRels object.
    /// </summary>
    /// <remarks>
    /// The Members property contains a list of members associated with a PlanetOsmRels object.
    /// Each member is represented by a string value.
    /// </remarks>
    /// <seealso cref="PlanetOsmRels"/>
    [Column("members")]
    public List<string>? Members { get; set; }

    /// <summary>
    /// Represents the PlanetOsmRels table in the OsmPg database.
    /// </summary>
    [Column("tags")]
    public List<string>? Tags { get; set; }
}
