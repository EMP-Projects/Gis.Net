namespace Gis.Net.Vector;

/// <summary>
/// Represents the options for a GIS service.
/// </summary>
/// <typeparam name="T">The type of the service options.</typeparam>
public abstract class GisServiceOptions<T> : GisOptions where T : class
{

    /// <summary>
    /// Represents a delegate for a method that asynchronously sorts a collection of features and returns an ordered queryable.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ordered queryable of features.</returns>
    public GisDelegate.SortFromDelegate? OnSort { get; set; }

    /// <summary>
    /// Represents the options for a GIS service.
    /// </summary>
    protected GisServiceOptions(string geomFilter) : base(geomFilter)
    {
    }
}