using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Core.DTO;

public class QueryBase : IQueryBase
{
    [FromQuery(Name = "id")]
    public long? Id { get; set; }
    
    [FromQuery(Name = "key")]
    public string? Key { get; set; }
    
    [FromQuery(Name = "search")]
    public string? Search { get; set; }
}