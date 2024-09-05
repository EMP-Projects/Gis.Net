using Gis.Net.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Gis.Net.Osm.OsmPg.Properties;

/// <inheritdoc />
public class OsmPropertiesService<T> : ServiceCore<OsmPropertiesModel, OsmPropertiesDto, OsmPropertiesQuery, OsmPropertiesRequest, T>
where T : DbContext
{

    /// <inheritdoc />
    public OsmPropertiesService(
        ILogger<OsmPropertiesService<T>> logger, 
        OsmPropertiesRepository<T> repository) : base(logger, repository)
    {
    }
}