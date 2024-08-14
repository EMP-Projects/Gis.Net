using Gis.Net.Core.Repositories;
using Gis.Net.Vector.DTO;

namespace Gis.Net.Vector.Repositories;

/// <summary>
/// Class representing the options for retrieving rows with GIS functionalities.
/// </summary>
/// <typeparam name="TDto">The type of Data Transfer Object (DTO) that implements IGisBaseDto.</typeparam>
/// <typeparam name="TModel">The type of the model that implements IGisDataBase.</typeparam>
/// <typeparam name="TQuery">The type of the query parameters object that derives from GisCoreQueryByParams.</typeparam>
public class GisOptionsGetRows<TModel, TDto, TQuery>(TQuery queryParams) :
    ListOptions<TModel, TDto, TQuery>(queryParams),
    IGisCoreOptions<TModel, TDto>
    where TDto : GisDto
    where TModel : Models.VectorModel
    where TQuery : GisVectorQuery, new()
{
    /// <summary>
    /// Delegate that defines a method to handle the feature creation event.
    /// </summary>
    public GisDelegate.CreatedFromDelegate? OnCreatedFeature { get; set; }
    
    /// <summary>
    /// Delegate that defines a method to load properties from a DTO into a model.
    /// </summary>
    public GisCoreDelegate<TDto>.LoadPropertiesFromDelegate? OnLoadProperties { get; set; }
}