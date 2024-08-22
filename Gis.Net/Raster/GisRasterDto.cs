using System.Text.Json.Serialization;
using Gis.Net.Vector.DTO;

namespace Gis.Net.Raster;

/// <inheritdoc cref="Gis.Net.Raster.IGisRasterUpload" />
public class GisRasterDto : GisDto, IGisRasterUpload
{
    /// <inheritdoc />
    [JsonPropertyName("pathFileRaster")]
    public string? PathFileRaster { get; set; }
}