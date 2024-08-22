using Gis.Net.Core.Repositories;
using Gis.Net.Vector.DTO;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;

namespace Gis.Net.Vector.Repositories;

/// <summary>
/// Interface for GIS core repository that extends the base IRepository interface with GIS specific operations.
/// </summary>
/// <typeparam name="TDto">The type of Data Transfer Object (DTO) that implements IGisBaseDto.</typeparam>
/// <typeparam name="TModel">The type of the model that implements IGisDataBase.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters object that derives from GisCoreQueryByParams.</typeparam>
/// <typeparam name="TContext"></typeparam>
public interface IGisCoreRepository<TModel, TDto, TQuery, out TContext> :
    IRepositoryCore<TModel, TDto, TQuery, TContext>
    where TDto : GisDto
    where TModel : Models.VectorModel
    where TQuery : GisVectorQuery, new()
    where TContext : DbContext
{
    /// <summary>
    /// Retrieves a collection of features based on the specified options.
    /// </summary>
    /// <param name="options">The options specifying the criteria for retrieving features.</param>
    /// <returns>A task that represents the asynchronous operation, containing the retrieved FeatureCollection.</returns>
    Task<FeatureCollection> GetRows(GisOptionsGetRows<TModel, TDto, TQuery> options);
    
    /// <summary>
    /// Finds a single feature based on its identifier and specified options.
    /// </summary>
    /// <param name="id">The identifier of the feature to find.</param>
    /// <param name="options">The options specifying the criteria for retrieving the feature.</param>
    /// <returns>A task that represents the asynchronous operation, containing the found FeatureCollection.</returns>
    Task<FeatureCollection> Find(long id, GisOptionsGetRows<TModel, TDto, TQuery> options);

    /// <summary>
    /// Retrieves a collection of features based on the specified options.
    /// </summary>
    /// <param name="options">The options specifying the criteria for retrieving features.</param>
    /// <returns>A task that represents the asynchronous operation, containing the retrieved FeatureCollection.</returns>
    Task<List<IFeature>> GetFeatures(GisOptionsGetRows<TModel, TDto, TQuery> options);
}