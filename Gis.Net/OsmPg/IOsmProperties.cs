namespace Gis.Net.OsmPg;

public interface IOsmProperties
{
    string Type { get; set; }
    string[] Tags { get; set; }
    string Name { get; set; }
}