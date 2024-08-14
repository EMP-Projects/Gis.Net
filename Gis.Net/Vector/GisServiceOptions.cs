using Gis.Net.Vector;

namespace TeamSviluppo.Gis;

public abstract class GisServiceOptions<T> : GisOptions where T : class
{

    public GisDelegate.SortFromDelegate? OnSort { get; set; }
    
    protected GisServiceOptions(string geomFilter) : base(geomFilter)
    {
    }
}