using Gis.Net.Core.Services;
using Gis.Net.VectorCore.OsmPg.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Gis.Net.OsmPg.Properties;

/// <inheritdoc />
public class OsmPropertiesService<T> : ServiceCore<OsmPropertiesModel, OsmPropertiesDto, OsmPropertiesQuery, OsmPropertiesRequest, T>
where T : DbContext, IOsmDbContext
{

    /// <inheritdoc />
    public OsmPropertiesService(
        ILogger<OsmPropertiesService<T>> logger, 
        OsmPropertiesRepository<T> repository) : base(logger, repository)
    {
    }
}