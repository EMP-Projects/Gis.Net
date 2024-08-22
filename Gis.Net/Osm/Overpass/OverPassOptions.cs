
using Gis.Net.Vector;

namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents the options for executing an Overpass query.
/// </summary>
public class OverPassOptions : GisOptions
{
    /// <summary>
    /// Represents the options for an OverPass query.
    /// </summary>
    public Dictionary<string, List<string>> Query { get; set; }

    /// <summary>
    /// Represents the options for an OverPass query.
    /// </summary>
    public OverPassOptions(string geomFilter) : base(geomFilter)
    {
        Query = new Dictionary<string, List<string>>();
    }

    /// <summary>
    /// Delegate query to tag openstreetmap
    /// </summary>
    public OverPassDelegate.QueryDelegate? OnQuery { get; set; }
}