using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <inheritdoc cref="IOsmProperties" />
[Table("osm_properties")]
public class OsmPropertiesModel : ModelBase, IOsmProperties
{
    [Column("type")]
    public required string Type { get; set; }
    
    [Column("tags")]
    public required string[] Tags { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
}