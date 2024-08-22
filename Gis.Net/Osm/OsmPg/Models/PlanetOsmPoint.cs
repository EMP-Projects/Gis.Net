using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a point on the planet obtained from OpenStreetMap data.
/// </summary>
[Keyless]
[Table("planet_osm_point")]
[Index("OsmId", Name = "planet_osm_point_osm_id_idx")]
public partial class PlanetOsmPoint : IOsmPgGeometryModel
{
    /// <summary>
    /// Represents the OsmId property of the <see cref="PlanetOsmPoint"/> class.
    /// </summary>
    /// <remarks>
    /// The OsmId property is used to uniquely identify a point in the OSM database.
    /// It is a 64-bit integer value.
    /// </remarks>
    [Column("osm_id")]
    public long? OsmId { get; set; }

    /// <summary>
    /// Represents the access property of a PlanetOsmPoint object.
    /// </summary>
    [Column("access")]
    public string? Access { get; set; }

    /// <summary>
    /// Gets or sets the house name of the address.
    /// </summary>
    [Column("addr:housename")]
    public string? AddrHousename { get; set; }

    /// <summary>
    /// Represents the house number of an address.
    /// </summary>
    [Column("addr:housenumber")]
    public string? AddrHousenumber { get; set; }

    /// <summary>
    /// Represents the address interpolation property of a <see cref="PlanetOsmPoint"/> object.
    /// </summary>
    [Column("addr:interpolation")]
    public string? AddrInterpolation { get; set; }

    /// <summary>
    /// Represents the administrative level of a geographic location.
    /// </summary>
    [Column("admin_level")]
    public string? AdminLevel { get; set; }

    /// <summary>
    /// Represents the aerialway property of a PlanetOsmPoint object.
    /// </summary>
    [Column("aerialway")]
    public string? Aerialway { get; set; }

    /// <summary>
    /// The Aeroway property represents the type of aeroway (aviation-related features) for a given planet_osm_point.
    /// </summary>
    [Column("aeroway")]
    public string? Aeroway { get; set; }

    /// <summary>
    /// Represents an amenity provided by a geographical location.
    /// </summary>
    [Column("amenity")]
    public string? Amenity { get; set; }

    /// <summary>
    /// Represents the area of a point in the <see cref="PlanetOsmPoint"/> class.
    /// </summary>
    [Column("area")]
    public string? Area { get; set; }

    /// <summary>
    /// Represents the barrier property of a PlanetOsmPoint.
    /// </summary>
    [Column("barrier")]
    public string? Barrier { get; set; }

    /// <summary>
    /// Represents a bicycle property of a point on a map.
    /// </summary>
    [Column("bicycle")]
    public string? Bicycle { get; set; }

    /// <summary>
    /// Represents the brand of a point of interest.
    /// </summary>
    [Column("brand")]
    public string? Brand { get; set; }

    /// <summary>
    /// Represents the bridge property of the <see cref="PlanetOsmPoint"/> class.
    /// </summary>
    /// <remarks>
    /// This property specifies if the point is located on a bridge.
    /// </remarks>
    [Column("bridge")]
    public string? Bridge { get; set; }

    /// <summary>
    /// Represents a boundary property of the PlanetOsmPoint class.
    /// </summary>
    /// <value>
    /// The boundary value.
    /// </value>
    [Column("boundary")]
    public string? Boundary { get; set; }

    /// <summary>
    /// Represents a building in the GIS system.
    /// </summary>
    [Column("building")]
    public string? Building { get; set; }

    /// <summary>
    /// The capital property represents the capital of a geographic location.
    /// </summary>
    /// <remarks>
    /// This property is used in the <see cref="PlanetOsmPoint"/> class to store the capital information of a point in the planet_osm_point table.
    /// </remarks>
    /// <value>
    /// A string representing the capital of the geographic location.
    /// </value>
    [Column("capital")]
    public string? Capital { get; set; }

    /// <summary>
    /// Represents a property related to construction in the PlanetOsmPoint.
    /// </summary>
    [Column("construction")]
    public string? Construction { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the entity is covered.
    /// </summary>
    /// <remarks>
    /// This property represents the "covered" attribute of the <see cref="PlanetOsmPoint"/> entity in the database.
    /// It indicates whether the entity is covered or not.
    /// </remarks>
    [Column("covered")]
    public string? Covered { get; set; }

    /// <summary>
    /// Represents a culvert in the PlanetOsmPoint table.
    /// </summary>
    [Column("culvert")]
    public string? Culvert { get; set; }

    /// <summary>
    /// Represents the cutting property of a <see cref="PlanetOsmPoint"/> object.
    /// </summary>
    /// <remarks>
    /// The cutting property is used to specify if a point on the planet's surface is a cutting or trench.
    /// The cutting property can have the following values:
    /// - <c>null</c>: The cutting property is not specified.
    /// Any other value indicates that the point is a cutting or trench, but the specific value depends on the data source.
    /// </remarks>
    [Column("cutting")]
    public string? Cutting { get; set; }

    /// <summary>
    /// Represents the denomination of a location or place.
    /// </summary>
    [Column("denomination")]
    public string? Denomination { get; set; }

    /// <summary>
    /// Represents the "disused" property of the PlanetOsmPoint class.
    /// </summary>
    [Column("disused")]
    public string? Disused { get; set; }

    /// <summary>
    /// Represents the Ele property of the PlanetOsmPoint class.
    /// </summary>
    /// <seealso cref="Gis.Net.Osm.OsmPg.Models.PlanetOsmPoint"/>
    [Column("ele")]
    public string? Ele { get; set; }

    /// <summary>
    /// Represents the embankment property of a <see cref="PlanetOsmPoint"/> object.
    /// </summary>
    [Column("embankment")]
    public string? Embankment { get; set; }

    /// <summary>
    /// Represents the "foot" property of a PlanetOsmPoint object.
    /// </summary>
    [Column("foot")]
    public string? Foot { get; set; }

    /// <summary>
    /// Represents an OSM point on the planet.
    /// </summary>
    [Column("generator:source")]
    public string? GeneratorSource { get; set; }

    /// <summary>
    /// Represents a harbour.
    /// </summary>
    [Column("harbour")]
    public string? Harbour { get; set; }

    /// <summary>
    /// Represents a highway in the PlanetOsmPoint model.
    /// </summary>
    [Column("highway")]
    public string? Highway { get; set; }

    /// <summary>
    /// Represents the historic information associated with a planet_osm_point entity.
    /// </summary>
    [Column("historic")]
    public string? Historic { get; set; }

    /// <summary>
    /// Represents a horse in the system.
    /// </summary>
    [Column("horse")]
    public string? Horse { get; set; }

    /// <summary>
    /// Represents the intermittent value of a PlanetOsmPoint.
    /// </summary>
    [Column("intermittent")]
    public string? Intermittent { get; set; }

    /// The `Junction` property represents the type of junction for a given location in the `PlanetOsmPoint` class.
    /// It is a string value that describes the type of junction, such as "roundabout", "intersection", "traffic_signals", etc.
    /// This property is defined in the `PlanetOsmPoint` class from the `Gis.Net.Osm.OsmPg.Models` namespace.
    /// The `PlanetOsmPoint` class is a partial class that implements the `IOsmPgGeometryModel` interface.
    /// It is mapped to the "planet_osm_point" table in the database and represents a point feature in the OpenStreetMap data.
    /// Note: This documentation is generated from the actual code and should be used as a reference for the `Junction` property.
    /// The example code has been omitted for brevity.
    /// Example Usage:
    /// --------------
    /// PlanetOsmPoint point = new PlanetOsmPoint();
    /// point.Junction = "roundabout";
    /// /
    [Column("junction")]
    public string? Junction { get; set; }

    /// <summary>
    /// Represents the land use information of a point in a map.
    /// </summary>
    [Column("landuse")]
    public string? Landuse { get; set; }

    /// <summary>
    /// Represents a layer in the PlanetOsmPoint entity.
    /// </summary>
    [Column("layer")]
    public string? Layer { get; set; }

    /// <summary>
    /// Represents the leisure property of the PlanetOsmPoint class.
    /// </summary>
    /// <remarks>
    /// This property represents the type of leisure facility associated with a geographical point.
    /// It can be used to determine the type of leisure activity or amenity available at the location.
    /// Examples of leisure types include parks, playgrounds, sports facilities, and recreational areas.
    /// </remarks>
    [Column("leisure")]
    public string? Leisure { get; set; }

    /// <summary>
    /// Represents the lock property of a PlanetOsmPoint.
    /// </summary>
    [Column("lock")]
    public string? Lock { get; set; }

    /// Represents a man-made feature in the OSM database.
    /// This class is a partial implementation of the `PlanetOsmPoint` model from the `Gis.Net.Osm.OsmPg.Models` namespace.
    /// It includes properties that represent various attributes associated with man-made features in the OSM database.
    /// /
    [Column("man_made")]
    public string? ManMade { get; set; }

    /// <summary>
    /// Represents a military property.
    /// </summary>
    [Column("military")]
    public string? Military { get; set; }

    /// <summary>
    /// Represents a motorcar property of a <see cref="PlanetOsmPoint"/> object.
    /// </summary>
    [Column("motorcar")]
    public string? Motorcar { get; set; }

    /// <summary>
    /// Gets or sets the name of the entry.
    /// </summary>
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents the "natural" property of a PlanetOsmPoint object.
    /// </summary>
    [Column("natural")]
    public string? Natural { get; set; }

    /// <summary>
    /// Represents the office property of a point in Planet OSM.
    /// </summary>
    [Column("office")]
    public string? Office { get; set; }

    /// <summary>
    /// Represents the property that indicates the oneway direction of a point in the planet_osm_point table.
    /// </summary>
    [Column("oneway")]
    public string? Oneway { get; set; }

    /// <summary>
    /// Represents the operator of a geographical point in the Planet OSM database.
    /// </summary>
    [Column("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Represents a place in the OpenStreetMap data.
    /// </summary>
    [Column("place")]
    public string? Place { get; set; }

    /// <summary>
    /// Represents a population property of a geographical point.
    /// </summary>
    /// <remarks>
    /// The population property provides information about the population of a specific location.
    /// It can be used to indicate the number of people living in a city, town, or village.
    /// </remarks>
    [Column("population")]
    public string? Population { get; set; }

    /// <summary>
    /// Represents the power of a PlanetOsmPoint.
    /// </summary>
    [Column("power")]
    public string? Power { get; set; }

    /// <summary>
    /// Represents a property for the power source of a planet osm point.
    /// </summary>
    [Column("power_source")]
    public string? PowerSource { get; set; }

    /// <summary>
    /// Represents a public transport entity.
    /// </summary>
    [Column("public_transport")]
    public string? PublicTransport { get; set; }

    /// <summary>
    /// Represents a railway property of a railway object.
    /// </summary>
    [Column("railway")]
    public string? Railway { get; set; }

    /// <summary>
    /// Represents a property of the PlanetOsmPoint class that contains the reference value.
    /// </summary>
    [Column("ref")]
    public string? Ref { get; set; }

    /// <summary>
    /// Represents the religion associated with a geographic point.
    /// </summary>
    [Column("religion")]
    public string? Religion { get; set; }

    /// <summary>
    /// Represents a property of the PlanetOsmPoint class that stores information about the route.
    /// </summary>
    [Column("route")]
    public string? Route { get; set; }

    /// <summary>
    /// Represents a service provided by a location or landmark.
    /// </summary>
    [Column("service")]
    public string? Service { get; set; }

    /// <summary>
    /// Represents a property shop.
    /// </summary>
    [Column("shop")]
    public string? Shop { get; set; }

    /// <summary>
    /// Represents a sport associated with a location in the OpenStreetMap database.
    /// </summary>
    [Column("sport")]
    public string? Sport { get; set; }

    /// Represents the Surface property of the PlanetOsmPoint class.
    /// The Surface property indicates the surface type of a point in the planet_osm_point table.
    /// It can be used to describe the type of surface on which the point is located, such as "asphalt", "concrete", "grass", etc.
    /// Property Type: string
    /// Example Usage:
    /// var point = new PlanetOsmPoint();
    /// point.Surface = "asphalt";
    /// Note: The actual usage and available values for the Surface property may vary based on the specific implementation and data source.
    /// /
    [Column("surface")]
    public string? Surface { get; set; }

    /// <summary>
    /// Represents the toll property of a <see cref="PlanetOsmPoint"/> object.
    /// </summary>
    [Column("toll")]
    public string? Toll { get; set; }

    /// <summary>
    /// Represents a point of interest related to tourism.
    /// </summary>
    [Column("tourism")]
    public string? Tourism { get; set; }

    /// <summary>
    /// Represents the TowerType property of the PlanetOsmPoint class.
    /// </summary>
    [Column("tower:type")]
    public string? TowerType { get; set; }

    /// <summary>
    /// Represents a tunnel in the OSM data.
    /// </summary>
    [Column("tunnel")]
    public string? Tunnel { get; set; }

    /// <summary>
    /// Represents the Water property of the PlanetOsmPoint class.
    /// </summary>
    [Column("water")]
    public string? Water { get; set; }

    /// <summary>
    /// Represents a waterway in the OpenStreetMap data.
    /// </summary>
    [Column("waterway")]
    public string? Waterway { get; set; }

    /// <summary>
    /// Represents a wetland feature in the OSM database.
    /// </summary>
    [Column("wetland")]
    public string? Wetland { get; set; }

    /// <summary>
    /// Gets or sets the width value.
    /// </summary>
    [Column("width")]
    public string? Width { get; set; }

    /// <summary>
    /// Represents a property of Wood in a PlanetOsmPoint object.
    /// </summary>
    [Column("wood")]
    public string? Wood { get; set; }

    /// <summary>
    /// Gets or sets the Z-order of the element.
    /// </summary>
    /// <remarks>
    /// The ZOrder property determines the order in which elements are rendered on the screen.
    /// Elements with a higher Z-order value are rendered on top of elements with a lower value.
    /// </remarks>
    /// <value>
    /// The Z-order value of the element.
    /// </value>
    [Column("z_order")]
    public int? ZOrder { get; set; }

    /// <summary>
    /// Represents a record in the Wikidata table.
    /// </summary>
    [Column("wikidata")]
    public string? Wikidata { get; set; }

    /// <summary>
    /// Represents the NameEtymologyWikidata property of the PlanetOsmPoint class.
    /// </summary>
    [Column("name:etymology:wikidata")]
    public string? NameEtymologyWikidata { get; set; }

    /// <summary>
    /// Represents a Wikipedia property of a planet_osm_point object.
    /// </summary>
    [Column("wikipedia")]
    public string? Wikipedia { get; set; }

    /// <summary>
    /// Represents a model class for storing information about a point in the OpenStreetMap database.
    /// </summary>
    [Column("name:etymology:wikipedia")]
    public string? NameEtymologyWikipedia { get; set; }

    /// <summary>
    /// Represents the timestamp of an OpenStreetMap element.
    /// </summary>
    [Column("osm_timestamp")]
    public string? OsmTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the object.
    /// </summary>
    /// <remarks>
    /// The tags are represented as a dictionary, where the key is the tag key and the value is the tag value.
    /// </remarks>
    [Column("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Represents the geometric way associated with a point in OpenStreetMap data.
    /// </summary>
    [Column("way", TypeName = "geometry(Point,3857)")]
    public Geometry? Way { get; set; }
}
