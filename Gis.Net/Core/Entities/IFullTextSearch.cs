using NpgsqlTypes;

namespace Gis.Net.Core.Entities;

/// <summary>
/// Represents an entity that supports full-text search.
/// </summary>
public interface IFullTextSearch
{
    /// <summary>
    /// Gets or sets the full text search vector.
    /// </summary>
    NpgsqlTsVector? SearchText { get; set; }
}