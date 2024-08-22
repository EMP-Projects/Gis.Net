namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS request.
/// </summary>
public interface IGisRequest
{
    /// <summary>
    /// Represents a GIS measure property.
    /// </summary>
    bool Measure { get; set; }

    /// <summary>
    /// Represents the spatial reference system used for GIS data.
    /// </summary>
    int? SrCode { get; set; }

    /// <summary>
    /// Represents a buffer value for a GIS request.
    /// </summary>
    double? Buffer { get; set; }

    /// <summary>
    /// Represents the option to include boundary information in a GIS request.
    /// </summary>
    bool? Boundary { get; set; }

    /// <summary>
    /// Represents a GIS request.
    /// </summary>
    bool? Difference { get; set; }
}