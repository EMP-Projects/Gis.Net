using System.Xml.Serialization;

namespace Gis.Net.Nominatim.Xml;

/// <summary>
/// Represents the parts of an address.
/// </summary>
[XmlRoot(ElementName = "addressparts")]
public class AddressParts
{
    /// <summary>
    /// Represents the house number in an address.
    /// </summary>
    [XmlElement(ElementName = "house_number")]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Represents a road in the address.
    /// </summary>
    [XmlElement(ElementName = "road")]
    public string? Road { get; set; }

    /// <summary>
    /// Represents a village in the address parts of a location.
    /// </summary>
    [XmlElement(ElementName = "village")]
    public string? Village { get; set; }

    /// <summary>
    /// Represents the town property of an address.
    /// </summary>
    [XmlElement(ElementName = "town")]
    public string? Town { get; set; }

    /// <summary>
    /// Represents a city in the address.
    /// </summary>
    [XmlElement(ElementName = "city")]
    public string? City { get; set; }

    /// <summary>
    /// Represents the county information of an address.
    /// </summary>
    [XmlElement(ElementName = "county")]
    public string? County { get; set; }

    /// <summary>
    /// Represents the postcode of an address.
    /// </summary>
    [XmlElement(ElementName = "postcode")]
    public string? Postcode { get; set; }

    /// <summary>
    /// Represents a country.
    /// </summary>
    [XmlElement(ElementName = "country")]
    public string? Country { get; set; }

    /// <summary>
    /// Represents the country code of an address.
    /// </summary>
    [XmlElement(ElementName = "country_code")]
    public string? CountryCode { get; set; }
}