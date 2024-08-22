using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents the outer boundary of a polygon geometry in XML format.
/// </summary>
[XmlRoot(ElementName = "outerBoundaryIs")]
public class OuterBoundaryIs
{
    /// <summary>
    /// Represents a linear ring geometry in XML format.
    /// </summary>
    [XmlElement(ElementName = "LinearRing")]
    public LinearRing? LinearRing { get; set; }
}