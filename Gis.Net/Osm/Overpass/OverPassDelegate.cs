using Gis.Net.Vector;

namespace TeamSviluppo.Gis.Overpass;

public class OverPassDelegate : GisDelegate
{
    /// <summary>
    /// Delegate query to tag openstreetmap
    /// </summary>
    public delegate Task<Dictionary<string, List<string>>> QueryDelegate();
}