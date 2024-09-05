using AutoMapper;
using Gis.Net.Osm.OsmPg.Properties;
using Gis.Net.Vector.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Gis.Net.Osm.OsmPg.Vector;

/// <inheritdoc />
public class OsmVectorRepository<T> : GisVectorCoreRepository<OsmVectorModel, OsmVectorDto, OsmVectorQuery, T, OsmPropertiesDto, OsmPropertiesModel>
where T : DbContext, IOsmDbContext
{

    /// <inheritdoc />
    public OsmVectorRepository(ILogger<OsmVectorRepository<T>> logger, T context, IMapper mapper) : base(logger, context, mapper)
    {
    }
}