using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a class that defines the structure of the "planet_osm_ways" table in the OsmPg database.
/// </summary>
[Table("planet_osm_ways")]
public partial class PlanetOsmWays : IOsmPgGenericModel
{
    /// <summary>
    /// Represents the unique identifier of a property in the PlanetOsmWays table.
    /// </summary>
    /// <remarks>
    /// This property is defined in the PlanetOsmWays class, which is a partial class representing a table in
    /// the OsmPg database. It is used to store the unique identifier of a property.
    /// The PlanetOsmWays table is mapped to the "planet_osm_ways" table in the database and contains other columns
    /// such as Nodes and Tags.
    /// The property Id is decorated with the [Key] and [Column("id")] attributes to signify that it is the primary key
    /// of the table and is mapped to the "id" column in the database.
    /// </remarks>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Represents a model for the PlanetOsmWays table in the OsmPg database.
    /// </summary>
    [Column("nodes")]
    public List<long> Nodes { get; set; } = null!;

    /// <summary>
    /// Represents a model for storing OpenStreetMap ways in the OsmPg database.
    /// </summary>
    [Column("tags")]
    public List<string>? Tags { get; set; }
}
