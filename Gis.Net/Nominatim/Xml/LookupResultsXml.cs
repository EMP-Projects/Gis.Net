using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents the XML response received from Nominatim lookup.
/// </summary>
[XmlRoot(ElementName = "lookupresults")]
public class LookupResultsXml
{
    /// <summary>
    /// Represents a place.
    /// </summary>
    [XmlElement(ElementName = "place")]
    public List<Place>? Place { get; set; }

    /// <summary>
    /// Represents the timestamp attribute of the LookupResultsXml class.
    /// </summary>
    /// <remarks>
    /// The Timestamp attribute represents the timestamp of the lookup results.
    /// </remarks>
    [XmlAttribute(AttributeName = "timestamp")]
    public string? Timestamp { get; set; }

    /// <summary>
    /// Represents the attribution information of a lookup result.
    /// </summary>
    [XmlAttribute(AttributeName = "attribution")]
    public string? Attribution { get; set; }

    /// <summary>
    /// Represents the query string used in the lookup request.
    /// </summary>
    [XmlAttribute(AttributeName = "querystring")]
    public string? Querystring { get; set; }

    /// <summary>
    /// Represents the MoreUrl property of the LookupResultsXml class.
    /// </summary>
    /// <remarks>
    /// The MoreUrl property is a string that represents the URL to retrieve more results for a query.
    /// It is used in the LookupResultsXml class to provide additional information for the query results.
    /// </remarks>
    [XmlAttribute(AttributeName = "more_url")]
    public string? MoreUrl { get; set; }
}