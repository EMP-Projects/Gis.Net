using Gis.Net.Core.Services;
using Gis.Net.Vector;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Repositories;
using Gis.Net.Vector.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Esri;

namespace Gis.Net.VectorCore.Services;

/// <inheritdoc cref="IGisCoreService{TModel,TDto,TQuery,TRequest,TContext}<TDto,TModel,TQuery>" />
public abstract class GisCoreService<TModel, TDto, TQuery, TRequest, TContext>: 
    ServiceCore<TModel, TDto, TQuery, TRequest, TContext>, 
    IGisCoreService<TModel, TDto, TQuery, TRequest, TContext>
    where TDto : GisVectorDto
    where TModel : Vector.Models.VectorModel
    where TQuery : GisVectorQuery, new()
    where TRequest : GisRequest
    where TContext : DbContext
{
    /// <inheritdoc />
    protected GisCoreService(ILogger logger, 
                             IGisCoreRepository<TModel, TDto, TQuery, TContext> repository) : 
        base(logger, repository)
    {
    }

    /// <inheritdoc />
    public override Task Validate(TDto dto, ECrudActions crudEnum)
    {
        if (dto.Key is null)
            throw new Exception("E' necessario specificare almeno il parametro [Key]");
        
        if (dto.Geom is null)
            throw new Exception("[Geom] is required");
        
        return Task.CompletedTask;
    }
    
    protected virtual async Task<ICollection<TModel>> ParseQueryParamsGeometry(ICollection<TModel> models, TQuery? queryByParams)
    {
        if (queryByParams is null)
            return models;
        
        if (queryByParams.SrCode is null)
            throw new Exception("E' necessario specificare almeno il parametro srCode");
        
        if (queryByParams.GisGeometry is not null)
            models = models.Where(x => queryByParams.GisGeometry & new GisGeometry(queryByParams.SrCode.Value, x.Geom)).ToList();
        
        return await Task.FromResult(models);
    }

    /// <summary>
    /// Asynchronously loads properties into a <see cref="Feature"/> object from a data transfer object (DTO).
    /// </summary>
    /// <param name="feature">The <see cref="Feature"/> object to which properties are to be added.</param>
    /// <param name="dto">The DTO that contains the properties to be loaded into the <see cref="Feature"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a <see cref="Feature"/> that has had properties loaded.</returns>
    protected virtual Task<Feature> OnLoadProperties(Feature feature, TDto dto)
    {
        feature.Attributes.Add(NameProperties, dto);
        return Task.FromResult(feature);
    }

    /// <summary>
    /// Permette di aggiungere un array di Id delle proprieta nella query
    /// per selezionare solo le features con le proprietà contenute nell'array
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    protected virtual Task<long[]?> QueryParamsByProperties(TQuery query) => Task.FromResult<long[]?>(null);
    
    /// <summary>
    /// Ordina le fetaures in base ai criteri applicati ai parametri geografici della query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="queryByParams"></param>
    /// <returns></returns>
    /// <exception cref="GisExceptions"></exception>
    protected virtual IQueryable<TModel> OnSortParams(IQueryable<TModel> query, TQuery? queryByParams)
    {
        if (queryByParams?.GisGeometry is null)
            return query;
        
        if (queryByParams.GisGeometry.IsPoint)
            query = query.OrderBy(x => x.Geom.Distance(queryByParams.GisGeometry.Geom));
        else if (queryByParams.GisGeometry.IsLine)
            query = query.OrderBy(x => x.Geom.Distance(queryByParams.GisGeometry.Geom)).ThenByDescending(x => x.Geom.Length);
        else if (queryByParams.GisGeometry.IsPolygon)
            query = query.OrderBy(x => x.Geom.Distance(queryByParams.GisGeometry.Geom)).ThenByDescending(x => x.Geom.Area);
        
        return query;

    }
    
    /// <inheritdoc />
    protected virtual async Task<TDto> UploadFile(TDto dto, string path)
    {
        var filePath = dto.UrlFile is not null 
            ? await GisFiles.UnzipShapeFileAndSave(dto.UrlFile!, path, true, dto.UploadBody)
            : await GisFiles.UnzipShapeFileAndSave(dto.File!, path, true);

        if (filePath is null)
            throw new Exception("I couldn't read the vector file");
        
        // the first geometry of the vector file
        var geom = Shapefile.ReadAllGeometries(filePath).FirstOrDefault();
        if (geom is null)
            throw new Exception("I was unable to read the features of the vector file");
        dto.Geom = geom;
        GisFiles.DeleteFile(filePath);
        return dto;
    }

    protected virtual IQueryable<TModel> BeforeQuery(IQueryable<TModel> query) => query; 
    protected virtual IQueryable<TModel> SortQuery(IQueryable<TModel> query) => query; 
    
    /// <inheritdoc />
    public virtual async Task<FeatureCollection?> FeatureCollection(TQuery? queryParams = null)
    {
        queryParams ??= new TQuery();
        queryParams.PropertyIds = await QueryParamsByProperties(queryParams);
        
        var options = new GisOptionsGetRows<TModel, TDto, TQuery>(queryParams)
        {
            OnBeforeQuery = BeforeQuery,
            OnSort = SortQuery,
            OnSortParams = OnSortParams,
            OnLoadProperties = OnLoadProperties,
            OnAfterQueryParams = ParseQueryParamsGeometry
        };

        var repo = GetRepository();
        
        if (repo is IGisCoreRepository<TModel, TDto, TQuery, TContext> gisRepository)
            return await gisRepository.GetRows(options);
        
        return null;
    }

    /// <inheritdoc />
    public virtual async Task<List<TModel>> Delete(TQuery queryParams)
    {
        queryParams.PropertyIds = await QueryParamsByProperties(queryParams);
        var collectionDto = await GetRepository().GetRows(GetRowsOptions(queryParams));

        var listModels = new List<TModel>();
        foreach (var dto in collectionDto)
            listModels.Add(await base.Delete(dto.Id));

        if (listModels is null || listModels.Count == 0)
            throw new Exception("Non ho cancellato nessuna geometria");
        
        return listModels;
    }

    private static string PathUploads() 
        => GisFiles.CreateUploadsFolderToSave("uploads", null);

    /// <inheritdoc />
    public override async Task<TModel?> Insert(TDto dto)
    {
        if (dto is null)
            throw new Exception("dto is null");
        return await base.Insert(dto);
    }

    /// <inheritdoc />
    public virtual async Task AddModel(TModel model) => await GetRepository().GetDbContext().AddAsync(model);

    /// <inheritdoc />
    public virtual async Task AddModels(IEnumerable<TModel> models) => await GetRepository().GetDbContext().AddRangeAsync(models);

    public virtual string? NameProperties { get; set; } = "data";
    
    public virtual async Task<TModel?> Upload(TDto dto)
    {
        if (dto is null)
            throw new Exception("dto is null");

        var path = PathUploads();
        var newDto = await UploadFile(dto, path);
        return await base.Insert(newDto);
    }

    private async Task<Geometry?> AggregateGeometry(TQuery query)
    {
        var features = await GetRepository().GetRows(new GisOptionsGetRows<TModel, TDto, TQuery>(query));
        return features.Select(f => f.Geom).Aggregate((g1, g2) => g1?.Union(g2));
    }
    
    /// <summary>
    /// Get Center Features 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public virtual async Task<double[]> Center(TQuery query)
    {
        var geom = await AggregateGeometry(query);
        if (geom is null) return [];
        var centroid = geom.Centroid;
        return [centroid.X, centroid.Y];
    }
    
    /// <summary>
    /// Get Extent Features
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public virtual async Task<double[]> Extent(TQuery query)
    {
        var geom = await AggregateGeometry(query);
        if (geom is null) return [];
        var extent = geom.EnvelopeInternal;
        return [extent.MinX, extent.MinY, extent.MaxX, extent.MaxY];
    }
}