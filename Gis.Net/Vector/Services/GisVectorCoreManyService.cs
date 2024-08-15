using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Gis.Net.Vector.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Vector.Services;

/// <inheritdoc />
public abstract class GisVectorCoreManyService<TModel, TDto, TQuery, TRequest, TContext, TPropertiesModel, TPropertiesDto>: 
    GisCoreService<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisVectorManyDto<TPropertiesDto>
    where TModel : GisCoreManyModel<TPropertiesModel>
    where TQuery : GisVectorQuery, new()
    where TPropertiesModel: ModelBase
    where TPropertiesDto: DtoBase
    where TRequest : GisRequest
    where TContext : DbContext
{
    /// <inheritdoc />
    protected GisVectorCoreManyService(
        ILogger logger,
        IGisCoreRepository<TModel, TDto, TQuery, TContext> netCoreRepository) :
        base(logger, netCoreRepository)
    { }
    
}