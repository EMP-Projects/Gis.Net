using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "outerBoundaryIs")]
public class OuterBoundaryIs
{
    [XmlElement(ElementName = "LinearRing")]
    public LinearRing? LinearRing { get; set; }
}