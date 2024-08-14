using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Core.DTO;

public interface IQueryBase
{
    long? Id { get; set; }
    
    string? Key { get; set; }
    
    string? Search { get; set; }
}