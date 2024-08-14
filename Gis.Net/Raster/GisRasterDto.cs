using System.Text.Json.Serialization;
using Gis.Net.Vector.DTO;

namespace Gis.Net.Raster;

public class GisRasterDto : GisDto, IGisRasterUpload
{
    [JsonPropertyName("pathFileRaster")]
    public string? PathFileRaster { get; set; }
}