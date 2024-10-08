using AutoMapper;
using Gis.Net.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Osm.OsmPg.Properties;

/// <inheritdoc />
public class OsmPropertiesRepository<T> : RepositoryCore<OsmPropertiesModel, OsmPropertiesDto, OsmPropertiesQuery, T>
where T : DbContext
{
    /// <inheritdoc />
    public OsmPropertiesRepository(
        ILogger<OsmPropertiesRepository<T>> logger, 
        T context,
        IMapper mapper) : 
        base(logger, context, mapper)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<OsmPropertiesModel> ParseQueryParams(IQueryable<OsmPropertiesModel> query, OsmPropertiesQuery? queryByParams)
    {
        if (queryByParams?.Name != null)
            query = query.Where(x => x.Name == queryByParams.Name);
        
        if (queryByParams?.Type != null)
            query = query.Where(x => x.Type != null && x.Type.Equals(queryByParams.Type));
        
        return query;
    }
}