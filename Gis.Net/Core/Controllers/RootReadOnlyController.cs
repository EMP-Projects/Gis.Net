using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Exceptions;
using Gis.Net.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TeamSviluppo.Controllers;

namespace Gis.Net.Core.Controllers;

public abstract class RootReadOnlyController<TDto, TModel, TQuery, TRequest> : 
    ControllerUtils,
    IReadOnlyController<TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
    where TRequest : RequestBase
{

    protected readonly ILogger Logger;
    private readonly IConfiguration _configuration;
    protected readonly IMapper Mapper;
    
    /// <inheritdoc />
    protected RootReadOnlyController(ILogger logger, 
        IConfiguration configuration, 
        IMapper mapper, 
        IServiceCore<TDto, TModel, TQuery, TRequest> serviceCore)
    {
        ServiceCore = serviceCore;
        Mapper = mapper;
        _configuration = configuration;
        Logger = logger;
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
            var secret = _configuration["Secret"] is not null
                ? _configuration["Secret"]!
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
    
    /// <summary>
    /// The service that provides operations for data manipulation and retrieval.
    /// </summary>
    /// <remarks>
    /// This field is used to access the service layer operations for DTOs (Data Transfer Objects),
    /// entities (Models), and query objects. It should be initialized during the controller's
    /// construction and provides methods to list, find, create, update, and delete entities as well
    /// as to apply queries to the data store.
    /// </remarks>
    protected readonly IServiceCore<TDto, TModel, TQuery, TRequest> ServiceCore;

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