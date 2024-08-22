namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents the interface for OSM Overpass tags.
/// </summary>
public interface IOsmOverPassTags
{
    /// <summary>
    /// Represents a key tag used in the OsmOverPass API.
    /// </summary>
    string KeyTag { get; set; }

    /// <summary>
    /// Represents a value tag used in the Overpass query.
    /// </summary>
    string ValueTag { get; set; }

    /// <summary>
    /// Represents the BBox (Bounding Box) property in Overpass query tags.
    /// </summary>
    string? BBox { get; set; }

    /// <summary>
    /// Represents a query for OSM Overpass tags.
    /// </summary>
    string? Query { get; }
}