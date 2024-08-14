using Gis.Net.Core.DTO;
using Gis.Net.Vector.DTO;
using NetTopologySuite.Features;

namespace Gis.Net.Vector.Repositories;

/// <summary>
/// Abstract class that defines a GIS core delegate with a specific DTO type.
/// </summary>
/// <typeparam name="TDto">The type of data transfer object (DTO) that implements IGisBaseDto.</typeparam>
public abstract class GisCoreDelegate<TDto> : GisDelegate
	where TDto : GisDto
{
	/// <summary>
	/// Delegate for loading properties from a DTO into a Feature.
	/// </summary>
	/// <param name="feature">The feature to load properties into.</param>
	/// <param name="dto">The data transfer object containing the properties to load.</param>
	/// <returns>A task that represents the asynchronous operation of loading properties.</returns>
	public delegate Task<Feature> LoadPropertiesFromDelegate(Feature feature, TDto dto);
}