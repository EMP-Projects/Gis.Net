namespace Gis.Net.Core.Entities;

/// <summary>
/// Represents the base interface for all models in the system.
/// </summary>
public interface IModelBase
{
    /// <summary>
    /// Gets or sets the unique identifier of the property.
    /// </summary>
    long Id { get; set; }
    
    /// <summary>
    /// Represents the entity key of a model.
    /// </summary>
    string EntityKey { get; set; }
    
    /// <summary>
    /// Represents the time stamp of a model.
    /// </summary>
    DateTime TimeStamp { get; set; }
}