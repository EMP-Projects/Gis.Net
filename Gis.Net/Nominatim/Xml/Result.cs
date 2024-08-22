using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents the result of a reverse geocoding operation.
/// </summary>
[XmlRoot(ElementName = "result")]
public class Result
{
    /// <summary>
    /// Represents the unique identifier of a place.
    /// </summary>
    [XmlAttribute(AttributeName = "place_id")]
    public string? PlaceId { get; set; }

    /// <summary>
    /// Gets or sets the OpenStreetMap (OSM) type of the result.
    /// </summary>
    /// <remarks>
    /// The OsmType property represents the type of the result in the OpenStreetMap data model.
    /// It can be one of the following values:
    /// - node: Represents a specific point in space defined by its latitude and longitude coordinates.
    /// - way: Represents a linear feature defined by a list of node references.
    /// - relation: Represents a collection of related objects defined by a list of member references.
    /// - area: Represents an enclosed area defined by a list of node references.
    /// - changeset: Represents a container object that holds information about a set of changes made to the OSM database.
    /// </remarks>
    [XmlAttribute(AttributeName = "osm_type")]
    public string? OsmType { get; set; }

    /// <summary>
    /// Represents the OpenStreetMap ID (OsmId) of a result in the Nominatim XML response.
    /// </summary>
    [XmlAttribute(AttributeName = "osm_id")]
    public string? OsmId { get; set; }

    /// <summary>
    /// Gets or sets the text content of the result.
    /// </summary>
    [XmlText]
    public string? Text { get; set; }
}