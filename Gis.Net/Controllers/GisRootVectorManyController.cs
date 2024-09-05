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

/// <summary>
/// Base abstract class for controllers that handle CRUD operations for vector entities with many properties.
/// </summary>
/// <typeparam name="TModel">The model type.</typeparam>
/// <typeparam name="TDto">The DTO type.</typeparam>
/// <typeparam name="TQuery">The query type.</typeparam>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TContext">The database context type.</typeparam>
/// <typeparam name="TPropertiesModel">The model properties type.</typeparam>
/// <typeparam name="TPropertiesDto">The DTO properties type.</typeparam>
public abstract class GisRootVectorManyController<TModel, TDto, TQuery, TRequest, TContext, TPropertiesModel, TPropertiesDto> : 
    GisRootController<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisVectorManyDto<TPropertiesDto>
    where TModel : GisCoreManyModel<TPropertiesModel>
    where TQuery : GisVectorQuery
    where TPropertiesDto: DtoBase
    where TPropertiesModel: ModelBase
    where TRequest : GisVectorRequest
    where TContext : DbContext
{

    /// <summary>
    /// Represents an abstract controller for interacting with multiple GIS vector resources.
    /// </summary>
    protected GisRootVectorManyController(ILogger logger,
        IConfiguration configuration,
        IMapper mapper,
        IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> service) :
        base(logger, configuration, mapper, service)
    {
        
    }
    
}