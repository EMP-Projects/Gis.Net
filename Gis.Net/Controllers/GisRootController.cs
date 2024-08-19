using AutoMapper;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Gis.Net.Vector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

public abstract class GisRootController<TModel, TDto, TQuery, TRequest, TContext> : 
    RootReadOnlyController<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisVectorDto
    where TModel : VectorModel
    where TQuery : GisVectorQuery
    where TRequest : GisRequest
    where TContext : DbContext
{
    
    /// <inheritdoc />
    protected GisRootController(ILogger logger,
        IConfiguration configuration,
        IMapper mapper,
        IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> service) :
        base(logger, configuration, mapper, service)
    {
        
    }
    
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

    private async Task<IActionResult> ReadFeaturesCollection(TQuery? query)
    {
        if (query is { Error: not null, IsValid: false })
            throw new Exception(query.Error);

        if (ServiceCore is not IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> gisNetCoreService)
            throw new ApplicationException("Gis service not initialized");

        var features = await gisNetCoreService.FeatureCollection(query);
        return Ok(features);
    }

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
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> List([FromQuery] TQuery queryParams) 
        => await Task.Run(() => Ok(null));
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [NonAction]
    public override async Task<IActionResult> Find(long id) 
        => await Task.Run(() => Ok(null));
}