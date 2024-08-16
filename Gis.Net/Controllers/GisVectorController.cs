using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Gis.Net.Vector.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

/// <inheritdoc />
public abstract class GisVectorController<TModel, TDto, TQuery, TRequest, TContext, TPropertiesModel, TPropertiesDto> : 
    GisController<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisVectorDto<TPropertiesDto>
    where TModel : GisCoreModel<TPropertiesModel>
    where TQuery : GisVectorQuery
    where TPropertiesDto: DtoBase
    where TPropertiesModel: ModelBase
    where TRequest : GisVectorRequest
    where TContext : DbContext
{
    
    /// <inheritdoc />
    protected GisVectorController(ILogger logger,
        IConfiguration configuration,
        IMapper mapper,
        IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> service) :
        base(logger, configuration, mapper, service)
    {
        
    }
}