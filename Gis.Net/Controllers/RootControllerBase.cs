using AutoMapper;
using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

public abstract class RootControllerBase : ControllerBase
{
    protected readonly ILogger Logger;
    protected readonly IConfiguration Configuration;
    protected readonly IMapper Mapper;

    protected RootControllerBase(ILogger logger, IConfiguration configuration, IMapper mapper)
    {
        Logger = logger;
        Configuration = configuration;
        Mapper = mapper;
    }
    
    protected IActionResult ArrayResult<T>(IEnumerable<T> rows) where T : IDtoBase => Ok(new ArrayResult<T>(rows));
    protected IActionResult ArrayResultError<T>(string error) where T : IDtoBase => BadRequest(new ArrayResult<T>(error));
    protected IActionResult SingleResult<T>(T result) where T : IDtoBase => Ok(new SingleResult<T>(result));
    protected IActionResult SingleResultWithError<T>(string error) where T : IDtoBase => BadRequest(new SingleResult<T>(error));
}