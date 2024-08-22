using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents the search results returned from Nominatim lookup.
/// </summary>
[XmlRoot(ElementName = "searchresults")]
public abstract class SearchResultsXml
{
    /// <summary>
    /// Represents a place returned from Nominatim lookup.
    /// </summary>
    [XmlElement(ElementName = "place")]
    public Place? Place { get; set; }

    /// <summary>
    /// Represents a timestamp property of a search result from Nominatim lookup.
    /// </summary>
    [XmlAttribute(AttributeName = "timestamp")]
    public string? Timestamp { get; set; }

    /// <summary>
    /// Represents the query string used in a search request.
    /// </summary>
    [XmlAttribute(AttributeName = "querystring")]
    public string? Querystring { get; set; }

    /// <summary>
    /// Represents a polygon element in the XML response from a Nominatim search.
    /// </summary>
    [XmlAttribute(AttributeName = "polygon")]
    public string? Polygon { get; set; }
}