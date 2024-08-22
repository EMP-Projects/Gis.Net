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
    /// <typeparam name="TModel">The type of the GIS core model.</typeparam>
    /// <typeparam name="TDto">The type of the GIS vector DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query object for querying GIS vectors.</typeparam>
    /// <typeparam name="TRequest">The type of the request object for creating or updating GIS vectors.</typeparam>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    /// <typeparam name="TPropertiesModel">The type of the properties model for GIS vectors.</typeparam>
    /// <typeparam name="TPropertiesDto">The type of the properties DTO for GIS vectors.</typeparam>
    protected GisRootVectorManyController(ILogger logger,
        IConfiguration configuration,
        IMapper mapper,
        IGisCoreService<TModel, TDto, TQuery, TRequest, TContext> service) :
        base(logger, configuration, mapper, service)
    {
        
    }
    
}