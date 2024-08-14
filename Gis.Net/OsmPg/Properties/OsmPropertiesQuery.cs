using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;
namespace Gis.Net.OsmPg.Properties;

/// <inheritdoc cref="Gis.Net.OsmPg.IOsmProperties" />
public class OsmPropertiesQuery : QueryBase, IOsmProperties
{
    [FromQuery(Name = "type")]
    public string? Type { get; set; }
    
    [FromQuery(Name = "tags")]
    public string[]? Tags { get; set; }
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}