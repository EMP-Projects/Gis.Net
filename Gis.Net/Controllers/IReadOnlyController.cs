using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Controllers;

/// <summary>
/// Represents a read-only controller interface with basic operations for querying and retrieving data.
/// </summary>
/// <typeparam name="TQuery">The type of the query parameters object.</typeparam>
public interface IReadOnlyController<in TQuery> where TQuery : QueryBase
{
    /// <summary>
    /// Retrieves a list of entities based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters for filtering the list of entities.</param>
    /// <returns>An action result containing the list of entities or an error message.</returns>
    Task<IActionResult> List([FromQuery] TQuery queryParams);
    
    /// <summary>
    /// Finds a single entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to find.</param>
    /// <returns>An action result containing the entity or an error message if not found.</returns>
    Task<IActionResult> Find(long id);
}