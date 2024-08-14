using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "geokml")]
public class GeoKml
{
    [XmlElement(ElementName = "Polygon")]
    public Polygon? Polygon { get; set; }
}