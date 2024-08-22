using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a line feature in the Planet OSM data.
/// </summary>
[Keyless]
[Table("planet_osm_line")]
[Index("OsmId", Name = "planet_osm_line_osm_id_idx")]
public partial class PlanetOsmLine : IOsmPgGeometryModel
{
    [Column("osm_id")]
    public long? OsmId { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the instance of <see cref="PlanetOsmLine"/> class.
    /// </summary>
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents the tags associated with the <see cref="PlanetOsmLine"/> class.
    /// </summary>
    [Column("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the access property.
    /// </summary>
    [Column("access")]
    public string? Access { get; set; }

    /// <summary>
    /// The name of the house associated with the address.
    /// </summary>
    [Column("addr:housename")]
    public string? AddrHousename { get; set; }

    /// <summary>
    /// Represents the house number of an address.
    /// </summary>
    [Column("addr:housenumber")]
    public string? AddrHousenumber { get; set; }

    /// <summary>
    /// Represents the address interpolation property for a PlanetOsmLine object.
    /// </summary>
    [Column("addr:interpolation")]
    public string? AddrInterpolation { get; set; }

    /// <summary>
    /// Represents the administrative level of a geographical entity.
    /// </summary>
    [Column("admin_level")]
    public string? AdminLevel { get; set; }

    /// <summary>
    /// Represents an aerialway feature in the OSM data.
    /// </summary>
    [Column("aerialway")]
    public string? Aerialway { get; set; }

    /// <summary>
    /// Represents a line feature in the Planet OSM data with aeroway tag.
    /// </summary>
    [Column("aeroway")]
    public string? Aeroway { get; set; }

    /// <summary>
    /// Represents an amenity in the OpenStreetMap database.
    /// </summary>
    [Column("amenity")]
    public string? Amenity { get; set; }

    /// <summary>
    /// Represents the Area property of the PlanetOsmLine class.
    /// </summary>
    /// <remarks>
    /// The Area property represents the area in which the PlanetOsmLine object is located.
    /// It provides information about the geographic region or boundaries in which the object exists.
    /// This property is defined as a string data type.
    /// </remarks>
    [Column("area")]
    public string? Area { get; set; }

    /// *Namespace:** Gis.Net.Osm.OsmPg.Models
    [Column("barrier")]
    public string? Barrier { get; set; }

    /// <summary>
    /// Represents a bicycle object.
    /// </summary>
    [Column("bicycle")]
    public string? Bicycle { get; set; }

    /// <summary>
    /// Represents the Brand property of a PlanetOsmLine object.
    /// </summary>
    [Column("brand")]
    public string? Brand { get; set; }

    /// <summary>
    /// Represents a bridge in the PlanetOsmLine data.
    /// </summary>
    [Column("bridge")]
    public string? Bridge { get; set; }

    /// <summary>
    /// Represents the boundary property of a <see cref="PlanetOsmLine"/>.
    /// </summary>
    [Column("boundary")]
    public string? Boundary { get; set; }

    /// <summary>
    /// Represents a building in the system.
    /// </summary>
    [Column("building")]
    public string? Building { get; set; }

    /// <summary>
    /// Represents a construction property of a PlanetOsmLine object.
    /// </summary>
    [Column("construction")]
    public string? Construction { get; set; }

    /// <summary>
    /// Represents the "covered" property of a planet_osm_line object.
    /// </summary>
    [Column("covered")]
    public string? Covered { get; set; }

    /// <summary>
    /// Represents a culvert feature in the Planet OSM Line data.
    /// </summary>
    [Column("culvert")]
    public string? Culvert { get; set; }

    /// <summary>
    /// Represents the "cutting" property of the PlanetOsmLine class.
    /// </summary>
    /// <remarks>
    /// The "cutting" property defines whether a line feature represents a cutting, which is an excavation made through the earth's surface for the construction of a transportation route, such as a road or railway.
    /// </remarks>
    [Column("cutting")]
    public string? Cutting { get; set; }

    /// <summary>
    /// Represents the denomination of a line object in the Planet OSM database.
    /// </summary>
    [Column("denomination")]
    public string? Denomination { get; set; }

    /// <summary>
    /// Represents the Disused property of the PlanetOsmLine class.
    /// </summary>
    /// <remarks>
    /// This property represents the status of a line feature being disused.
    /// The value of this property indicates whether the line feature is disused or not.
    /// </remarks>
    [Column("disused")]
    public string? Disused { get; set; }

    /// <summary>
    /// Represents an embankment in the GIS database.
    /// </summary>
    [Column("embankment")]
    public string? Embankment { get; set; }

    /// <summary>
    /// Represents the "foot" attribute of the PlanetOsmLine class.
    /// </summary>
    [Column("foot")]
    public string? Foot { get; set; }

    /// <summary>
    /// Represents a generator source property for a PlanetOsmLine object.
    /// </summary>
    [Column("generator:source")]
    public string? GeneratorSource { get; set; }

    /// <summary>
    /// Represents a harbour.
    /// </summary>
    [Column("harbour")]
    public string? Harbour { get; set; }

    /// <summary>
    /// Represents a highway in the OSM data.
    /// </summary>
    [Column("highway")]
    public string? Highway { get; set; }

    /// <summary>
    /// Represents the historic property of a PlanetOsmLine object.
    /// </summary>
    [Column("historic")]
    public string? Historic { get; set; }

    /// <summary>
    /// Represents a horse.
    /// </summary>
    [Column("horse")]
    public string? Horse { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the property is intermittent.
    /// </summary>
    /// <value>
    /// The value indicating whether the property is intermittent.
    /// </value>
    /// <remarks>
    /// This property indicates whether the road or feature represented by this entity is intermittent.
    /// An intermittent road or feature is one that is not continuous, but rather has breaks or gaps in its existence.
    /// For example, it could be a road that is only passable during certain seasons or weather conditions.
    /// This property is of type string and can have various values depending on the specific data source or schema.
    /// </remarks>
    [Column("intermittent")]
    public string? Intermittent { get; set; }

    /// <summary>
    /// Represents a junction in the OpenStreetMap data.
    /// </summary>
    [Column("junction")]
    public string? Junction { get; set; }

    /// <summary>
    /// Represents the land use classification of a property.
    /// </summary>
    [Column("landuse")]
    public string? Landuse { get; set; }

    /// Represents a layer property of a PlanetOsmLine object.
    /// /
    [Column("layer")]
    public string? Layer { get; set; }

    /// <summary>
    /// Represents the leisure attribute of a PlanetOsmLine object.
    /// </summary>
    [Column("leisure")]
    public string? Leisure { get; set; }

    /// <summary>
    /// Represents a lock.
    /// </summary>
    [Column("lock")]
    public string? Lock { get; set; }

    /// <summary>
    /// Represents a man-made object.
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

    [Column("natural")]
    public string? Natural { get; set; }

    /// <summary>
    /// Represents an office.
    /// </summary>
    [Column("office")]
    public string? Office { get; set; }

    /// <summary>
    /// Represents the property that indicates the direction of travel on a line.
    /// </summary>
    [Column("oneway")]
    public string? Oneway { get; set; }

    /// <summary>
    /// Represents the operator of a PlanetOsmLine object.
    /// </summary>
    [Column("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Represents a place in the PlanetOsmLine table.
    /// </summary>
    [Column("place")]
    public string? Place { get; set; }

    /// <summary>
    /// Represents the population property of a PlanetOsmLine.
    /// </summary>
    [Column("population")]
    public string? Population { get; set; }

    /// <summary>
    /// Represents the power attribute of a PlanetOsmLine object.
    /// </summary>
    [Column("power")]
    public string? Power { get; set; }

    /// <summary>
    /// Represents a power source.
    /// </summary>
    [Column("power_source")]
    public string? PowerSource { get; set; }

    /// <summary>
    /// Represents a public transport entity.
    /// </summary>
    [Column("public_transport")]
    public string? PublicTransport { get; set; }

    /// <summary>
    /// Represents a railway element in the GIS system.
    /// </summary>
    [Column("railway")]
    public string? Railway { get; set; }

    /// <summary>
    /// Represents a line in the Planet OSM data with various properties.
    /// </summary>
    [Column("ref")]
    public string? Ref { get; set; }

    /// <summary>
    /// Represents the religion associated with a geographical entity.
    /// </summary>
    [Column("religion")]
    public string? Religion { get; set; }

    /// <summary>
    /// Represents a line route in the OpenStreetMap database.
    /// </summary>
    [Column("route")]
    public string? Route { get; set; }

    /// <summary>
    /// Represents a service provided by an entity.
    /// </summary>
    [Column("service")]
    public string? Service { get; set; }

    /// <summary>
    /// Represents a shop.
    /// </summary>
    [Column("shop")]
    public string? Shop { get; set; }

    /// <summary>
    /// Represents a sport property.
    /// </summary>
    [Column("sport")]
    public string? Sport { get; set; }

    /// <summary>
    /// Represents the surface type of a planet_osm_line object.
    /// </summary>
    [Column("surface")]
    public string? Surface { get; set; }

    /// <summary>
    /// Represents a toll property of a PlanetOsmLine object.
    /// </summary>
    /// <value>
    /// The toll associated with the line.
    /// </value>
    /// <remarks>
    /// This property indicates whether a line has toll restrictions.
    /// </remarks>
    [Column("toll")]
    public string? Toll { get; set; }

    /// <summary>
    /// Represents a tourism property in the OSM database.
    /// </summary>
    [Column("tourism")]
    public string? Tourism { get; set; }

    /// <summary>
    /// Represents the type of tower.
    /// </summary>
    [Column("tower:type")]
    public string? TowerType { get; set; }

    [Column("tracktype")]
    public string? Tracktype { get; set; }

    /// <summary>
    /// Represents a tunnel feature in the OSM database.
    /// </summary>
    [Column("tunnel")]
    public string? Tunnel { get; set; }

    /// <summary>
    /// Represents a water feature.
    /// </summary>
    [Column("water")]
    public string? Water { get; set; }

    /// <summary>
    /// Represents a waterway in the PlanetOsmLine data.
    /// </summary>
    [Column("waterway")]
    public string? Waterway { get; set; }

    /// <summary>
    /// Represents a wetland on the planet.
    /// </summary>
    [Column("wetland")]
    public string? Wetland { get; set; }

    /// <summary>
    /// Gets or sets the width of the object.
    /// </summary>
    /// <value>
    /// The width value.
    /// </value>
    [Column("width")]
    public string? Width { get; set; }

    /// <summary>
    /// Represents a wood property of the PlanetOsmLine class.
    /// </summary>
    [Column("wood")]
    public string? Wood { get; set; }

    /// <summary>
    /// Represents the Z-order property of a PlanetOsmLine object.
    /// Z-order is a value that represents the order in which objects should be rendered in a 3D space, with higher values being rendered on top.
    /// </summary>
    [Column("z_order")]
    public int? ZOrder { get; set; }

    /// <summary>
    /// Represents the WayArea property of the PlanetOsmLine model.
    /// </summary>
    /// <remarks>
    /// This property represents the area of the way. It is defined as a float value.
    /// </remarks>
    [Column("way_area")]
    public float? WayArea { get; set; }

    /// <summary>
    /// Represents a line object in the Wikidata table.
    /// </summary>
    [Column("wikidata")]
    public string? Wikidata { get; set; }

    /// <summary>
    /// Represents the NameEtymologyWikidata property of the PlanetOsmLine class.
    /// </summary>
    [Column("name:etymology:wikidata")]
    public string? NameEtymologyWikidata { get; set; }

    /// <summary>
    /// Represents a line feature in the Planet OSM dataset with Wikipedia information.
    /// </summary>
    [Column("wikipedia")]
    public string? Wikipedia { get; set; }

    /// <summary>
    /// Represents a model class for the planet_osm_line table in the database.
    /// </summary>
    [Column("name:etymology:wikipedia")]
    public string? NameEtymologyWikipedia { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the OpenStreetMap data when this object was last modified.
    /// </summary>
    [Column("osm_timestamp")]
    public string? OsmTimestamp { get; set; }

    /// <summary>
    /// Represents a Way in the OSM database.
    /// </summary>
    [Column("way", TypeName = "geometry(LineString,3857)")]
    public Geometry? Way { get; set; }
}
