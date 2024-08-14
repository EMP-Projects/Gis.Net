using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "result")]
public class Result
{
    [XmlAttribute(AttributeName = "place_id")]
    public string? PlaceId { get; set; }
    
    [XmlAttribute(AttributeName = "osm_type")]
    public string? OsmType { get; set; }
    
    [XmlAttribute(AttributeName = "osm_id")]
    public string? OsmId { get; set; }
    
    [XmlText]
    public string? Text { get; set; }
}