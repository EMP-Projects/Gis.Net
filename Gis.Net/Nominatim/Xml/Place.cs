using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "place")]
public class Place
{

    [XmlElement(ElementName = "geokml")]
    public GeoKml? GeoKml { get; set; }
    
    [XmlElement(ElementName = "house_number")]
    public string? HouseNumber { get; set; }

    [XmlElement(ElementName = "road")]
    public string? Road { get; set; }
    
    [XmlElement(ElementName = "village")]
    public string? Village { get; set; }
    
    [XmlElement(ElementName = "town")]
    public string? Town { get; set; }
    
    [XmlElement(ElementName = "city")]
    public string? City { get; set; }

    [XmlElement(ElementName = "county")]
    public string? County { get; set; }

    [XmlElement(ElementName = "postcode")]
    public string? Postcode { get; set; }
    
    [XmlElement(ElementName = "country")]
    public string? Country { get; set; }
    
    [XmlElement(ElementName = "country_code")]
    public string? CountryCode { get; set; }

    [XmlAttribute(AttributeName = "place_id")]
    public string? PlaceId { get; set; }
    
    [XmlAttribute(AttributeName = "osm_type")]
    public string? OsmType { get; set; }
    
    [XmlAttribute(AttributeName = "osm_id")]
    public string? OsmId { get; set; }
    
    [XmlAttribute(AttributeName = "boundingbox")]
    public string? BoundingBox { get; set; }
    
    [XmlAttribute(AttributeName = "lat")]
    public string? Lat { get; set; }
    
    [XmlAttribute(AttributeName = "lon")]
    public string? Lon { get; set; }
    
    [XmlAttribute(AttributeName = "display_name")]
    public string? DisplayName { get; set; }
    
    [XmlAttribute(AttributeName = "class")]
    public string? Class { get; set; }
    
    [XmlAttribute(AttributeName = "type")]
    public string? Type { get; set; }
    
    [XmlElement(ElementName = "state_district")]
    public string? StateDistrict { get; set; }
    
    [XmlElement(ElementName = "state")]
    public string? State { get; set; }
    
    [XmlAttribute(AttributeName = "place_rank")]
    public string? PlaceRank { get; set; }
    
    [XmlAttribute(AttributeName = "address_rank")]
    public string? AddressRank { get; set; }
    
    [XmlAttribute(AttributeName = "importance")]
    public string? Importance { get; set; }
    
    [XmlElement(ElementName = "tourism")]
    public string? Tourism { get; set; }
    
    [XmlElement(ElementName = "suburb")]
    public string? Suburb { get; set; }
}