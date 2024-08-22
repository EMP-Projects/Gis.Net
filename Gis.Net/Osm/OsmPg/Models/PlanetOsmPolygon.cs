using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a polygon in the Planet OSM data.
/// </summary>
[Keyless]
[Table("planet_osm_polygon")]
[Index("OsmId", Name = "planet_osm_polygon_osm_id_idx")]
public partial class PlanetOsmPolygon : IOsmPgGeometryModel
{
    /// <summary>
    /// Gets or sets the OSM ID of the polygon.
    /// </summary>
    [Column("osm_id")]
    public long? OsmId { get; set; }

    /// <summary>
    /// Gets or sets the access information for the polygon.
    /// </summary>
    [Column("access")]
    public string? Access { get; set; }

    /// <summary>
    /// Represents the house name associated with an address.
    /// </summary>
    [Column("addr:housename")]
    public string? AddrHousename { get; set; }

    /// <summary>
    /// Represents the house number of an address.
    /// </summary>
    [Column("addr:housenumber")]
    public string? AddrHousenumber { get; set; }

    /// <summary>
    /// Represents the interpolated address along a street segment.
    /// </summary>
    [Column("addr:interpolation")]
    public string? AddrInterpolation { get; set; }

    /// <summary>
    /// Represents the administrative level of an OSM polygon.
    /// </summary>
    [Column("admin_level")]
    public string? AdminLevel { get; set; }

    /// <summary>
    /// Represents the aerialway attribute of a PlanetOsmPolygon object.
    /// </summary>
    [Column("aerialway")]
    public string? Aerialway { get; set; }

    /// <summary>
    /// Represents the aeroway information of a <see cref="PlanetOsmPolygon"/>.
    /// </summary>
    [Column("aeroway")]
    public string? Aeroway { get; set; }

    /// <summary>
    /// Represents the amenity associated with a <see cref="PlanetOsmPolygon"/> object.
    /// </summary>
    [Column("amenity")]
    public string? Amenity { get; set; }

    /// <summary>
    /// Represents the area of a polygon in the PlanetOsmPolygon class.
    /// </summary>
    [Column("area")]
    public string? Area { get; set; }

    /// <summary>
    /// Represents a barrier attribute of a planet_osm_polygon object.
    /// </summary>
    [Column("barrier")]
    public string? Barrier { get; set; }

    /// <summary>
    /// Represents a bicycle.
    /// </summary>
    [Column("bicycle")]
    public string? Bicycle { get; set; }

    /// <summary>
    /// Represents the brand associated with a PlanetOsmPolygon.
    /// </summary>
    [Column("brand")]
    public string? Brand { get; set; }

    /// <summary>
    /// Represents the bridge property of a PlanetOsmPolygon.
    /// </summary>
    /// <remarks>
    /// This property represents whether a polygon is a bridge or not.
    /// </remarks>
    [Column("bridge")]
    public string? Bridge { get; set; }

    /// <summary>
    /// Represents the boundary property of a <see cref="PlanetOsmPolygon"/> object.
    /// </summary>
    /// <remarks>
    /// The boundary property is a string that represents the boundary type of a polygon in the Planet OSM data model.
    /// </remarks>
    [Column("boundary")]
    public string? Boundary { get; set; }

    /// <summary>
    /// Represents a building.
    /// </summary>
    [Column("building")]
    public string? Building { get; set; }

    /// <summary>
    /// Represents the construction property of a <see cref="PlanetOsmPolygon"/> object.
    /// </summary>
    [Column("construction")]
    public string? Construction { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the object is covered.
    /// </summary>
    [Column("covered")]
    public string? Covered { get; set; }

    /// <summary>
    /// Represents the Culvert property of a PlanetOsmPolygon.
    /// </summary>
    [Column("culvert")]
    public string? Culvert { get; set; }

    /// <summary>
    /// Represents the cutting property of a PlanetOsmPolygon.
    /// </summary>
    /// <remarks>
    /// This property indicates whether the polygon represents a cutting, which is a type of excavation or trench made in the ground.
    /// </remarks>
    [Column("cutting")]
    public string? Cutting { get; set; }

    /// <summary>
    /// The denomination of a place of worship.
    /// </summary>
    /// <remarks>
    /// This property represents the religious denomination of a place of worship, such as a church, mosque, or temple.
    /// </remarks>
    [Column("denomination")]
    public string? Denomination { get; set; }

    /// <summary>
    /// Represents the "disused" property of a <see cref="PlanetOsmPolygon"/> object.
    /// </summary>
    [Column("disused")]
    public string? Disused { get; set; }

    /// <summary>
    /// Represents an embankment in the PlanetOsmPolygon model.
    /// </summary>
    [Column("embankment")]
    public string? Embankment { get; set; }

    /// <summary>
    /// The foot property represents the foot access rights of a given feature.
    /// </summary>
    [Column("foot")]
    public string? Foot { get; set; }

    /// <summary>
    /// Represents the Generator Source property of a PlanetOsmPolygon object.
    /// </summary>
    [Column("generator:source")]
    public string? GeneratorSource { get; set; }

    /// <summary>
    /// Represents a harbour in the planet_osm_polygon table.
    /// </summary>
    [Column("harbour")]
    public string? Harbour { get; set; }

    /// <summary>
    /// Represents a highway in the OSM data.
    /// </summary>
    [Column("highway")]
    public string? Highway { get; set; }

    /// <summary>
    /// Represents the historic property of a planet_osm_polygon object.
    /// </summary>
    [Column("historic")]
    public string? Historic { get; set; }

    /// <summary>
    /// Represents a horse.
    /// </summary>
    [Column("horse")]
    public string? Horse { get; set; }

    /// <summary>
    /// Represents the intermittent property of a polygon in the Planet OSM database.
    /// </summary
    [Column("intermittent")]
    public string? Intermittent { get; set; }

    /// <summary>
    /// Represents a junction.
    /// </summary>
    [Column("junction")]
    public string? Junction { get; set; }

    /// <summary>
    /// Represents land use information for a specific polygon on the planet.
    /// </summary>
    [Column("landuse")]
    public string? Landuse { get; set; }

    /// <summary>
    /// Represents a layer in the PlanetOsmPolygon table.
    /// </summary>
    [Column("layer")]
    public string? Layer { get; set; }

    /// <summary>
    /// Represents the leisure attribute of a polygon in the OSM dataset.
    /// </summary>
    [Column("leisure")]
    public string? Leisure { get; set; }

    /// <summary>
    /// Represents a lock property.
    /// </summary>
    [Column("lock")]
    public string? Lock { get; set; }

    /// <summary>
    /// Represents a man-made object.
    /// </summary>
    [Column("man_made")]
    public string? ManMade { get; set; }

    /// <summary>
    /// Represents a military attribute of a planet_osm_polygon object.
    /// </summary>
    [Column("military")]
    public string? Military { get; set; }

    /// <summary>
    /// Represents a motorcar property of a <see cref="PlanetOsmPolygon"/>.
    /// </summary>
    [Column("motorcar")]
    public string? Motorcar { get; set; }

    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents a natural property of a <see cref="PlanetOsmPolygon"/>.
    /// </summary>
    [Column("natural")]
    public string? Natural { get; set; }

    /// <summary>
    /// Represents an office in the GIS system.
    /// </summary>
    [Column("office")]
    public string? Office { get; set; }

    /// <summary>
    /// Represents the "oneway" property of the <see cref="PlanetOsmPolygon"/> class.
    /// </summary>
    [Column("oneway")]
    public string? Oneway { get; set; }

    /// <summary>
    /// Represents an operator associated with a PlanetOsmPolygon object.
    /// </summary>
    [Column("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Represents a place on a map.
    /// </summary>
    [Column("place")]
    public string? Place { get; set; }

    /// <summary>
    /// Represents the population of a location.
    /// </summary>
    [Column("population")]
    public string? Population { get; set; }

    /// <summary>
    /// Represents the power attribute of a <see cref="PlanetOsmPolygon"/> object.
    /// </summary>
    [Column("power")]
    public string? Power { get; set; }

    /// <summary>
    /// Represents the power source of a PlanetOsmPolygon.
    /// </summary>
    [Column("power_source")]
    public string? PowerSource { get; set; }

    /// <summary>
    /// Represents a public transport entity.
    /// </summary>
    [Column("public_transport")]
    public string? PublicTransport { get; set; }

    /// <summary>
    /// Represents a railway feature.
    /// </summary>
    [Column("railway")]
    public string? Railway { get; set; }

    [Column("ref")]
    public string? Ref { get; set; }

    /// <summary>
    /// Represents a model class for the "planet_osm_polygon" table in the PostgreSQL database.
    /// </summary>
    [Column("religion")]
    public string? Religion { get; set; }

    /// <summary>
    /// Represents a route in the OSM database.
    /// </summary>
    [Column("route")]
    public string? Route { get; set; }

    /// <summary>
    /// Represents the Service property of PlanetOsmPolygon.
    /// </summary>
    [Column("service")]
    public string? Service { get; set; }

    /// <summary>
    /// Represents a shop in the OSM database.
    /// </summary>
    [Column("shop")]
    public string? Shop { get; set; }

    /// <summary>
    /// Represents a sport.
    /// </summary>
    [Column("sport")]
    public string? Sport { get; set; }

    /// <summary>
    /// Represents the surface property of a polygon in the planet_osm_polygon table.
    /// </summary>
    [Column("surface")]
    public string? Surface { get; set; }

    /// <summary>
    /// Represents the Toll property of a PlanetOsmPolygon object.
    /// </summary>
    [Column("toll")]
    public string? Toll { get; set; }

    /// <summary>
    /// Represents the tourism information associated with a location.
    /// </summary>
    [Column("tourism")]
    public string? Tourism { get; set; }

    /// <summary>
    /// Represents a tower type.
    /// </summary>
    [Column("tower:type")]
    public string? TowerType { get; set; }

    /// <summary>
    /// Represents the track type of a geographic feature in OpenStreetMap.
    /// </summary>
    [Column("tracktype")]
    public string? Tracktype { get; set; }

    /// <summary>
    /// Represents a tunnel feature in the OSM data.
    /// </summary>
    [Column("tunnel")]
    public string? Tunnel { get; set; }

    /// <summary>
    /// Represents the water property of a <see cref="PlanetOsmPolygon"/> object.
    /// </summary>
    [Column("water")]
    public string? Water { get; set; }

    /// <summary>
    /// Represents a waterway feature in OpenStreetMap.
    /// </summary>
    [Column("waterway")]
    public string? Waterway { get; set; }

    /// <summary>
    /// Represents a wetland.
    /// </summary>
    [Column("wetland")]
    public string? Wetland { get; set; }

    /// <summary>
    /// Gets or sets the width of the property.
    /// </summary>
    /// <remarks>
    /// The width is a string representing the width of the property.
    /// </remarks>
    [Column("width")]
    public string? Width { get; set; }

    /// <summary>
    /// Represents a property called Wood.
    /// </summary>
    [Column("wood")]
    public string? Wood { get; set; }

    /// <summary>
    /// Represents the Z-order (drawing order) of a polygon in a GIS system.
    /// </summary>
    [Column("z_order")]
    public int? ZOrder { get; set; }

    /// <summary>
    /// Represents the way area value of a <see cref="PlanetOsmPolygon"/>.
    /// </summary>
    [Column("way_area")]
    public float? WayArea { get; set; }

    /// <summary>
    /// Represents a model for the Wikidata property in the PlanetOsmPolygon table.
    /// </summary>
    [Column("wikidata")]
    public string? Wikidata { get; set; }

    /// <summary>
    /// Represents a model class for the "planet_osm_polygon" table in the OSM database.
    /// </summary>
    [Column("name:etymology:wikidata")]
    public string? NameEtymologyWikidata { get; set; }

    /// <summary>
    /// Represents a model for the Wikipedia property in the planet_osm_polygon table.
    /// </summary>
    [Column("wikipedia")]
    public string? Wikipedia { get; set; }

    /// <summary>
    /// Represents the NameEtymologyWikipedia property of the PlanetOsmPolygon class.
    /// Contains the name etymology information from Wikipedia.
    /// </summary>
    [Column("name:etymology:wikipedia")]
    public string? NameEtymologyWikipedia { get; set; }

    /// <summary>
    /// Represents the timestamp of an OpenStreetMap (OSM) entity.
    /// </summary>
    [Column("osm_timestamp")]
    public string? OsmTimestamp { get; set; }

    /// <summary>
    /// Represents the properties of a <see cref="PlanetOsmPolygon"/> entity.
    /// </summary>
    [Column("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Represents a property of the <see cref="PlanetOsmPolygon"/> class.
    /// </summary>
    [Column("way", TypeName = "geometry(Geometry,3857)")]
    public Geometry? Way { get; set; }
}
