using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a road from the OpenStreetMap data stored in the 'planet_osm_roads' table.
/// </summary>
[Keyless]
[Table("planet_osm_roads")]
[Index("OsmId", Name = "planet_osm_roads_osm_id_idx")]
public partial class PlanetOsmRoads : IOsmPgGeometryModel
{
    /// <summary>
    /// Represents the OsmId property of the PlanetOsmRoads class.
    /// </summary>
    [Column("osm_id")]
    public long? OsmId { get; set; }

    /// <summary>
    /// Gets or sets the access property of the <see cref="PlanetOsmRoads"/> class.
    /// </summary>
    /// <value>
    /// The access property.
    /// </value>
    [Column("access")]
    public string? Access { get; set; }

    /// <summary>
    /// Gets or sets the house name associated with the address of the road.
    /// </summary>
    [Column("addr:housename")]
    public string? AddrHousename { get; set; }

    /// <summary>
    /// Represents the house number of an address.
    /// </summary>
    [Column("addr:housenumber")]
    public string? AddrHousenumber { get; set; }

    /// <summary>
    /// Gets or sets the value of the "addr:interpolation" property of the "planet_osm_roads" table.
    /// </summary>
    /// <remarks>
    /// The "addr:interpolation" property is used to specify how a range of house numbers on a street is interpolated.
    /// This property is typically used when there is a gap in the house numbers on a street and it is necessary to infer
    /// the missing house numbers based on the known house numbers at either end of the gap.
    /// The value of the "addr:interpolation" property can be one of the following:
    /// - "all" indicates that all house numbers within the range should be listed individually.
    /// - "even" indicates that only even house numbers within the range should be listed.
    /// - "odd" indicates that only odd house numbers within the range should be listed.
    /// - "alphabetic" indicates that the house numbers within the range should be represented using alphabetic characters.
    /// </remarks>
    [Column("addr:interpolation")]
    public string? AddrInterpolation { get; set; }

    /// <summary>
    /// Represents the administrative level of a road.
    /// </summary>
    [Column("admin_level")]
    public string? AdminLevel { get; set; }

    /// <summary>
    /// Represents the aerialway attribute of the PlanetOsmRoads class.
    /// </summary>
    [Column("aerialway")]
    public string? Aerialway { get; set; }

    /// <summary>
    /// Represents a property that specifies the type of aeroway.
    /// </summary>
    [Column("aeroway")]
    public string? Aeroway { get; set; }

    /// <summary>
    /// Represents an amenity property.
    /// </summary>
    [Column("amenity")]
    public string? Amenity { get; set; }

    /// <summary>
    /// Represents the area of a certain object or feature.
    /// </summary>
    [Column("area")]
    public string? Area { get; set; }

    /// <summary>
    /// Represents a barrier on a road.
    /// </summary>
    [Column("barrier")]
    public string? Barrier { get; set; }

    /// <summary>
    /// Represents a Bicycle.
    /// </summary>
    [Column("bicycle")]
    public string? Bicycle { get; set; }

    /// <summary>
    /// Represents the brand of a road or building.
    /// </summary>
    [Column("brand")]
    public string? Brand { get; set; }

    /// <summary>
    /// Represents a bridge property of a road segment.
    /// </summary>
    [Column("bridge")]
    public string? Bridge { get; set; }

    /// <summary>
    /// Represents the boundary of the road or area.
    /// </summary>
    [Column("boundary")]
    public string? Boundary { get; set; }

    /// <summary>
    /// Represents a building in the Planet OSM Roads data.
    /// </summary>
    [Column("building")]
    public string? Building { get; set; }

    /// <summary>
    /// Represents the construction information of a road in the Planet OSM database.
    /// </summary>
    [Column("construction")]
    public string? Construction { get; set; }

    /// <summary>
    /// Gets or sets the information about whether a road is covered or not.
    /// </summary>
    /// <remarks>
    /// This property represents the attribute "covered" of the table "planet_osm_roads" in the OSM data model.
    /// It indicates whether the road is covered.
    /// </remarks>
    [Column("covered")]
    public string? Covered { get; set; }

    /// <summary>
    /// Represents a culvert in the Planet OSM Roads data.
    /// </summary>
    [Column("culvert")]
    public string? Culvert { get; set; }

    /// <summary>
    /// Represents the cutting property of the <see cref="PlanetOsmRoads"/> class.
    /// </summary>
    [Column("cutting")]
    public string? Cutting { get; set; }

    /// <summary>
    /// Represents the denomination of a road.
    /// </summary>
    [Column("denomination")]
    public string? Denomination { get; set; }

    /// <summary>
    /// Represents the "disused" property of the PlanetOsmRoads class.
    /// </summary>
    [Column("disused")]
    public string? Disused { get; set; }

    /// <summary>
    /// Represents information about an embankment.
    /// </summary>
    [Column("embankment")]
    public string? Embankment { get; set; }

    [Column("foot")]
    public string? Foot { get; set; }

    /// <summary>
    /// Represents the GeneratorSource property of the PlanetOsmRoads class.
    /// </summary>
    [Column("generator:source")]
    public string? GeneratorSource { get; set; }

    /// <summary>
    /// Represents a harbour in the GIS system.
    /// </summary>
    [Column("harbour")]
    public string? Harbour { get; set; }

    /// <summary>
    /// Represents a highway in the OSM database.
    /// </summary>
    [Column("highway")]
    public string? Highway { get; set; }

    /// <summary>
    /// A class representing the historic property of the PlanetOsmRoads model.
    /// </summary>
    [Column("historic")]
    public string? Historic { get; set; }

    /// <summary>
    /// Represents a Horse.
    /// </summary>
    [Column("horse")]
    public string? Horse { get; set; }

    /// <summary>
    /// Represents the "intermittent" property of the PlanetOsmRoads class.
    /// </summary>
    [Column("intermittent")]
    public string? Intermittent { get; set; }

    /// <summary>
    /// Represents a junction on a road.
    /// </summary>
    [Column("junction")]
    public string? Junction { get; set; }

    /// <summary>
    /// Represents the land use of a geographic area.
    /// </summary>
    [Column("landuse")]
    public string? Landuse { get; set; }

    /// <summary>
    /// Represents a layer property of the PlanetOsmRoads class.
    /// </summary>
    [Column("layer")]
    public string? Layer { get; set; }

    /// Represents the Leisure property of the PlanetOsmRoads class.
    /// This property represents the types of leisure facilities or amenities associated with a road.
    /// /
    [Column("leisure")]
    public string? Leisure { get; set; }

    /// <summary>
    /// Represents the properties of a lock.
    /// </summary>
    [Column("lock")]
    public string? Lock { get; set; }

    /// <summary>
    /// Represents the 'man_made' property of the PlanetOsmRoads class.
    /// </summary>
    [Column("man_made")]
    public string? ManMade { get; set; }

    /// <summary>
    /// Represents a military property.
    /// </summary>
    [Column("military")]
    public string? Military { get; set; }

    /// <summary>
    /// Represents a motorcar.
    /// </summary>
    [Column("motorcar")]
    public string? Motorcar { get; set; }

    /// <summary>
    /// Gets or sets the name of the road.
    /// </summary>
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents a property of the <see cref="PlanetOsmRoads"/> class that describes the type of natural feature.
    /// </summary>
    [Column("natural")]
    public string? Natural { get; set; }

    /// <summary>
    /// Represents the property information for an office.
    /// </summary>
    [Column("office")]
    public string? Office { get; set; }

    /// <summary>
    /// Represents the 'oneway' property of a road.
    /// </summary>
    [Column("oneway")]
    public string? Oneway { get; set; }

    /// <summary>
    /// Represents an operator for a road in the OpenStreetMap database.
    /// </summary>
    /// <remarks>
    /// This class represents the "operator" property for a road in the OpenStreetMap database.
    /// The "operator" property specifies the operator or organization that manages or operates the road.
    /// </remarks>
    [Column("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Represents a place.
    /// </summary>
    [Column("place")]
    public string? Place { get; set; }

    /// <summary>
    /// Represents a property of a <see cref="PlanetOsmRoads"/> object that holds information about population.
    /// </summary>
    [Column("population")]
    public string? Population { get; set; }

    /// <summary>
    /// Represents the power attribute of a road in the PlanetOsmRoads class.
    /// </summary>
    [Column("power")]
    public string? Power { get; set; }

    /// <summary>
    /// Represents the power source of a road.
    /// </summary>
    [Column("power_source")]
    public string? PowerSource { get; set; }

    /// <summary>
    /// Represents a public transport entity in the OsmPg database.
    /// </summary>
    [Column("public_transport")]
    public string? PublicTransport { get; set; }

    /// <summary>
    /// Represents a railway in the PlanetOsmRoads database table.
    /// </summary>
    [Column("railway")]
    public string? Railway { get; set; }

    [Column("ref")]
    public string? Ref { get; set; }

    /// <summary>
    /// Represents the religion property of the PlanetOsmRoads class.
    /// </summary>
    [Column("religion")]
    public string? Religion { get; set; }

    [Column("route")]
    public string? Route { get; set; }

    /// <summary>
    /// Represents a property for the service tag in the "planet_osm_roads" table.
    /// </summary>
    [Column("service")]
    public string? Service { get; set; }

    /// <summary>
    /// Represents a property shop.
    /// </summary>
    [Column("shop")]
    public string? Shop { get; set; }

    /// <summary>
    /// Represents a sport activity or facility.
    /// </summary>
    [Column("sport")]
    public string? Sport { get; set; }

    /// <summary>
    /// Represents the surface type of a road or area in the OpenStreetMap data.
    /// </summary>
    [Column("surface")]
    public string? Surface { get; set; }

    /// <summary>
    /// Represents the toll property of a road.
    /// </summary>
    [Column("toll")]
    public string? Toll { get; set; }

    /// <summary>
    /// Represents a property related to tourism.
    /// </summary>
    [Column("tourism")]
    public string? Tourism { get; set; }

    /// <summary>
    /// Represents the type of a tower.
    /// </summary>
    [Column("tower:type")]
    public string? TowerType { get; set; }

    /// <summary>
    /// Represents the track type of a road.
    /// </summary>
    [Column("tracktype")]
    public string? Tracktype { get; set; }

    /// <summary>
    /// Represents a tunnel property.
    /// </summary>
    [Column("tunnel")]
    public string? Tunnel { get; set; }

    /// <summary>
    /// Represents the Water property of the PlanetOsmRoads class.
    /// </summary>
    [Column("water")]
    public string? Water { get; set; }

    /// <summary>
    /// Represents a waterway feature in the OSM data.
    /// </summary>
    [Column("waterway")]
    public string? Waterway { get; set; }

    /// <summary>
    /// Represents a wetland feature.
    /// </summary>
    [Column("wetland")]
    public string? Wetland { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [Column("width")]
    public string? Width { get; set; }

    /// <summary>
    /// Represents the property "Wood" for the PlanetOsmRoads class.
    /// </summary>
    /// <remarks>
    /// This property represents the type of wood that is associated with a road in the planet_osm_roads table.
    /// </remarks>
    [Column("wood")]
    public string? Wood { get; set; }

    /// <summary>
    /// The order in which the road feature is rendered.
    /// </summary>
    [Column("z_order")]
    public int? ZOrder { get; set; }

    /// <summary>
    /// Represents a property that stores the area of a way in the OSM database.
    /// </summary>
    [Column("way_area")]
    public float? WayArea { get; set; }

    /// <summary>
    /// Represents a property in the <see cref="PlanetOsmRoads"/> class that stores the Wikidata value.
    /// </summary>
    [Column("wikidata")]
    public string? Wikidata { get; set; }

    /// <summary>
    /// Represents the NameEtymologyWikidata property of the PlanetOsmRoads class.
    /// </summary>
    [Column("name:etymology:wikidata")]
    public string? NameEtymologyWikidata { get; set; }

    /// <summary>
    /// Represents a road in planet_osm_roads table.
    /// </summary>
    [Column("wikipedia")]
    public string? Wikipedia { get; set; }

    /// <summary>
    /// Represents the model for the <c>planet_osm_roads</c> table in the database.
    /// </summary>
    [Column("name:etymology:wikipedia")]
    public string? NameEtymologyWikipedia { get; set; }

    /// <summary>
    /// Represents the timestamp of an OpenStreetMap object.
    /// </summary>
    [Column("osm_timestamp")]
    public string? OsmTimestamp { get; set; }

    /// <summary>
    /// Represents a class that maps to the "planet_osm_roads" table in the database and contains various tags.
    /// </summary>
    /// <remarks>
    /// This class is used to store information about roads in the OpenStreetMap database.
    /// It implements the <see cref="Gis.Net.Osm.OsmPg.Models.IOsmPgGeometryModel"/> interface.
    /// </remarks>
    [Column("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Represents a road in the OSM (OpenStreetMap) database.
    /// </summary>
    [Column("way", TypeName = "geometry(LineString,3857)")]
    public Geometry? Way { get; set; }
}
