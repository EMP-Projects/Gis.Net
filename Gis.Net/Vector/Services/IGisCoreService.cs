using Gis.Net.Core.Services;
using Gis.Net.Vector.DTO;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;

namespace Gis.Net.Vector.Services;

/// <summary>
/// Defines the contract for a core GIS service that provides various spatial data operations.
/// </summary>
/// <typeparam name="TDto">The Data Transfer Object type which must inherit from DtoBase and IGisBaseDto.</typeparam>
/// <typeparam name="TModel">The model type which must inherit from ModelBase and IGisDataBase.</typeparam>
/// <typeparam name="TQuery">The query type which must be a GisCoreQueryByParams.</typeparam>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TContext"></typeparam>
public interface IGisCoreService<TModel, TDto, TQuery, in TRequest, out TContext>: 
    IServiceCore<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisDto
    where TModel : Models.VectorModel
    where TQuery : GisVectorQuery
    where TRequest : GisRequest
    where TContext : DbContext
{
    /// <summary>
    /// Asynchronously retrieves a FeatureCollection based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The parameters defining the query for the feature collection.</param>
    /// <returns>A task representing the asynchronous operation, containing the resulting FeatureCollection or null if no features are found.</returns>
    Task<FeatureCollection?> FeatureCollection(TQuery? queryParams);

    /// <summary>
    /// Asynchronously deletes features based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The parameters defining the query for deletion.</param>
    /// <returns>A task representing the asynchronous operation, containing the delete results for the models.</returns>
    Task<List<TModel>> Delete(TQuery queryParams);
    
    /// <summary>
    /// Asynchronously adds a new model to the GIS data storage.
    /// </summary>
    /// <param name="model">The model to add.</param>
    /// <returns>A task representing the asynchronous operation of adding the model.</returns>
    Task AddModel(TModel model);
    
    /// <summary>
    /// Asynchronously adds multiple models to the GIS data storage.
    /// </summary>
    /// <param name="models">The collection of models to add.</param>
    /// <returns>A task representing the asynchronous operation of adding the models.</returns>
    Task AddModels(IEnumerable<TModel> models);
    
    string? NameProperties { get; set; }

    Task<TModel?> Upload(TDto dto);

    Task<double[]> Center(TQuery query);
    
    Task<double[]> Extent(TQuery query);
}