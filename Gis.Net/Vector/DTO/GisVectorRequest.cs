using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Vector.DTO;

public class GisVectorRequest : GisRequest
{
    [FromQuery(Name = "geometry")] public string? Geometry { get; set; }
    
    [FromQuery(Name = "file")] public IFormFile? File { get; set; }
    
    [FromQuery(Name = "url")] public string? UrlFile { get; set; }
    
    [FromQuery(Name = "uploadBody"), JsonIgnore] public string? UploadBody { get; set; }
    
    [FromQuery(Name = "pathFileRaster"), JsonIgnore] public string? PathFileRaster { get; set; }
    
    [FromQuery(Name = "raster"), JsonIgnore] public string? Raster { get; set; }
}