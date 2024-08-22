using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents a base query class.
/// </summary>
public class QueryBase : IQueryBase
{
    /// Represents the Id property of a query object.
    /// /
    [FromQuery(Name = "id")]
    public long? Id { get; set; }

    /// <summary>
    /// Represents the key property of a query.
    /// </summary>
    [FromQuery(Name = "key")]
    public string? Key { get; set; }

    /// <summary>
    /// Represents a property used for searching.
    /// </summary>
    [FromQuery(Name = "search")]
    public string? Search { get; set; }
}