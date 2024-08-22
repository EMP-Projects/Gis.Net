using NpgsqlTypes;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents the interface for full text search in a GIS core.
/// </summary>
public interface IGisCoreFullText
{
    /// <summary>
    /// Gets or sets the search text for full-text search.
    /// </summary>
    /// <remarks>
    /// This property is used to perform full-text search operations. It stores the search text in the format of `NpgsqlTsVector` which is used for efficient and accurate text searching.
    /// </remarks>
    /// <seealso cref="NpgsqlTypes.NpgsqlTsVector"/>
    NpgsqlTsVector? SearchText { get; set; }
}