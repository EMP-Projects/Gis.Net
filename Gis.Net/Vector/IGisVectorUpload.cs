using Microsoft.AspNetCore.Http;

namespace Gis.Net.Vector;

/// <summary>
/// Represents an interface for uploading GIS vector data.
/// </summary>
public interface IGisVectorUpload
{
    /// <summary>
    /// Represents a file for upload in a GIS vector data upload operation.
    /// </summary>
    IFormFile? File { get; set; }

    /// <summary>
    /// Represents a URL file for GIS vector upload.
    /// </summary>
    string? UrlFile { get; set; }

    /// <summary>
    /// Represents the body of an upload request for GIS vector data.
    /// </summary>
    /// <remarks>
    /// The <see cref="UploadBody"/> property can be set with the body content of the upload request.
    /// </remarks>
    string? UploadBody { get; set; }
}