using Gis.Net.Vector;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.Overpass;


/// <summary>
/// Provides utility methods for parsing and manipulating Overpass tags.
/// </summary>
public static class OverPassTags
{
    /// <summary>
    /// Parse tags from a property value string and return a list of valid tags.
    /// </summary>
    /// <param name="propertyValue">The property value string containing tags.</param>
    /// <param name="options">The OverPassOptions for parsing the tags.</param>
    /// <returns>A list of valid tags extracted from the property value string.</returns>
    public static IEnumerable<string?>? ParseTags(string propertyValue, OverPassOptions options)
    {
        if (string.IsNullOrEmpty(propertyValue)) return null;
        propertyValue = propertyValue.Replace("{\n ", "", StringComparison.CurrentCultureIgnoreCase);
        propertyValue = propertyValue.Replace("\n}", "", StringComparison.CurrentCultureIgnoreCase);
        propertyValue = propertyValue.Replace("\"", "", StringComparison.CurrentCultureIgnoreCase);
        propertyValue = propertyValue.Replace(" ", "", StringComparison.CurrentCultureIgnoreCase);
        propertyValue = propertyValue.Replace("\n", "", StringComparison.CurrentCultureIgnoreCase);
            
        var values = propertyValue.Split(",");

        var listProps = values.ToList()
            .Select(v => string.IsNullOrEmpty(v) ? null : CheckTag(v, options))
            .Where(l => l is not null)
            .ToList();
            
        return listProps;
    }

    /// <summary>
    /// Check if tag within list queries
    /// </summary>
    /// <param name="propertyValue">The property value to check</param>
    /// <param name="options">The OverPass options</param>
    /// <returns>The matching tag value if found, otherwise null</returns>
    private static string? CheckTag(string propertyValue, OverPassOptions options)
    {
        var prop = propertyValue.Split(":");

        if (prop.Length != 2)
            return null;

        options.Query.TryGetValue(prop[0], out var valuesTag);
        var valueTag = valuesTag?.FirstOrDefault(v1 => v1 == prop[1]);
        return valueTag;
    }

    /// <summary>
    /// Gets the tags for the specified OsmTag and geometry.
    /// </summary>
    /// <param name="tag">The OsmTag to get the tags for.</param>
    /// <param name="geom">The geometry to get the tags for.</param>
    /// <returns>A dictionary containing the OsmTag and a list of corresponding OsmOverPassTags.</returns>
    public static Dictionary<EOsmTag, List<OsmOverPassOverPassTags>> GetTags(EOsmTag tag, Geometry geom)
    {
        var bbox = GisUtility.CoordinatesBBoxFromGeometry(geom);
        return GetTags(tag, bbox);
    }

    /// <summary>
    /// Retrieves a dictionary of tags for the specified OsmTag and geometry.
    /// </summary>
    /// <param name="tag">The OsmTag to retrieve tags for.</param>
    /// <param name="bbox"></param>
    /// <returns>A dictionary of tags for the specified OsmTag and geometry.</returns>
    private static Dictionary<EOsmTag, List<OsmOverPassOverPassTags>> GetTags(EOsmTag tag, string bbox)
    {
        Dictionary<EOsmTag, List<OsmOverPassOverPassTags>> result = new();
        var key = OsmTag.Tags(tag);
        var values = OsmTag.Items(tag).ToList();
        var listOsmTags = values.Select(value => new OsmOverPassOverPassTags(key, value, bbox)).ToList();
        result.Add(tag, listOsmTags);
        return result;
    }
}