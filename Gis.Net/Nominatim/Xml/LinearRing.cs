using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents a linear ring geometry in XML format.
/// </summary>
[XmlRoot(ElementName = "LinearRing")]
public class LinearRing
{
    /// <summary>
    /// Represents a set of coordinates for a geographic location.
    /// </summary>
    [XmlElement(ElementName = "coordinates")]
    public string? Coordinates { get; set; }
}