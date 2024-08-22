using AutoMapper;
using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

/// <summary>
/// Represents a base controller for root entities in the Gis.Net application.
/// </summary>
public abstract class RootControllerBase : ControllerBase
{
    /// <summary>
    /// Represents a logger instance used for logging in the application.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Represents a configuration instance used for storing application configurations.
    /// </summary>
    protected readonly IConfiguration Configuration;

    /// <summary>
    /// Determines the mapping between different types of objects.
    /// </summary>
    protected readonly IMapper Mapper;

    /// <summary>
    /// Base controller class for root controllers.
    /// </summary>
    protected RootControllerBase(ILogger logger, IConfiguration configuration, IMapper mapper)
    {
        Logger = logger;
        Configuration = configuration;
        Mapper = mapper;
    }

    /// <summary>
    /// Represents a result object containing an array of data.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the array. This type must implement IDtoBase.</typeparam>
    /// <param name="rows">The collection of data to be included in the result.</param>
    /// <returns>An IActionResult representing the array result.</returns>
    protected IActionResult ArrayResult<T>(IEnumerable<T> rows) where T : IDtoBase => Ok(new ArrayResult<T>(rows));

    /// <summary>
    /// Represents a method in the RootControllerBase class that returns a BadRequest result with an error message wrapped in an ArrayResult object.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the ArrayResult object. This type must implement IDtoBase.</typeparam>
    /// <param name="error">The error message to be included in the ArrayResult object.</param>
    /// <returns>An IActionResult representing a BadRequest result with an ArrayResult object containing the error message.</returns>
    protected IActionResult ArrayResultError<T>(string error) where T : IDtoBase => BadRequest(new ArrayResult<T>(error));

    /// <summary>
    /// Returns a single result as an <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result, which must implement <see cref="IDtoBase"/>.</typeparam>
    /// <param name="result">The result to be returned.</param>
    /// <returns>The single result as an <see cref="IActionResult"/>.</returns>
    protected IActionResult SingleResult<T>(T result) where T : IDtoBase => Ok(new SingleResult<T>(result));

    /// <summary>
    /// Returns a single result operation containing either data or an error.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the result. This type must implement IDtoBase.</typeparam>
    /// <param name="error">The error message associated with the result.</param>
    /// <returns>An IActionResult object representing the single result operation.</returns>
    protected IActionResult SingleResultWithError<T>(string error) where T : IDtoBase => BadRequest(new SingleResult<T>(error));
}