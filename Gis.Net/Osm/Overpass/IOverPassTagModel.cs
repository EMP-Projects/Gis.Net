namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents the model for an Overpass tag.
/// </summary>
/// <typeparam name="T">The type of the query.</typeparam>
public interface IOverPassTagModel<T> where T : class
{
    /// <summary>
    /// Represents the unique identifier of a query.
    /// </summary>
    long? QueryId { get; set; }
    
    /// <summary>
    /// Represents a query for the Overpass API.
    /// </summary>
    T? Query { get; set; }
    
    /// <summary>
    /// Represents a tag.
    /// </summary>
    string? Tag { get; set; }
    
    /// <summary>
    /// Gets or sets the description for the OverPass tag model.
    /// </summary>
    string? Description { get; set; }
}