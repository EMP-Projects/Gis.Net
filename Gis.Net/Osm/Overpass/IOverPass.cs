using NetTopologySuite.Features;

namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents the Overpass service for querying OpenStreetMap data.
/// </summary>
public interface IOverPass
{
    /// <summary>
    /// Find geometry from Openstreetmap by intersect with geometry
    /// </summary>
    /// <param name="options">The OverPassOptions object containing the query and geometry information</param>
    /// <returns>The FeatureCollection representing the intersected geometries</returns>
    /// <exception cref="ArgumentException">Thrown when the geometry is not valid to execute overpass queries</exception>
    Task<FeatureCollection?> Intersects(OverPassOptions options);
}