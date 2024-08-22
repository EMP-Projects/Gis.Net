namespace Gis.Net.Nominatim.Dto;

/// <inheritdoc />
public class ResultLimitation : IResultLimitations
{
    /// <summary>
    /// Level of detail required for the address.Default: 18.
    /// This is a number that corresponds roughly to the zoom level used in XYZ tile sources in frameworks like Leaflet.js, Openlayers etc.
    /// 
    /// In terms of address details the zoom levels are as follows:
    ///
    /// zoom address detail
    /// ----------------------
    /// 3	    country
    /// 5	    state
    /// 8	    county
    /// 10	    city
    /// 12	    town / borough
    /// 13	    village / suburb
    /// 14	    neighbourhood
    /// 15	    locality
    /// 16	    major streets
    /// 17	    major and minor streets
    /// 18	    building
    /// 
    /// </summary>
    public override int Zoom { get; set; } = 18;
    
    /// <summary>
    /// Limit search results to one or more countries.
    /// 
    /// countrycode must be the ISO 3166-1alpha2 code,
    /// e.g.gb for the United Kingdom, de for Germany.
    /// 
    /// Each place in Nominatim is assigned to one country code based on OSM country boundaries.
    /// In rare cases a place may not be in any country at all, for example, in international waters.
    /// </summary>
    public override List<string>? CountryCodes { get; set; } = new()
    {
        "it"
    };

    /// <summary>
    /// If you do not want certain OSM objects to appear in the search result, give a comma separated list of the place_ids you want to skip.
    /// This can be used to retrieve additional search results.
    ///
    /// For example, if a previous query only returned a few results, then including those here would cause the search to return other,
    /// less accurate, matches (if possible).
    ///
    /// </summary>
    public override List<string>? ExcludePlaceIds { get; set; } = null;

    /// <summary>
    /// 
    /// The preferred area to find search results.
    /// Any two corner points of the box are accepted as long as they span a real box.x is longitude, y is latitude.
    ///
    /// </summary>
    public override List<double>? ViewBox { get; set; } = null;

    /// <summary>
    ///
    /// When a viewbox is given, restrict the result to items contained within that viewbox(see above).
    /// When viewbox and bounded = 1 are given, an amenity only search is allowed.
    /// 
    /// Give the special keyword for the amenity in square brackets, e.g. [pub] and a selection of objects of this type is returned.
    /// There is no guarantee that the result is complete. (Default: 0)
    ///
    /// </summary>
    public override int Bounded { get; set; } = 0;

    /// <summary>
    /// Limit the number of returned results. (Default: 10, Maximum: 50)
    /// </summary>
    public override int Limit { get; set; } = 1;

    /// <summary>
    /// If you are making large numbers of request please include an appropriate email address to identify your requests.
    ///
    /// Info: https://operations.osmfoundation.org/policies/nominatim/
    /// </summary>
    public override string? Email { get; set; }
}