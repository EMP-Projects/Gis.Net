
using Gis.Net.Vector;

namespace Gis.Net.Osm.Overpass;

public class OverPassOptions : GisOptions
{
    public Dictionary<string, List<string>> Query { get; set; }

    public OverPassOptions(string geomFilter) : base(geomFilter)
    {
        Query = new Dictionary<string, List<string>>();
    }

    public OverPassDelegate.QueryDelegate? OnQuery { get; set; }
}