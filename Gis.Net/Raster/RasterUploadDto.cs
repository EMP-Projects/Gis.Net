namespace Gis.Net.Raster;

/// <inheritdoc />
public class RasterUploadDto : IRasterUploadDto
{
    /// <inheritdoc />
    public string Url { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Body { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Path { get; set; } = string.Empty;

    /// <inheritdoc />
    public bool? Replace { get; set; } = true;
}