using AutoMapper;
using Gis.Net.Core.Exceptions;
using Gis.Net.Core.Repositories;
using Gis.Net.Vector.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.Repositories;

/// <inheritdoc cref="Gis.Net.Vector.Repositories.IGisCoreRepository<TModel,TDto,TQuery,out TContext>" />
public abstract class GisCoreRepository<TModel, TDto, TQuery, TContext> : 
    RepositoryCore<TModel, TDto, TQuery, TContext>,
    IGisCoreRepository<TModel, TDto, TQuery, TContext>
    where TDto: GisVectorDto
    where TModel: Models.VectorModel
    where TQuery: GisVectorQuery, new()
    where TContext : DbContext
{
    /// <inheritdoc />
    protected GisCoreRepository(ILogger logger, TContext context, IMapper mapper) : 
        base(logger, context, mapper)
    {
        
    }

    /// <summary>
    /// Creates a geometry feature for the given DTO and options.
    /// </summary>
    /// <param name="dto">The DTO containing the geometry information.</param>
    /// <param name="options">The options for creating the geometry feature.</param>
    /// <returns>The created geometry feature.</returns>
    protected virtual async Task<Feature> CreateGeometry(TDto dto, GisOptionsGetRows<TModel, TDto, TQuery> options)
        =>  await CreateFeatureFromGeometry(dto, dto.Geom!, options);

    /// <summary>
    /// Creates a feature from a geometry.
    /// </summary>
    /// <param name="dto">The DTO object containing the geometry information.</param>
    /// <param name="geom">The geometry object.</param>
    /// <param name="options">The options for creating the feature.</param>
    /// <returns>The created feature.</returns>
    protected virtual async Task<Feature> CreateFeatureFromGeometry(TDto dto, Geometry geom, GisOptionsGetRows<TModel, TDto, TQuery> options)
    {
        // If the SrCode parameter is not specified, throw an exception.
        if (options.QueryParams?.SrCode is null)
            throw new Exception("To create a feature the SrCode parameter is mandatory");
              
        // Create an empty feature with the specified spatial reference code and geometry.
        var feature = GisUtility.CreateEmptyFeature((int)options.QueryParams.SrCode, geom);

        // If the buffer parameter is not null, apply the buffer to the geometry.
        feature.Geometry = options.QueryParams.Buffer is not null 
            ? GisUtility.BufferGeometry(dto.Geom!, (double)options.QueryParams.Buffer) 
            : geom;

        // Add the properties to the feature.
        return await Task.FromResult(feature);
    }

    /// <summary>
    /// Parses the query parameters and applies them to the IQueryable query.
    /// </summary>
    /// <param name="query">The original IQueryable query.</param>
    /// <param name="queryByParams">The query parameters to apply.</param>
    /// <returns>The modified IQueryable query with the applied query parameters.</returns>
    protected override IQueryable<TModel> ParseQueryParams(IQueryable<TModel> query, TQuery? queryByParams)
    {
        // If the Ids property is not null, apply the appropriate filter to the query.
        if (queryByParams?.Ids is not null)
            query = query.Where(x => queryByParams.Ids.Contains(x.Id));

        // If the GisGeometry property is not null, apply the appropriate spatial filter to the query.
        if (queryByParams?.GisGeometry is null) return base.ParseQueryParams(query, queryByParams);
        
        // Apply the appropriate spatial filter based on the geometry type.
        if (queryByParams.GisGeometry.IsPolygon)
            query = query.Where(x => x.Geom.Intersects(queryByParams.GisGeometry.Geom));
        else if (queryByParams.GisGeometry.IsPoint)
            query = query.Where(x => x.Geom.IsWithinDistance(queryByParams.GisGeometry.Geom, queryByParams.Distance ?? 100));
        else if (queryByParams.GisGeometry.IsLine)
            query = query.Where(x => x.Geom.Touches(queryByParams.GisGeometry.Geom));

        // Return the modified query.
        return base.ParseQueryParams(query, queryByParams);
    }

    /// <inheritdoc />
    public async Task<FeatureCollection> GetRows(GisOptionsGetRows<TModel, TDto, TQuery> options)
    {
        // Get the features based on the specified options.
        var features = await GetFeatures(options);
        
        // If the features are null, throw an exception.
        return GisUtility.CreateFeatureCollection(features!);
    }

    /// <inheritdoc />
    public virtual async Task<FeatureCollection> Find(long id, GisOptionsGetRows<TModel, TDto, TQuery> options)
    {
        // Find the item with the specified ID.
        var item = await base.Find(id);
        
        // If the item is null, throw a NotFoundException.
        if (item is null)
            throw new NotFoundException("I couldn't find the item");
        
        // Create a feature from the item and options.
        var feature = await DtoToFeature(item, options);
        if (feature is null)
            throw new ApplicationException("There are problems creating the geometric feature");
        
        // Return the feature collection.
        return GisUtility.CreateFeatureCollection([feature]);   
    }

    private async Task<IFeature?> DtoToFeature(TDto dto, GisOptionsGetRows<TModel, TDto, TQuery> options) 
    {
        var feature = await CreateGeometry(dto, options);
        
        GisUtility.AddProperty(ref feature, "Id", dto.Id.ToString());

        if (options.QueryParams is { Measure: true, Buffer: > 0 })
            if (options.QueryParams.GisGeometry?.Geom is null)
                throw new Exception("Error in the calculation of the measurements, the geometry is not defined");
            else 
                GisUtility.CalculateMeasure(ref feature, options.QueryParams.GisGeometry.Geom);

        // delete null values
        GisUtility.DeleteNullProperties(ref feature);

        // if the OnCreatedFeature event is not null, invoke it
        if (options.OnCreatedFeature is not null)
            await options.OnCreatedFeature.Invoke(feature);

        // if the OnLoadProperties event is not null, invoke it
        if (options.OnLoadProperties is not null)
            await options.OnLoadProperties.Invoke(feature, dto);
                
        return feature;
    }

    /// <inheritdoc />
    public virtual async Task<List<IFeature>> GetFeatures(GisOptionsGetRows<TModel, TDto, TQuery> options)
    {
        // return collection Dto
        var collection = await base.GetRows(options);
        
        // transform collection Dto to FeatureCollection
        var features = new List<IFeature>();
        
        // for each Dto in collection
        foreach (var f in collection)
        {
            var feature = await DtoToFeature(f, options);
            if (feature is not null)
                features.Add(feature);
        }

        return features;
    }
}