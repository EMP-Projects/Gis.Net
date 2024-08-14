namespace Gis.Net.Raster;

/// <summary>
/// Interface defining the properties required for uploading raster data.
/// </summary>
public interface IRasterUploadDto
{
    /// <summary>
    /// The URL where the raster data will be uploaded.
    /// </summary>
    string Url { get; set; }
    
    /// <summary>
    /// The body content of the upload request.
    /// </summary>
    string Body { get; set; }
    
    /// <summary>
    /// The file path of the raster data to be uploaded.
    /// </summary>
    string Path { get; set; }
    
    /// <summary>
    /// A flag indicating whether the existing raster data should be replaced.
    /// </summary>
    bool? Replace { get; set; }
}