using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "Polygon")]
public class Polygon
{
    [XmlElement(ElementName = "outerBoundaryIs")]
    public OuterBoundaryIs? OuterBoundaryIs { get; set; }
}