using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector;

/// <summary>
/// Represents the options for performing GIS operations.
/// </summary>
public class GisOptions
{
    /// <summary>
    /// Reference system
    /// </summary>
    public int? SrCode { get; set; }
    
    /// <summary>
    /// Geometry to calculate slope
    /// </summary>
    public Geometry? Geom { get; set; }
    
    /// <summary>
    /// Meters to add buffer
    /// </summary>
    public double? Buffer { get; set; }

    /// <summary>
    /// Represents a delegate for a method that is called when a feature is created.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// This delegate is used in the <see cref="GisOptions"/> class to specify a method that will be called when a feature is created.
    /// The method must accept a <see cref="NetTopologySuite.Features.Feature"/> parameter and return a <see cref="Task"/> representing the asynchronous operation.
    /// The implementation of the method can modify the feature as needed.
    /// </remarks>
    public GisDelegate.CreatedFromDelegate? OnCreatedFeature { get; set; }

    /// <summary>
    /// Represents the option to include measurement in GIS operations.
    /// </summary>
    public bool? Measure { get; set; }

    /// <summary>
    /// Represents a filter string defining a geometry object and an optional buffer distance, used in GIS operations.
    /// </summary>
    /// <value>The filter string defining the geometry.</value>
    /// <remarks>
    /// The filter string defines the geometry object to be used in GIS operations. It can be in any valid string representation
    /// of a geometry object that is compatible with the NetTopologySuite.Geometries.Geometry class.
    /// </remarks>
    /// <example>
    /// This example shows how to create a GeometryFilter object:
    /// <code>
    /// GisOptions options = new GisOptions("POINT (50 60)");
    /// </code>
    /// </example>
    public string? GeomFilter { get; set; }

    /// <summary>
    /// Represents the options for performing GIS operations.
    /// </summary>
    public GisOptions(string geomFilter)
    {
        GeomFilter = geomFilter;
        Geom = GisUtility.CreateGeometryFromFilter(geomFilter, Buffer);
    }

    /// <summary>
    /// Represents the options for performing GIS operations.
    /// </summary>
    public GisOptions(double latY, double lngX)
    {
        Geom = GisUtility.CreatePoint((int)SrCode!, new[] { lngX, latY });
    }

    /// <summary>
    /// Represents the options for performing GIS operations.
    /// </summary>
    public GisOptions(double latYMin, double lngXMin, double latYMax, double lngXMax)
    {
        Geom = GisUtility.CreateGeometryFromBBox((int)SrCode!, lngXMin, latYMin, lngXMax, latYMax );
    }

    /// <inheritdoc />
    public string? SearchText { get; set; }
}