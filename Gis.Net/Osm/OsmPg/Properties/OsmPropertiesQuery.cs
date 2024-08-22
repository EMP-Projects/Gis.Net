using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;
namespace Gis.Net.Osm.OsmPg.Properties;

/// <inheritdoc cref="IOsmProperties" />
public class OsmPropertiesQuery : QueryBase, IOsmProperties
{
    /// <inheritdoc />
    [FromQuery(Name = "type")]
    public string? Type { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "tags")]
    public string[]? Tags { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}