namespace TeamSviluppo.Gis.DTO;

/// <summary>
/// Definisce i parametri per una query GIS.
/// </summary>
public interface IGisQuery 
{
    /// <summary>
    /// Ottiene o imposta la longitudine minima dell'area di interesse (X min).
    /// </summary>
    double? LngXMin { get; set; } 
    
    /// <summary>
    /// Ottiene o imposta la latitudine minima dell'area di interesse (Y min).
    /// </summary>
    double? LatYMin { get; set; } 
    
    /// <summary>
    /// Ottiene o imposta la longitudine massima dell'area di interesse (X max).
    /// </summary>
    double? LngXMax { get; set; } 
    
    /// <summary>
    /// Ottiene o imposta la latitudine massima dell'area di interesse (Y max).
    /// </summary>
    double? LatYMax { get; set; }
    
    /// <summary>
    /// Ottiene o imposta la longitudine del punto di interesse (X).
    /// </summary>
    double? LngX { get; set; } 

    /// <summary>
    /// Ottiene o imposta la latitudine del punto di interesse (Y).
    /// </summary>
    double? LatY { get; set; } 
}