using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS query.
/// </summary>
public class GisQuery : QueryBase, IGisQuery, IGisRequest
{
    /// <summary>
    /// Gets or sets the minimum longitude value.
    /// </summary>
    [FromQuery(Name = "lngXMin")] public double? LngXMin { get; set; }

    /// <summary>
    /// Represents the minimum latitude value in a GIS query.
    /// </summary>
    [FromQuery(Name = "latXMin")] public double? LatYMin { get; set; }

    /// <summary>
    /// Gets or sets the maximum value of the X-axis longitude coordinate range for the query.
    /// </summary>
    [FromQuery(Name = "lngXMax")] public double? LngXMax { get; set; }

    /// <summary>
    /// Gets or sets the maximum latitude value for the geographic query.
    /// </summary>
    [FromQuery(Name = "latYMin")] public double? LatYMax { get; set; }

    /// <summary>
    /// Represents a request for querying geospatial data with a specified X-coordinate in longitude.
    /// </summary>
    [FromQuery(Name = "lngX")] public double? LngX { get; set; }

    /// <summary>
    /// Represents the latitude (Y coordinate) for a geographical point.
    /// </summary>
    [FromQuery(Name = "latY")] public double? LatY { get; set; }

    /// <summary>
    /// Represents a GIS query for measuring data.
    /// </summary>
    [FromQuery(Name = "measure")]
    public bool Measure { get; set; }

    /// <summary>
    /// Represents a GIS query with specific parameters.
    /// </summary>
    [FromQuery(Name = "srCode")]
    public int? SrCode { get; set; } = (int)ESrCode.WebMercator;

    /// <summary>
    /// Gets or sets the buffer value.
    /// </summary>
    [FromQuery(Name = "buffer")]
    public double? Buffer { get; set; }

    /// <summary>
    /// Represents a query parameter for boundary coordinates in GIS operations.
    /// </summary>
    [FromQuery(Name = "boundary")]
    public bool? Boundary { get; set; }

    /// <summary>
    /// Represents the property that specifies whether to use the "difference" parameter in a GIS query.
    /// </summary>
    /// <remarks>
    /// The "difference" parameter is used to specify whether to calculate the difference between two geometric shapes in a GIS query.
    /// The difference result represents the portion of the first shape that does not overlap with the second shape.
    /// This property is used in conjunction with the other GIS query parameters such as coordinates, buffer, and boundary.
    /// </remarks>
    [FromQuery(Name = "difference")]
    public bool? Difference { get; set; }
}