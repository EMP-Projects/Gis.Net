
namespace Gis.Net.Osm;

/// <summary>
/// The OsmTag class provides methods to work with OpenStreetMap tags.
/// </summary>
public static class OsmTag
{
    /// <summary>
    /// Converts an enumeration of OsmTag to a corresponding string value.
    /// </summary>
    /// <param name="tag">The OsmTag enumeration value.</param>
    /// <returns>The string representation of the OsmTag enumeration.</returns>
    public static string Tags(EOsmTag tag)
    {
        return tag switch
        {
            EOsmTag.Amenity => "amenity",
            EOsmTag.AeroWay => "aeroway",
            EOsmTag.Building => "building",
            EOsmTag.Boundary => "boundary",
            EOsmTag.Historic => "historic",
            EOsmTag.LandUse => "landuse",
            EOsmTag.Military => "military",
            EOsmTag.Natural => "natural",
            EOsmTag.Office => "office",
            EOsmTag.Place => "place",
            EOsmTag.Railway => "railway",
            EOsmTag.Religion => "religion",
            EOsmTag.Sport => "sport",
            EOsmTag.Tourist => "tourist",
            EOsmTag.Waterway => "waterway",
            EOsmTag.Leisure => "leisure",
            EOsmTag.Tourism => "tourism",
            EOsmTag.ManMade => "man_made",
            EOsmTag.Denomination => "denomination",
            EOsmTag.Highway => "highway",
            EOsmTag.AerialWay => "aerialway",
            EOsmTag.Emergency => "emergency",
            _ => string.Empty
        };
    }

    /// <summary>
    /// Retrieves the items related to a specific OsmTag.
    /// </summary>
    /// <param name="tag">The OsmTag enum value.</param>
    /// <returns>An array of strings representing the related items.</returns>
    public static string[] Items(EOsmTag tag) => DictionaryTags()[Tags(tag)];

    /// <summary>
    /// Gets the dictionary of tags for OSM data.
    /// </summary>
    /// <returns>The dictionary of tags.</returns>
    private static Dictionary<string, string[]> DictionaryTags()
    {
        return new Dictionary<string, string[]>
        {
            {
                "boundary", ["national_park"]
            },
            {
                "aeroway", ["helipad", "apron"]
            },
            {
                "military", ["airfield"]
            },
            {
                "landuse", ["cemetery", "residential", "industrial", "allotments", "meadown", "commercial", "quarry", "orchad", "vineyard", "scrub", "grass", "farmland", "farmyard", "reservoir"]
            },
            {
                "historic", ["monument", "memorial", "castle", "ruins", "archaeological_site", "wayside_criss", "wayside_shrine", "battlefield", "fort"]
            },
            {
                "emergency", ["phone"]
            },
            {
                "man_made", ["surveillance", "tower", "pier"]
            },
            {
                "tourist", ["information", "attraction", "artwork"]
            },
            {
                "tourism", ["hotel", "motel", "bed_and_breakfast", "guest_house", "hostel", "chalet", "camp_site", "alpine_hut", "caravan_site", "picnic_site", "viewpoint", "zoo", "theme_park"]
            },
            {
                "shop", [
                    "supermarket", "bakery", "kiosk", "mall", "department_store", "general", "convenience", "clothes", "florist", "chemist", "books",
                    "butcher", "shoes", "alcohol", "beverages", "optician", "jewerly", "gift", "sports", "stationery", "outdoor", "mobile_phone", "toys", 
                    "newsagent", "greengrocer", "beauty", "video", "car", "bicycle", "doityourself", "hardware", "furniture", "computer", "garden_centre", "hairdresser",
                    "car_repair", "travel_agency", "laundry", "dry_cleaning"
                ]
            },
            {
                "sport", ["tennis"]
            },
            { 
                "leisure", [
                    "park" ,"playground" , "dog_park" ,"sports_centre" ,"pitch" , "swimming_pool" , "water_park" , "golf_course" , "stadium" , "ice_rink" , 
                    "nature_reserve" , "recreation_ground" , "retail" , "slipway" , "marina"
                ] },
            { "office", ["diplomatic"] },
            { "amenity", [
                "place_of_worship","police","fire_station","post_office","telephone","library","townhall",
                "courthouse","prison","embassy","community_centre","nursing_home","arts_centre","grave_yard",
                "marketplace","university","school","kindergarten","college","public_building","pharmacy",
                "hospital","doctors","dentist","veterinary","theatre","nightclub","cinema","swimming_pool",
                "restaurant","fast_food","cafe","pub","bar","food_court","biergarten","shelter","car_rental",
                "car_wash","car_sharing","bicycle_rental","travel_agency","laundry","dry_cleaning","vending_machine",
                "bank","atm","toilets","bench","drinking_water","fountain","hunting_stand","waste_basket","emergency_phone",
                "fire_hydrant","fuel","park","bus_station","taxi","airport","airfield","ferry_terminal" 
            ]},
            { "denomination", [
                "anglican","catholic","evangelical","lutheran","methodist","orthodox","protestant","baptist",
                "mormon","sunni","shia"
            ] },
            { "religion", [
                    "christian","jewish","muslim","buddhist","hindu","taoist","shintoist","sikh"
                ] },
            { "place", [
                "city","town","village","hamlet","suburb","island","farm","isolated_dwelling","region",
                "county","locality"
            ]},
            { "natural", [
                "spring","glacier","peak","cliff","volcano","tree","mine","cave_entrance","beach","wood",
                "health","water","glacier","wetland"
            ] },
            { "waterway", [
                "river", "stream", "canal", "drain", "dam", "waterfall", "lock_gate", "weir", "riverbank", 
                "dock"
            ] },
            { "highway", [
                "motorway","trunk", "primary", "secondary", "tertiary", "unclassified", "residential", 
                "living_street", "pedestrian", "busway", "motorway_link", "trunk_link", "primary_link", 
                "secondary_link", "tertiary_link", "service", "track", "bridleway", "cycleway", "footway", 
                "path", "steps", "emergency_access_point", "traffic_signals", "mini_roundabou", "stop", 
                "crossing", "level_crossing", "ford", "motorway_junction", "turning_circle", "speed_camera", 
                "street_lamp", "services", "bicycle_parking", "bus_stop"
            ] },
            { "railway", [
                "rail","light_rail","subway","tram","monorail","narrow_gauge","miniature","funicular",
                "rack","station","halt","tram_stop"
            ] },
            { "aerialway", [
                    "drag_lift","chair_lift","cable_car","gondola","goods","station"
                ] }
        };

    }
}