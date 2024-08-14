using Gis.Net.Osm;
using Gis.Net.Vector;
using NetTopologySuite.Geometries;
using TeamSviluppo.Gis;
using Gis.Net.Osm.Overpass.Dto;
using TeamSviluppo.Gis.Overpass;

namespace Gis.Net.Osm.Overpass;

public static class OverPassTags
{   
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
    /// <param name="propertyValue"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    private static string? CheckTag(string propertyValue, OverPassOptions options)
    {
        var prop = propertyValue.Split(":");

        if (prop.Length != 2)
            return null;

        options.Query.TryGetValue(prop[0], out var valuesTag);
        var valueTag = valuesTag?.FirstOrDefault(v1 => v1 == prop[1]);
        return valueTag;
    }
    
    public static Dictionary<EOsmTag, List<OsmOverPassOverPassTags>> GetTags(EOsmTag tag, Geometry geom)
    {
        var bbox = GisUtility.CoordinatesBBoxFromGeometry(geom);
        return GetTags(tag, bbox);
    }

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