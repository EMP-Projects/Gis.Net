using Gis.Net.Osm;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents an element in the OverPass query.
/// </summary>
public class OverPassElement
{

    /// Represents an OverPass element.
    /// <summary>
    /// The OverPassElement class represents an element in the OverPass query result.
    /// </summary>
    public EOsmTag Tag { get; private set; }

    /// <summary>
    /// Represents a property for OverPassElement.
    /// </summary>
    public int SrCode { get; set; }

    /// <summary>
    /// Represents a query for OverPass tags.
    /// </summary>
    public Dictionary<string, List<string>>? Query { get; set; }

    /// <summary>
    /// Represents an Overpass element.
    /// </summary>
    public OverPassElement(EOsmTag tag) => Tag = tag;

    /// <summary>
    /// Generates the payload for a given geometry.
    /// </summary>
    /// <param name="geom">The geometry for which to generate the payload.</param>
    /// <returns>The generated payload as a string.</returns>
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