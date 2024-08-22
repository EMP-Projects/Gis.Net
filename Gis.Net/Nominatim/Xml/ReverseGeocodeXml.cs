using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents a class for deserializing the response from a reverse geocoding operation using Nominatim API.
/// </summary>
[XmlRoot(ElementName = "reversegeocode")]
public class ReverseGeocodeXml
{
    /// <summary>
    /// Represents the result of a reverse geocoding operation.
    /// </summary>
    [XmlElement(ElementName = "result")]
    public Result? Result { get; set; }

    /// <summary>
    /// Represents the parts of an address.
    /// </summary>
    [XmlElement(ElementName = "addressparts")]
    public AddressParts? AddressParts { get; set; }

    /// <summary>
    /// Represents a timestamp associated with a reverse geocode result.
    /// </summary>
    [XmlAttribute(AttributeName = "timestamp")]
    public string? Timestamp { get; set; }

    /// <summary>
    /// The query string used in a reverse geocoding operation.
    /// </summary>
    [XmlAttribute(AttributeName = "querystring")]
    public string? Querystring { get; set; }
}