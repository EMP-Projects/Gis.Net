using AutoMapper;
using Gis.Net.OsmPg.Properties;
using Gis.Net.Vector.Repositories;
using Gis.Net.VectorCore.OsmPg.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Gis.Net.OsmPg.Vector;

/// <inheritdoc />
public class OsmVectorRepository<T> : GisVectorCoreRepository<OsmVectorModel, OsmVectorDto, OsmVectorQuery, T, OsmPropertiesDto, OsmPropertiesModel>
where T : DbContext
{

    /// <inheritdoc />
    public OsmVectorRepository(ILogger<OsmVectorRepository<T>> logger, T context, IMapper mapper) : base(logger, context, mapper)
    {
    }
}