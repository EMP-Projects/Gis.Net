using Gis.Net.Vector.DTO;

namespace Gis.Net.Vector.Repositories;

/// <summary>
/// Interface defining GIS core options that extend the base IGisOptions interface.
/// </summary>
/// <typeparam name="TModel">The type of the model that implements IGisDataBase.</typeparam>
/// <typeparam name="TDto">The type of Data Transfer Object (DTO) that implements IGisBaseDto.</typeparam>
public interface IGisCoreOptions<TModel, TDto>
    where TDto : GisDto
    where TModel : Models.VectorModel
{
    /// <summary>
    /// Delegate that defines a method to load properties from a DTO into a model.
    /// </summary>
    /// <value>
    /// The delegate that handles the loading of properties. This can be set to a custom implementation if needed.
    /// </value>
    GisCoreDelegate<TDto>.LoadPropertiesFromDelegate? OnLoadProperties { get; set; } 
}