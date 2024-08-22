
namespace Gis.Net.Osm.Overpass;

/// <summary>
/// Represents an interface for an Overpass model.
/// </summary>
/// <typeparam name="T">The type of the Tags collection.</typeparam>
public interface IOverPassModel<T> where T : class
{
    /// <summary>
    /// Represents the collection of tags associated with an Overpass model.
    /// </summary>
    /// <typeparam name="T">The type of the tags.</typeparam>
    ICollection<T>? Tags { get; set; }

    /// <summary>
    /// Represents a query for the overpass API.
    /// </summary>
    string? Query { get; set; }

    /// <summary>
    /// Represents the type of an OverPass model.
    /// </summary>
    EOsmTag? Type { get; set; }

    /// <summary>
    /// Gets or sets the description of the property.
    /// </summary>
    string? Description { get; set; }
}