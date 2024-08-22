using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <summary>
/// Represents a model that holds the properties of an OpenStreetMap object.
/// </summary>
[Table("osm_properties")]
public class OsmPropertiesModel : ModelBase, IOsmProperties
{
    /// <inheritdoc />
    [Column("type")]
    public required string Type { get; set; }

    /// <inheritdoc />
    [Column("tags")]
    public required string[] Tags { get; set; }

    /// <inheritdoc />
    [Column("name")]
    public string? Name { get; set; }
}