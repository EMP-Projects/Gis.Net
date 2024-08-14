using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

[XmlRoot(ElementName = "addressparts")]
public class AddressParts
{
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
}