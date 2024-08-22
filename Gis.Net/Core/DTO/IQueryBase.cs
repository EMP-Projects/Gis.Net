
namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents a base query interface.
/// </summary>
public interface IQueryBase
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <remarks>
    /// This property represents the ID of the object.
    /// </remarks>
    long? Id { get; set; }

    /// <summary>
    /// Represents a query base with an optional key parameter.
    /// </summary>
    string? Key { get; set; }

    /// <summary>
    /// Represents a query base object used for property search.
    /// </summary>
    string? Search { get; set; }
}