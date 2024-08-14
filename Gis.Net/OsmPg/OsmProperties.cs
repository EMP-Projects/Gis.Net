using Gis.Net.OsmPg;

namespace EcoSensorApi.Osm.Properties;

public class OsmProperties : IOsmProperties
{
    public required string Type { get; set; }
    public required string[] Tags { get; set; }
    public string? Name { get; set; }
}