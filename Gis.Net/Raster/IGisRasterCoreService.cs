using Gis.Net.Core.Entities;
using Gis.Net.Vector.Models;
using NetTopologySuite.Geometries;

namespace Gis.Net.Raster;

/// <summary>
/// Defines the contract for a service that handles operations on GIS raster data.
/// </summary>
/// <typeparam name="TModel">The type of the vector model, must inherit from GisVectorCoreModel.</typeparam>
public interface IGisRasterCoreService<TModel> where TModel : ModelBase
{
    /// <summary>
    /// Asynchronously inserts raster data into the service using a list of subdirectory paths.
    /// </summary>
    /// <param name="subDirectories">A list of subdirectory paths where the raster data files are located.</param>
    /// <returns>A task that represents the asynchronous raster insert operation.</returns>
    Task InsertRaster(List<string> subDirectories);

    /// <summary>
    /// Finds all vector entities that intersect with the specified geometry.
    /// </summary>
    /// <param name="geom">The geometry to test for intersection.</param>
    /// <returns>A list of vector entities that intersect with the specified geometry, or null if none are found.</returns>
    Task<List<TModel>?> Intersects(Geometry geom);

    /// <summary>
    /// Finds all vector entities that intersect with the specified point and buffer.
    /// </summary>
    /// <param name="geom">The point geometry to test for intersection.</param>
    /// <param name="buffer">The buffer distance around the point geometry.</param>
    /// <returns>A list of vector entities that intersect with the specified geometry and buffer, or null if none are found.</returns>
    Task<List<TModel>?> Intersects(Point geom, double buffer);
}