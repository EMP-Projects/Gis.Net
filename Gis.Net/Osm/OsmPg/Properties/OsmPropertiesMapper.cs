using Gis.Net.Core.Mapper;

namespace Gis.Net.Osm.OsmPg.Properties;

public class OsmPropertiesMapper : AbstractMapperProfile<OsmPropertiesDto, OsmPropertiesModel, OsmPropertiesRequest>
{
    public OsmPropertiesMapper()
    {
        CreateMap<OsmProperties, OsmPropertiesDto>();
    }
}