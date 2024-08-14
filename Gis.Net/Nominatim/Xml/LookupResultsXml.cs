using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "lookupresults")]
public class LookupResultsXml
{
    [XmlElement(ElementName = "place")]
    public List<Place>? Place { get; set; }
    
    [XmlAttribute(AttributeName = "timestamp")]
    public string? Timestamp { get; set; }
    
    [XmlAttribute(AttributeName = "attribution")]
    public string? Attribution { get; set; }

    [XmlAttribute(AttributeName = "querystring")]
    public string? Querystring { get; set; }

    [XmlAttribute(AttributeName = "more_url")]
    public string? MoreUrl { get; set; }
}