using Gis.Net.Osm.OsmPg.Models;

namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Represents a delegate used for querying OSM geometries.
/// </summary>
/// <typeparam name="T">The type of OSM geometry model.</typeparam>
/// <param name="query">The query to be executed on the data source.</param>
/// <returns>An <see cref="IQueryable"/> of type T representing the results of the query.</returns>
/// <remarks>
/// The QueryDelegate is used to apply additional filtering or transformations to the query before it is executed.
/// It is typically used in conjunction with the <see cref="OsmOptions{T}"/> class to customize the behavior of the query.
/// </remarks>
public delegate IQueryable<T> QueryDelegate<T>(IQueryable<T> query) 
    where T : class, IOsmPgGeometryModel;

/// <summary>
/// Represents a delegate used for querying OSM geometries.
/// </summary>
/// <typeparam name="T">The type of OSM geometry model.</typeparam>
/// <param name="query">The query to be executed on the data source.</param>
/// <returns>An <see cref="IEnumerable{T}"/> representing the results of the query.</returns>
/// <remarks>
/// The EnumerableDelegate is used to apply additional filtering or transformations to the query before it is executed.
/// It is typically used in conjunction with the <see cref="OsmOptions{T}"/> class to customize the behavior of the query.
/// </remarks>
public delegate IEnumerable<T> EnumerableDelegate<T>(IEnumerable<T> query, string[] tags) 
    where T : class, IOsmPgGeometryModel;