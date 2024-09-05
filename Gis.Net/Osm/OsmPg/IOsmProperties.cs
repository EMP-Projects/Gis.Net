namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Represents the properties of an OpenStreetMap entity.
/// </summary>
public interface IOsmProperties
{
    /// <summary>
    /// Represents the properties of an OpenStreetMap entity.
    /// </summary>
    string? Type { get; set; }
    /// <summary>
    /// Represents the properties of an OpenStreetMap entity.
    /// </summary>
    string[]? Tags { get; set; }
    /// <summary>
    /// Gets or sets the name of the OpenStreetMap entity.
    /// </summary>
    string Name { get; set; }
}