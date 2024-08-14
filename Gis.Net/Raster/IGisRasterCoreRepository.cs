using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using NetTopologySuite.Geometries;

namespace Gis.Net.Raster;

/// <summary>
/// Defines the contract for a repository that handles operations on GIS raster data.
/// </summary>
/// <typeparam name="TModel">The type of the raster model, must inherit from GisRasterCoreModel.</typeparam>
/// <typeparam name="TDto">The type of the data transfer object for rasters, must inherit from GisRasterCoreDto.</typeparam>
/// <typeparam name="TPropertiesModel"></typeparam>
/// <typeparam name="TPropertiesDto"></typeparam>
public interface IGisRasterCoreRepository<TModel, in TDto, TPropertiesModel, TPropertiesDto> 
    where TDto : GisVectorDto<TPropertiesDto>
    where TModel : GisCoreModel<TPropertiesModel>
    where TPropertiesModel: ModelBase
    where TPropertiesDto: DtoBase
{
    /// <summary>
    /// Asynchronously inserts raw raster data into the database using the specified DTO and file path.
    /// </summary>
    /// <param name="dto">The data transfer object containing the data to be inserted.</param>
    /// <param name="path">The file path to the binary file containing the raw raster data.</param>
    /// <returns>A task that represents the asynchronous insert operation, containing the inserted raster model or null if the operation fails.</returns>
    Task<int> InsertRaw(TDto dto, string path);
    
    Task<string> UploadRaster(IRasterUploadDto options);

    /// <summary>
    /// Finds all vector entities that intersect with the specified point and buffer.
    /// </summary>
    /// <param name="geom">The point geometry to test for intersection.</param>
    /// <param name="buffer">The buffer distance around the point geometry.</param>
    /// <returns>A list of vector entities that intersect with the specified geometry.</returns>
    Task<List<TModel>> Intersects(Point geom, double buffer);

    /// <summary>
    /// Finds all vector entities that intersect with the specified geometry.
    /// </summary>
    /// <param name="geom">The geometry to test for intersection.</param>
    /// <returns>A list of vector entities that intersect with the specified geometry, or null if none are found.</returns>
    Task<List<TModel>?> Intersects(Geometry geom);

    /// <summary>
    /// Finds all vector entities that intersect with the specified point and buffer, filtered by a timestamp.
    /// </summary>
    /// <param name="geom">The point geometry to test for intersection.</param>
    /// <param name="buffer">The buffer distance around the point geometry.</param>
    /// <param name="timestamp">The timestamp to filter the results.</param>
    /// <returns>A list of vector entities that intersect with the specified geometry and match the timestamp filter.</returns>
    Task<List<TModel>?>? Intersects(Point geom, double buffer, DateTime timestamp);
}