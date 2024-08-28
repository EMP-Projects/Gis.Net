using Gis.Net.Osm.OsmPg.Models;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Represents the options for querying OSM geometries.
/// </summary>
/// <typeparam name="T">The type of OSM geometry model.</typeparam>
public class OsmOptions<T> where T : class, IOsmPgGeometryModel
{
    /// <summary>
    /// Represents the options for querying OSM data.
    /// </summary>
    /// <typeparam name="T">The type of the OSM geometry model.</typeparam>
    public required string Type { get; set; }

    /// <summary>
    /// Represents the options for querying OSM entities.
    /// </summary>
    /// <typeparam name="T">The type of OSM geometry model.</typeparam>
    public string[]? Tags { get; set; }

    /// <summary>
    /// Represents an event that is triggered before executing the query.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="query">The query to be executed.</param>
    /// <returns>The modified query.</returns>
    public QueryDelegate<T>? OnBeforeQuery { get; set; }
    /// <summary>
    /// Represents a delegate method that is called after executing a query in the OsmPg class.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the query result.</typeparam>
    /// <param name="query">The query result.</param>
    /// <param name="tags">The array of tags to filter the entities.</param>
    /// <returns>The filtered query result.</returns>
    public EnumerableDelegate<T>? OnAfterQuery { get; set; }

    /// <summary>
    /// Represents the distance in meters used for querying OSM options.
    /// </summary>
    /// <remarks>
    /// The distance in meters determines the radius within which the OSM data will be queried.
    /// </remarks>
    public double? DistanceMt { get; set; }

    /// <summary>
    /// Represents the SrCode property of the OsmOptions class.
    /// </summary>
    public int? SrCode { get; set; }

    /// <summary>
    /// Represents the options for querying OSM entities with spatial constraints.
    /// </summary>
    /// <typeparam name="T">The type of the OSM geometry model.</typeparam>
    public required Geometry? Geom { get; set; }
    
    /// <summary>
    /// Represents an error message related to the <see cref="OsmOptions{T}"/> class.
    /// </summary>
    public string? Error
    {
        get
        {
            if (DistanceMt == null)
                return "DistanceMt is required";
            if (SrCode == null)
                return "SrCode is required";
            if (Geom == null || !Geom.IsValid)
                return "Geom is required";
            return null;
        }
    }
}