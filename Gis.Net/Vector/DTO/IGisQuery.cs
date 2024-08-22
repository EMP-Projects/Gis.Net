namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS query.
/// </summary>
public interface IGisQuery
{

    /// <summary>
    /// Represents the minimum longitude value for a GIS query.
    /// </summary>
    double? LngXMin { get; set; }

    /// <summary>
    /// Represents the minimum latitude (Y) value of a GIS query.
    /// </summary>
    double? LatYMin { get; set; }

    /// <summary>
    /// Represents the maximum longitude value of a GIS query.
    /// </summary>
    double? LngXMax { get; set; }

    /// <summary>
    /// Represents the maximum latitude (Y-coordinate) value of a GIS query.
    /// </summary>
    double? LatYMax { get; set; }

    /// <summary>
    /// Represents the longitude coordinate of a GIS query.
    /// </summary>
    double? LngX { get; set; }

    /// <summary>
    /// Gets or sets the latitude (Y) value of a GIS query.
    /// </summary>
    double? LatY { get; set; } 
}