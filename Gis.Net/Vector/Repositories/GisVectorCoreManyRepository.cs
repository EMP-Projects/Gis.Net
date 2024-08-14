using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Vector.Repositories;

/// <inheritdoc />
public abstract class GisVectorCoreManyRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto> : 
    GisCoreRepository<TModel, TDto, TQuery, TContext>
    where TDto: GisVectorManyDto<TPropertiesDto>
    where TModel: GisCoreManyModel<TPropertiesModel>
    where TQuery: GisVectorQuery, new()
    where TPropertiesModel: ModelBase
    where TPropertiesDto: DtoBase
    where TContext: DbContext
{

    /// <inheritdoc />
    protected GisVectorCoreManyRepository(ILogger logger, TContext context, IMapper mapper) : base(logger, context, mapper)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<TModel> ApplyIncludes(DbSet<TModel> table) 
        => table.Include(x => x.PropertiesCollection);
}