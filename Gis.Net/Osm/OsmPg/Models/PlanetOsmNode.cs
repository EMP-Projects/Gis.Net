using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a node in the Planet OSM data model.
/// </summary>
[Table("planet_osm_nodes")]
public partial class PlanetOsmNode : IOsmPgGenericModel
{
    /// <summary>
    /// Gets or sets the identifier of the node.
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the latitude of the object.
    /// </summary>
    /// <remarks>
    /// The latitude value represents the geographic coordinate of the object
    /// in decimal degrees, indicating its position north or south of the equator.
    /// </remarks>
    /// <value>
    /// The latitude value.
    /// </value>
    [Column("lat")]
    public int Lat { get; set; }

    /// <summary>
    /// Represents the longitude (lon) property of a PlanetOsmNode object.
    /// </summary>
    /// <remarks>
    /// The lon property stores the longitude value of a geographic coordinate associated with a PlanetOsmNode object.
    /// </remarks>
    [Column("lon")]
    public int Lon { get; set; }
}
