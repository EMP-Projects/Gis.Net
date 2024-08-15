using NetTopologySuite.Geometries;

namespace Gis.Net.Vector;

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
    
    public GisDelegate.CreatedFromDelegate? OnCreatedFeature { get; set; }

    /// <inheritdoc />
    public bool? Measure { get; set; }
    
    public string? GeomFilter { get; set; }
    
    public GisOptions(string geomFilter)
    {
        GeomFilter = geomFilter;
        Geom = GisUtility.CreateGeometryFromFilter(geomFilter, Buffer);
    }
    
    public GisOptions(double latY, double lngX)
    {
        Geom = GisUtility.CreatePoint((int)SrCode!, new[] { lngX, latY });
    }
    
    public GisOptions(double latYMin, double lngXMin, double latYMax, double lngXMax)
    {
        Geom = GisUtility.CreateGeometryFromBBox((int)SrCode!, lngXMin, latYMin, lngXMax, latYMax );
    }

    /// <inheritdoc />
    public string? SearchText { get; set; }
}