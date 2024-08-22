using AutoMapper;
using Gis.Net.Core.Exceptions;
using Gis.Net.Core.Repositories;
using Gis.Net.Vector.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.Repositories;

/// <inheritdoc />
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
        if (options.QueryParams?.SrCode is null)
            throw new Exception("Per creare una feature è obbligatorio il parametro SrCode");
                
        var feature = GisUtility.CreateEmptyFeature((int)options.QueryParams.SrCode, geom);

        feature.Geometry = options.QueryParams.Buffer is not null 
            ? GisUtility.BufferGeometry(dto.Geom!, (double)options.QueryParams.Buffer) 
            : geom;

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
        if (queryByParams?.Ids is not null)
            query = query.Where(x => queryByParams.Ids.Contains(x.Id));

        return base.ParseQueryParams(query, queryByParams);
    }

    /// <inheritdoc />
    public async Task<FeatureCollection> GetRows(GisOptionsGetRows<TModel, TDto, TQuery> options)
    {
        var features = await GetFeatures(options);
        return GisUtility.CreateFeatureCollection(features!);
    }

    /// <inheritdoc />
    public virtual async Task<FeatureCollection> Find(long id, GisOptionsGetRows<TModel, TDto, TQuery> options)
    {
        var item = await base.Find(id);
        
        if (item is null)
            throw new NotFoundException("I couldn't find the item");
        
        var feature = await DtoToFeature(item, options);
        if (feature is null)
            throw new ApplicationException("There are problems creating the geometric feature");
        
        return GisUtility.CreateFeatureCollection(new List<IFeature> { feature });   
    }

    private async Task<IFeature?> DtoToFeature(TDto dto, GisOptionsGetRows<TModel, TDto, TQuery> options) 
    {
        var feature = await CreateGeometry(dto, options);
        
        GisUtility.AddProperty(ref feature, "Id", dto.Id.ToString());

        if (options.QueryParams is { Measure: true, Buffer: > 0 })
            if (options.QueryParams.GisGeometry?.Geom is null)
                throw new Exception("Errore nel calcolo delle misure");
            else 
                GisUtility.CalculateMeasure(ref feature, options.QueryParams.GisGeometry.Geom);

        // delete null values
        GisUtility.DeleteNullProperties(ref feature);

        if (options.OnCreatedFeature is not null)
            await options.OnCreatedFeature.Invoke(feature);

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
        foreach (var f in collection)
        {
            var feature = await DtoToFeature(f, options);
            if (feature is not null)
                features.Add(feature);
        }

        return features;
    }
}