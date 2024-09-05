using AutoMapper;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Gis.Net.Vector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

/// <summary>
/// Root controller for handling GIS operations.
/// </summary>
/// <typeparam name="TModel">The type of the GIS vector model.</typeparam>
/// <typeparam name="TDto">The type of the DTO for the GIS vector.</typeparam>
/// <typeparam name="TQuery">The type of the query for GIS operations.</typeparam>
/// <typeparam name="TRequest">The type of the request for GIS operations.</typeparam>
/// <typeparam name="TContext">The type of the database context.</typeparam>
public abstract class GisRootController<TModel, TDto, TQuery, TRequest, TContext> : 
    RootReadOnlyController<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisVectorDto
    where TModel : VectorModel
    where TQuery : GisVectorQuery
    where TRequest : GisRequest
    where TContext : DbContext
{

    /// <summary>
    /// Base class for GIS root controllers that provide CRUD operations for vector data.
    /// </summary>
    protected GisRootController(ILogger logger,
        IConfiguration configuration,
        IMapper mapper,
        IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> service) :
        base(logger, configuration, mapper, service)
    {
        
    }

    /// <summary>
    /// Retrieves the center coordinates of a given query.
    /// </summary>
    /// <param name="query">The query object containing the parameters for the center calculation.</param>
    /// <returns>The center coordinates as an array of doubles.</returns>
    /// <exception cref="Exception">Thrown when the GIS service is not initialized.</exception>
    [HttpGet("center")]
    public virtual async Task<IActionResult> GetCenter([FromQuery] TQuery query) 
    {
        try
        {
            if (ServiceCore is not IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> gisNetCoreService)
                throw new Exception("Gis service not initialized");
            var features = await gisNetCoreService.Center(query);
            return Ok(features);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the extent of the GIS features based on the provided query.
    /// </summary>
    /// <param name="query">The query parameters used to filter the features.</param>
    /// <returns>An <see cref="IActionResult"/> containing the extent of the GIS features.</returns>
    [HttpGet("extent")]
    public virtual async Task<IActionResult> GetExtent([FromQuery] TQuery query) 
    {
        try
        {
            if (ServiceCore is not IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> gisNetCoreService)
                throw  new Exception("Gis service not initialized");
            var features = await gisNetCoreService.Extent(query);
            return Ok(features);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Reads a collection of features based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query parameters used to filter the features.</param>
    /// <returns>The collection of features.</returns>
    private async Task<IActionResult> ReadFeaturesCollection(TQuery? query)
    {
        if (query is { Error: not null, IsValid: false })
            throw new Exception(query.Error);

        if (ServiceCore is not IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> gisNetCoreService)
            throw new ApplicationException("Gis service not initialized");

        var features = await gisNetCoreService.FeatureCollection(query);
        return Ok(features);
    }

    /// <summary>
    /// Retrieves the features based on the provided query.
    /// </summary>
    /// <param name="query">The query parameters for retrieving the features.</param>
    /// <returns>The result of the feature retrieval operation.</returns>
    [HttpPost("features")]
    public virtual async Task<IActionResult> GetFeatures([FromBody] TQuery? query) 
    {
        try
        {
            return await ReadFeaturesCollection(query);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves features from the query parameters.
    /// </summary>
    /// <param name="query">The query parameters.</param>
    /// <returns>An IActionResult object representing the result of the operation.</returns>
    [HttpGet("features")]
    public virtual async Task<IActionResult> GetFeaturesFromQuery([FromQuery] TQuery? query) 
    {
        try
        {
            if (query?.GeomFilter is not null)
                throw new ArgumentException("GeomFilter parameter not allowed");
            
            return await ReadFeaturesCollection(query);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a list of entities based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters for filtering the list of entities.</param>
    /// <returns>An action result containing the list of entities or an error message.</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> List([FromQuery] TQuery queryParams) 
        => await Task.Run(() => Ok(null));

    /// <summary>
    /// Finds a single entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to find.</param>
    /// <returns>An action result containing the entity or an error message if not found.</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> Find(long id) 
        => await Task.Run(() => Ok(null));
}