namespace Gis.Net.Osm.Overpass;

public interface IOsmOverPassTags
{
    string KeyTag { get; set; }
    
    string ValueTag { get; set; }

    string? BBox { get; set; }

    string? Query { get; }
}