using NetTopologySuite.Features;

namespace Gis.Net.Vector;

/// <summary>
/// Represents a class that contains delegate definitions for GIS-related operations.
/// </summary>
public abstract class GisDelegate
{
    /// <summary>
    /// Defines a delegate for a method that asynchronously processes a feature and returns a modified feature.
    /// </summary>
    /// <param name="feature">The feature to be processed.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the processed feature.</returns>
    public delegate Task<Feature> CreatedFromDelegate(Feature feature);

    /// <summary>
    /// Defines a delegate for a method that asynchronously sorts a collection of features and returns an ordered queryable.
    /// </summary>
    /// <param name="features">The collection of features to be sorted.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ordered queryable of features.</returns>
    public delegate Task<IOrderedQueryable<Feature>> SortFromDelegate(IQueryable<Feature> features);
}