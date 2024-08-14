namespace Gis.Net.Vector;

/// <summary>
/// Defines spatial reference system codes used for GIS data.
/// </summary>
public enum ESrCode
{
    /// <summary>
    /// Represents the World Geodetic System 1984 (WGS 84) spatial reference.
    /// It is the standard for use in cartography, geodesy, and satellite navigation including GPS.
    /// Its EPSG code is 4326.
    /// </summary>
    Wgs84 = 4326,
    
    /// <summary>
    /// Represents the Web Mercator projection spatial reference.
    /// It is widely used by online mapping services, such as Google Maps and Bing Maps.
    /// Its EPSG code is 3857.
    /// </summary>
    WebMercator = 3857,
}