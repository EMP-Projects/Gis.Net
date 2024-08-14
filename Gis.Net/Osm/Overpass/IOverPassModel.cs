using Gis.Net.Osm;

namespace Gis.Net.Osm.Overpass;

public interface IOverPassModel<T> where T : class
{
    ICollection<T>? Tags { get; set; }
    
    string? Query { get; set; }

    EOsmTag? Type { get; set; } 

    string? Description { get; set; }
}