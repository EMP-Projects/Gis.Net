namespace Gis.Net.Vector;

/// <summary>
/// A static class representing the cardinal points and their respective degrees.
/// </summary>
public static class GisCardinalPoint
{
    /// <summary>
    /// Constant string representing the North cardinal point.
    /// </summary>
    private const string North = "NORTH";
        
    /// <summary>
    /// Constant string representing the South cardinal point.
    /// </summary>
    private const string South = "SOUTH";
        
    /// <summary>
    /// Constant string representing the North-East cardinal point.
    /// </summary>
    private const string NorthEast = "NORTHEAST";
        
    /// <summary>
    /// Constant string representing the North-West cardinal point.
    /// </summary>
    private const string NorthWest = "NORTHWEST";
        
    /// <summary>
    /// Constant string representing the South-East cardinal point.
    /// </summary>
    private const string SouthEast = "SOUTHEAST";
        
    /// <summary>
    /// Constant string representing the South-West cardinal point.
    /// </summary>
    private const string SouthWest = "SOUTHWEST";
        
    /// <summary>
    /// Constant string representing the East cardinal point.
    /// </summary>
    private const string East = "EAST";
        
    /// <summary>
    /// Constant string representing the West cardinal point.
    /// </summary>
    private const string West = "WEST";
        
    /// <summary>
    /// A dictionary mapping cardinal points to their slope degrees ranges.
    /// </summary>
    public static readonly Dictionary<string, double[]> SlopeDegrees = new()
    {
        { North, [0, 45] },
        { East, [45, 135] },
        { South, [135, 225] },
        { West, [225, 315] }
    };
        
    /// <summary>
    /// A dictionary mapping cardinal points to their points degrees ranges.
    /// </summary>
    public static readonly Dictionary<string, double[]> PointsDegrees = new()
    {
        { North, [0, 45] },
        { NorthEast, [45, 90] },
        { East, [90, 135] },
        { SouthEast, [135, 180] },
        { South, [180, 225] },
        { SouthWest, [225, 270] },
        { West, [270, 315] },
        { NorthWest, [315, 360] }
    };
}