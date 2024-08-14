using AutoMapper;
using Gis.Net.Core.Repositories;
using Gis.Net.VectorCore.OsmPg.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.OsmPg.Properties;

/// <inheritdoc />
public class OsmPropertiesRepository<T> : RepositoryCore<OsmPropertiesModel, OsmPropertiesDto, OsmPropertiesQuery, T>
where T : DbContext, IOsmDbContext
{
    /// <inheritdoc />
    public OsmPropertiesRepository(
        ILogger<OsmPropertiesRepository<T>> logger, 
        T context,
        IMapper mapper) : 
        base(logger, context, mapper)
    {
    }
    
    protected override IQueryable<OsmPropertiesModel> ParseQueryParams(IQueryable<OsmPropertiesModel> query, OsmPropertiesQuery? queryByParams)
    {
        if (queryByParams?.Name != null)
            query = query.Where(x => x.Name == queryByParams.Name);
        
        if (queryByParams?.Type != null)
            query = query.Where(x => x.Type.Equals(queryByParams.Type));
        
        return query;
    }
}