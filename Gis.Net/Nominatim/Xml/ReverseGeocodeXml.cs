using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "reversegeocode")]
public class ReverseGeocodeXml
{
    [XmlElement(ElementName = "result")]
    public Result? Result { get; set; }
    
    [XmlElement(ElementName = "addressparts")]
    public AddressParts? AddressParts { get; set; }
    
    [XmlAttribute(AttributeName = "timestamp")]
    public string? Timestamp { get; set; }
    
    [XmlAttribute(AttributeName = "querystring")]
    public string? Querystring { get; set; }
}