namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a model for OpenStreetMap entities with PostGIS geometries.
/// </summary>
public interface IOsmPgModel
{
    /// <summary>
    /// Represents the OsmId property.
    /// </summary>
    long? OsmId { get; set; }

    /// <summary>
    /// Represents the Name property.
    /// </summary>
    string? Name { get; set; }
    
    /// <summary>
    /// Represents the tag properties for an OpenStreetMap entity.
    /// </summary>
    Dictionary<string, string>? Tags { get; set; }
}