namespace Gis.Net.Osm.OsmPg;

/// <inheritdoc />
public class OsmProperties : IOsmProperties
{
    /// <inheritdoc />
    public required string? Type { get; set; }

    /// <inheritdoc />
    public required string[]? Tags { get; set; } = [];
    
    /// <inheritdoc />
    public string? Name { get; set; }
}