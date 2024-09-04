using Gis.Net.Core.Mapper;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <summary>
/// This class is responsible for mapping between OsmPropertiesModel, OsmPropertiesDto, and OsmPropertiesRequest.
/// It inherits from the AbstractMapperProfile base class.
/// </summary>
public class OsmPropertiesMapper : AbstractMapperProfile<OsmPropertiesModel, OsmPropertiesDto, OsmPropertiesRequest>
{
    /// <inheritdoc />
    public OsmPropertiesMapper()
    {
        CreateMap<OsmProperties, OsmPropertiesDto>();
    }
}