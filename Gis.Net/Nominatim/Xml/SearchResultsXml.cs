using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "searchresults")]
public abstract class SearchResultsXml
{
    [XmlElement(ElementName = "place")]
    public Place? Place { get; set; }
    
    [XmlAttribute(AttributeName = "timestamp")]
    public string? Timestamp { get; set; }
    
    [XmlAttribute(AttributeName = "querystring")]
    public string? Querystring { get; set; }
    
    [XmlAttribute(AttributeName = "polygon")]
    public string? Polygon { get; set; }
}