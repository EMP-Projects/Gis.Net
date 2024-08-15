namespace Gis.Net.Osm.OsmPg.Models;

public interface IOsmPgModel
{
    long? OsmId { get; set; }
    string? Name { get; set; }
    Dictionary<string, string>? Tags { get; set; }
}