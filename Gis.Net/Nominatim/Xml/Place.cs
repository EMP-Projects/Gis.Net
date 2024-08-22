using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents a place returned from Nominatim lookup.
/// </summary>
[XmlRoot(ElementName = "place")]
public class Place
{

    /// <summary>
    /// Represents a GeoKml object.
    /// </summary>
    [XmlElement(ElementName = "geokml")]
    public GeoKml? GeoKml { get; set; }
    
    [XmlElement(ElementName = "house_number")]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Represents a road in a geographic location.
    /// </summary>
    [XmlElement(ElementName = "road")]
    public string? Road { get; set; }

    /// <summary>
    /// Represents a village in a geographic location.
    /// </summary>
    [XmlElement(ElementName = "village")]
    public string? Village { get; set; }

    /// <summary>
    /// Represents a town in the context of a place.
    /// </summary>
    [XmlElement(ElementName = "town")]
    public string? Town { get; set; }

    /// <summary>
    /// Represents a place.
    /// </summary>
    [XmlElement(ElementName = "city")]
    public string? City { get; set; }

    /// <summary>
    /// Represents a place with detailed location information.
    /// </summary>
    [XmlElement(ElementName = "county")]
    public string? County { get; set; }

    /// <summary>
    /// Represents a postcode.
    /// </summary>
    [XmlElement(ElementName = "postcode")]
    public string? Postcode { get; set; }

    /// <summary>
    /// Represents a country.
    /// </summary>
    [XmlElement(ElementName = "country")]
    public string? Country { get; set; }

    /// <summary>
    /// Represents the country code of a place.
    /// </summary>
    [XmlElement(ElementName = "country_code")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Represents a place in Nominatim XML response.
    /// </summary>
    [XmlAttribute(AttributeName = "place_id")]
    public string? PlaceId { get; set; }

    /// <summary>
    /// Represents the OSM (OpenStreetMap) type of a place in Nominatim XML response.
    /// </summary>
    [XmlAttribute(AttributeName = "osm_type")]
    public string? OsmType { get; set; }

    /// <summary>
    /// Represents the OpenStreetMap ID of a place.
    /// </summary>
    [XmlAttribute(AttributeName = "osm_id")]
    public string? OsmId { get; set; }

    /// <summary>
    /// Represents the bounding box of a place.
    /// </summary>
    [XmlAttribute(AttributeName = "boundingbox")]
    public string? BoundingBox { get; set; }

    /// <summary>
    /// Represents the latitude property of a place.
    /// </summary>
    /// <remarks>
    /// The latitude of a place is the geographical coordinate that specifies the north-south position of the place on the Earth's surface.
    /// </remarks>
    [XmlAttribute(AttributeName = "lat")]
    public string? Lat { get; set; }

    /// <summary>
    /// Represents a property with the longitude value.
    /// </summary>
    [XmlAttribute(AttributeName = "lon")]
    public string? Lon { get; set; }
    
    [XmlAttribute(AttributeName = "display_name")]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Represents a place object.
    /// </summary>
    [XmlAttribute(AttributeName = "class")]
    public string? Class { get; set; }

    /// <summary>
    /// Represents a Place object.
    /// </summary>
    [XmlAttribute(AttributeName = "type")]
    public string? Type { get; set; }

    /// <summary>
    /// Represents a state district within a place.
    /// </summary>
    [XmlElement(ElementName = "state_district")]
    public string? StateDistrict { get; set; }

    /// <summary>
    /// Represents a state.
    /// </summary>
    [XmlElement(ElementName = "state")]
    public string? State { get; set; }

    /// <summary>
    /// Represents the ranking of a place in Nominatim.
    /// </summary>
    [XmlAttribute(AttributeName = "place_rank")]
    public string? PlaceRank { get; set; }

    /// <summary>
    /// Represents the address rank of a place.
    /// </summary>
    [XmlAttribute(AttributeName = "address_rank")]
    public string? AddressRank { get; set; }

    /// <summary>
    /// Represents the importance of a place.
    /// </summary>
    [XmlAttribute(AttributeName = "importance")]
    public string? Importance { get; set; }

    /// <summary>
    /// Represents a tourism property in a specific place.
    /// </summary>
    [XmlElement(ElementName = "tourism")]
    public string? Tourism { get; set; }

    /// <summary>
    /// Represents a suburb within a place.
    /// </summary>
    [XmlElement(ElementName = "suburb")]
    public string? Suburb { get; set; }
}