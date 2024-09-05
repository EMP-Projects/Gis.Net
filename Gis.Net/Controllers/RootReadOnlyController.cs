using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Exceptions;
using Gis.Net.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

/// <summary>
/// Abstract base class for read-only controllers that implement the IReadOnlyController interface.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TDto">The type of the DTO.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters.</typeparam>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TContext">The type of the database context.</typeparam>
public abstract class RootReadOnlyController<TModel, TDto, TQuery, TRequest, TContext> : 
    RootControllerBase,
    IReadOnlyController<TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
    where TRequest : RequestBase
    where TContext : DbContext
{

    /// <summary>
    /// Base class for all controllers that provide read-only operations on a service.
    /// </summary>
    protected readonly IServiceCore<TModel, TDto, TQuery, TRequest, TContext> ServiceCore;
    
    /// <inheritdoc />
    protected RootReadOnlyController(ILogger logger, 
        IConfiguration configuration, 
        IMapper mapper, 
        IServiceCore<TModel, TDto, TQuery, TRequest, TContext> serviceCore) : base(logger, configuration, mapper)
    {
        ServiceCore = serviceCore;
    }
    
    /// <summary>
    /// Method to extract a single record from the repository, searched by primary key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// 
    [HttpGet("token/{id:long}")]
    public virtual async Task<IActionResult> FindToken(long id)
    {
        try
        {
            var secret = Configuration["Secret"] is not null
                ? Configuration["Secret"]!
                : System.Reflection.Assembly.GetExecutingAssembly().GetName().Name!;
            var token = await ServiceCore.FindToken(id, secret);
            Response.ContentType = "text/plain";
            return Content(token!);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    /// <inheritdoc />
    [HttpGet]
    public virtual async Task<IActionResult> List([FromQuery] TQuery queryParams)
    {
        try
        {
            var result = await ServiceCore.List(queryParams);
            return ArrayResult(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return ArrayResultError<TDto>(ex.Message);
        }
    }

    /// <inheritdoc />
    [HttpGet("{id:long}")]
    public virtual async Task<IActionResult> Find(long id)
    {
        try
        {
            if (id == 0) throw new InvalidParameter(nameof(id));
            var result = await ServiceCore.Find(id);
            return SingleResult(result);
        }
        catch (Exception ex)
        {
            return SingleResultWithError<TDto>(ex.Message);
        }
    }
    
}