using Microsoft.AspNetCore.Http;

namespace Gis.Net.Vector;

public interface IGisVectorUpload
{
    IFormFile? File { get; set; }
    
    string? UrlFile { get; set; }
    
    string? UploadBody { get; set; }
}