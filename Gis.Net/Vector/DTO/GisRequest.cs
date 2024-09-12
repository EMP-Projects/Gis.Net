using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS request.
/// </summary>
public class GisRequest : RequestBase, IGisRequest
{
    /// <summary>
    /// Represents the geometric filter used in a GIS request.
    /// </summary>
    [JsonPropertyName("geomFilter")] public string? GeomFilter { get; set; }

    /// <summary>
    /// Represents a GIS request object that extends the RequestBase class and implements the IGisRequest interface.
    /// </summary>
    [JsonPropertyName("measure")] public bool? Measure { get; set; }

    /// <summary>
    /// Represents a GIS request object.
    /// </summary>
    [JsonPropertyName("srCode"), JsonIgnore]
    public int? SrCode { get; set; } = (int)ESrCode.WebMercator;

    /// <summary>
    /// Represents a GIS buffer request object.
    /// </summary>
    [JsonPropertyName("buffer")] public double? Buffer { get; set; }

    /// <summary>
    /// Represents a GIS request object.
    /// </summary>
    [JsonPropertyName("boundary")] public bool? Boundary { get; set; }

    /// <summary>
    /// Represents a property for specifying whether to perform a difference operation.
    /// </summary>
    /// <remarks>
    /// This property is used in the <see cref="GisRequest"/> class to determine whether to perform a difference operation
    /// between two or more geometries.
    /// </remarks>
    [JsonPropertyName("difference")] public bool? Difference { get; set; }
}