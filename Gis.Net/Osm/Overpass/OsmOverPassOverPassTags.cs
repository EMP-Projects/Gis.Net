using Gis.Net.Osm.Overpass;

namespace Gis.Net.Osm.Overpass;

public class OsmOverPassOverPassTags : IOsmOverPassTags
{
    public string KeyTag { get; set; }
    
    public string ValueTag { get; set; }

    public string? BBox { get; set; }
    
    public OsmOverPassOverPassTags(string key, string value)
    {
        KeyTag = key;
        ValueTag = value;
    }
    
    public OsmOverPassOverPassTags(string key, string value, string? bbox) : this(key, value)
    {
        BBox = bbox;
    }

    public string Tag => ValueTag == "*" ? $"{KeyTag}" : $"\"{KeyTag}\"=\"{ValueTag}\"";
    
    public string Query => $"node[{Tag}]({BBox});way[{Tag}]({BBox});relation[{Tag}]({BBox});";
}