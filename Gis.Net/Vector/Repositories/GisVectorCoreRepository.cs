using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Vector.Repositories;

/// <inheritdoc />
public abstract class GisVectorCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesDto, TPropertiesModel> : 
    GisCoreRepository<TModel, TDto, TQuery, TContext>
    where TDto: GisVectorDto<TPropertiesDto>
    where TModel: GisCoreModel<TPropertiesModel>
    where TQuery: GisVectorQuery, new()
    where TPropertiesDto: DtoBase
    where TPropertiesModel: ModelBase
    where TContext : DbContext
{
    /// <inheritdoc />
    protected GisVectorCoreRepository(ILogger logger, TContext context, IMapper mapper) : 
        base(logger, context, mapper) { }

    /// <inheritdoc />
    protected override IQueryable<TModel> ApplyIncludes(DbSet<TModel> table) 
        => table.Include(x => x.Properties);

    /// <inheritdoc />
    protected override IQueryable<TModel> ParseQueryParams(IQueryable<TModel> query, TQuery? queryByParams)
    {
        if (queryByParams?.PropertyId is not null)
            query = query.Where(f => f.IdProperties.Equals(queryByParams.PropertyId));
        
        if (queryByParams?.PropertyIds is not null)
            query = query.Where(x => queryByParams.PropertyIds.Contains(x.IdProperties));

        return base.ParseQueryParams(query, queryByParams);
    }
}