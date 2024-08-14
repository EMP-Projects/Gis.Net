using Gis.Net.OsmPg.Models;
using NetTopologySuite.Geometries;

namespace Gis.Net.OsmPg;

/// <summary>
/// Opzioni per la generazione delle features da OpenStreetMap
/// </summary>
/// <typeparam name="T"></typeparam>
public class OsmOptions<T> where T : class, IOsmPgGeometryModel
{
    public required string Type { get; set; }
    
    public required string[] Tags { get; set; }
    
    public QueryDelegate<T>? OnBeforeQuery { get; set; }
    public EnumerableDelegate<T>? OnAfterQuery { get; set; }
    
    /// <summary>
    ///  Distanza di tolleranza pe la ricerca delle geometrie
    /// </summary>
    public double? DistanceMt { get; set; }
    
    /// <summary>
    /// Sistema di riferimento delle coordinate
    /// </summary>
    public int? SrCode { get; set; }
    
    /// <summary>
    /// Geometria richiesta per la collezione.
    /// </summary>
    public required Geometry? Geom { get; set; }

    public string? Error
    {
        get
        {
            if (Tags.Length == 0)
                return "Tags is required";
            if (DistanceMt == null)
                return "DistanceMt is required";
            if (SrCode == null)
                return "SrCode is required";
            if (Geom == null || !Geom.IsValid)
                return "Geom is required";
            return null;
        }
    }
}