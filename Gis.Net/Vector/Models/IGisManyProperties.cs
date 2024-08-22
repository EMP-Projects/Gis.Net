using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents an interface for a GIS model with multiple properties.
/// </summary>
/// <typeparam name="TGis">The type of the GIS model.</typeparam>
/// <typeparam name="TModel">The type of the base model with the properties.</typeparam>
public interface IGisManyProperties<TGis, TModel>
    where TGis : GisCoreManyModel<TModel>
    where TModel : ModelBase
{
    /// <summary>
    /// Represents the GIS ID property of a GIS model with multiple properties.
    /// </summary>
    /// <remarks>
    /// The GIS ID is a unique identifier assigned to the GIS model.
    /// </remarks>
    long GisId { get; set; }
    
    /// <summary>
    /// Represents a GIS model with multiple properties.
    /// </summary>
    TGis? Gis { get; set; }
}