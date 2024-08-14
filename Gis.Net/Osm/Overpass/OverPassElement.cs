using Gis.Net.Osm;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.Overpass;

public class OverPassElement
{

    public EOsmTag Tag { get; private set; }

    public int SrCode { get; set; }
    
    public Dictionary<string, List<string>>? Query { get; set; }
    
    public OverPassElement(EOsmTag tag) => Tag = tag;
    
    public string GetPayload(Geometry geom) {
        var tags = OverPassTags.GetTags(Tag, geom);
        var queries = tags[Tag].Select(t => {
            if (Query?.Count == 0 && t.ValueTag == "*") return t.Query;
            var keyExists = Query!.TryGetValue(t.KeyTag, out var value);
            if (!keyExists || value is null) return string.Empty;
            return value.Contains(t.ValueTag) ? t.Query : string.Empty;
        }).Where(q => !string.IsNullOrEmpty(q))
          .ToArray();
            
        return queries.Length > 0 ? string.Join("", queries) : string.Empty;
    }
}