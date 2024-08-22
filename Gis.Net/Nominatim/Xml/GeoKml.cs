using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents a GeoKml object.
/// </summary>
[XmlRoot(ElementName = "geokml")]
public class GeoKml
{
    /// Represents a polygon object in the GeoKml XML data.
    /// /
    [XmlElement(ElementName = "Polygon")]
    public Polygon? Polygon { get; set; }
}