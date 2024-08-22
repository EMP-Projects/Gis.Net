namespace Gis.Net.Raster;

/// Interface for uploading raster data.
/// /
public interface IGisRasterUpload
{
    /// <summary>
    /// Represents the path of the raster file.
    /// </summary>
    /// <remarks>
    /// This property gets or sets the path of the raster file.
    /// </remarks>
    string? PathFileRaster { get; set; }
}