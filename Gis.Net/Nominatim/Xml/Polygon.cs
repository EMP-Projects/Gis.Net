using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents a Polygon object in XML format.
/// </summary>
[XmlRoot(ElementName = "Polygon")]
public class Polygon
{
    /// <summary>
    /// Represents the outer boundary of a polygon geometry in XML format.
    /// </summary>
    [XmlElement(ElementName = "outerBoundaryIs")]
    public OuterBoundaryIs? OuterBoundaryIs { get; set; }
}