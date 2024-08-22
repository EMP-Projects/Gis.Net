using Gis.Net.Osm.Overpass;

namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents a set of OpenStreetMap Overpass tags.
/// </summary>
public class OsmOverPassOverPassTags : IOsmOverPassTags
{
    /// <summary>
    /// Represents a key tag for OSM Overpass queries.
    /// </summary>
    public string KeyTag { get; set; }

    /// <summary>
    /// Represents a value tag used in the OSM Overpass query.
    /// </summary>
    public string ValueTag { get; set; }

    /// <summary>
    /// Represents a set of OpenStreetMap (OSM) Overpass tags, used for querying data within a specified bounding box.
    /// </summary>
    public string? BBox { get; set; }

    /// <summary>
    /// Represents the implementation of OSM Overpass tags.
    /// </summary>
    public OsmOverPassOverPassTags(string key, string value)
    {
        KeyTag = key;
        ValueTag = value;
    }

    /// <summary>
    /// Represents the OSM Overpass tags.
    /// </summary>
    public OsmOverPassOverPassTags(string key, string value, string? bbox) : this(key, value)
    {
        BBox = bbox;
    }

    /// <summary>
    /// Represents a tag for OSM Overpass.
    /// </summary>
    public string Tag => ValueTag == "*" ? $"{KeyTag}" : $"\"{KeyTag}\"=\"{ValueTag}\"";

    /// <summary>
    /// Represents a property query for OSM Overpass.
    /// </summary>
    public string Query => $"node[{Tag}]({BBox});way[{Tag}]({BBox});relation[{Tag}]({BBox});";
}