namespace Gis.Net.Osm.OsmPg;

public class OsmProperties : IOsmProperties
{
    public required string Type { get; set; }
    public required string[] Tags { get; set; }
    public string? Name { get; set; }
}