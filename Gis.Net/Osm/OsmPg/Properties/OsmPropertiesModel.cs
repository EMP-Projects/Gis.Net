using System.ComponentModel.DataAnnotations;
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
    [Column("type"), MaxLength(100)]
    public string? Type { get; set; }

    /// <inheritdoc />
    [Column("tags")]
    public string[]? Tags { get; set; }

    /// <inheritdoc />
    [Column("name"), MaxLength(500)]
    public string? Name { get; set; }
}