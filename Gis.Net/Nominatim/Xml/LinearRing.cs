using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "LinearRing")]
public class LinearRing
{
    [XmlElement(ElementName = "coordinates")]
    public string? Coordinates { get; set; }
}