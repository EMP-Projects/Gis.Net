using EcoSensorApi.Osm.Properties;
using Gis.Net.Core.Mapper;
using Gis.Net.VectorCore.OsmPg.Properties;
namespace Gis.Net.OsmPg.Properties;

public class OsmPropertiesMapper : AbstractMapperProfile<OsmPropertiesDto, OsmPropertiesModel, OsmPropertiesRequest>
{
    public OsmPropertiesMapper()
    {
        CreateMap<OsmProperties, OsmPropertiesDto>();
    }
}