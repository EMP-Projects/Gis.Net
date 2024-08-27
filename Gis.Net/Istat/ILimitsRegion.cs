using NetTopologySuite.Geometries;

namespace Gis.Net.Istat;

/// <summary>
/// Represents a region with its limits and associated properties.
/// </summary>
public interface ILimitsRegion : ILimitsModelBase
{
    /// <summary>
    /// Gets or sets the name of the region.
    /// </summary>
    string? RegName { get; set; }

    /// <summary>
    /// Gets or sets the numeric ISTAT code of the region.
    /// </summary>
    int? RegIstatCodeNum { get; set; }

    /// <summary>
    /// Gets or sets the ISTAT code of the region.
    /// </summary>
    string? RegIstatCode { get; set; }

    /// <summary>
    /// Gets or sets the geometry of the region in Well-Known Binary (WKB) format.
    /// </summary>
    Geometry? WkbGeometry { get; set; }
}