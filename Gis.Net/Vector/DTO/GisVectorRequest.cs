using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS vector request.
/// </summary>
public class GisVectorRequest : GisRequest
{
    /// <summary>
    /// Represents a geometry property.
    /// </summary>
    [FromQuery(Name = "geometry")] public string? Geometry { get; set; }

    /// <summary>
    /// Represents a file property in a GIS vector request.
    /// </summary>
    [FromQuery(Name = "file")] public IFormFile? File { get; set; }

    /// <summary>
    /// Represents a GIS request with a URL file.
    /// </summary>
    [FromQuery(Name = "url")] public string? UrlFile { get; set; }

    /// <summary>
    /// Represents a request object for uploading a body.
    /// </summary>
    [FromQuery(Name = "uploadBody"), JsonIgnore] public string? UploadBody { get; set; }

    /// <summary>
    /// Represents a GIS request for a raster file path.
    /// </summary>
    [FromQuery(Name = "pathFileRaster"), JsonIgnore] public string? PathFileRaster { get; set; }

    /// <summary>
    /// Represents a Raster property.
    /// </summary>
    [FromQuery(Name = "raster"), JsonIgnore] public string? Raster { get; set; }
}