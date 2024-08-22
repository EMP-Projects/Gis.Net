using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace Gis.Net.Core.Entities;

/// <summary>
/// Represents the base class for all models in the system.
/// </summary>
[Index(nameof(EntityKey))]
public abstract class ModelBase : IModelBase, IFullTextSearch
{
    /// <inheritdoc />
    [Column("id"), Key]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the key of the entity.
    /// </summary>
    /// <remarks>
    /// The entity key is a string value that uniquely identifies the entity.
    /// </remarks>
    /// <value>The key of the entity.</value>
    [Column("entity_key")]
    public required string EntityKey { get; set; }

    /// <summary>
    /// Represents the full-text search property of a model.
    /// </summary>
    /// <remarks>
    /// This property is used to store the full-text search information for a model. It is implemented as a nullable NpgsqlTsVector property.
    /// </remarks>
    [Column("search_text")]
    public NpgsqlTsVector? SearchText { get; set; }

    /// <summary>
    /// Represents the timestamp property for models in the system.
    /// </summary>
    /// <remarks>
    /// The timestamp property is used to store the date and time when a model is created or last updated.
    /// By default, the timestamp is set to the current UTC date and time when a new instance of the model is created.
    /// The timestamp can be customized by implementing the IModelBase interface and overriding the TimeStamp property.
    /// </remarks>
    [Column("timestamp")]
    public required DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}