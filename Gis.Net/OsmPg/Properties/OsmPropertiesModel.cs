using System.ComponentModel.DataAnnotations.Schema;
using EcoSensorApi.Osm.Properties;
using Gis.Net.Core.Entities;
using Gis.Net.OsmPg;
using Gis.Net.Vector.Models;

namespace Gis.Net.VectorCore.OsmPg.Properties;

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