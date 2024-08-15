using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Services;
using Gis.Net.Raster.Models;
using Gis.Net.Vector;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Gis.Net.Vector.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace Gis.Net.Raster;

/// <summary>
/// Provides abstract methods for service operations on GIS raster data.
/// </summary>
/// <typeparam name="TDto">The type of the data transfer object for rasters, must inherit from GisRasterCoreDto and be instantiable.</typeparam>
/// <typeparam name="TModel">The type of the raster model, must inherit from GisRasterCoreModel.</typeparam>
/// <typeparam name="TQuery">The type of the query object for core GIS operations, must inherit from GisCoreQueryByParams.</typeparam>
/// <typeparam name="TPropertiesModel"></typeparam>
/// <typeparam name="TPropertiesDto"></typeparam>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TContext"></typeparam>
public abstract class GisRasterCoreService<TModel, TDto, TQuery, TRequest, TContext, TPropertiesModel, TPropertiesDto>: 
    GisCoreService<TModel, TDto, TQuery, TRequest, TContext>,
    IGisRasterCoreService<TModel>
    where TDto : GisVectorDto<TPropertiesDto>, IGisRasterUpload, IGisRaster, new()
    where TModel : GisCoreModel<TPropertiesModel>, IGisCoreManyModels<TPropertiesModel>, new()
    where TQuery : GisVectorQuery, new()
    where TPropertiesModel: ModelBase
    where TPropertiesDto: DtoBase
    where TRequest : GisRequest
    where TContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the GisRasterCoreService class with the specified logger, repository, and authentication service.
    /// </summary>
    /// <param name="logger">The logger for logging messages.</param>
    /// <param name="repository">The repository for data access operations.</param>
    /// <param name="authService">The authentication service for user authentication and authorization.</param>
    protected GisRasterCoreService(ILogger<GisRasterCoreService<TModel, TDto, TQuery, TRequest, TContext, TPropertiesModel, TPropertiesDto>> logger,
                                   GisRasterCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto> repository) : 
        base(logger, repository)
    {
    }

    private const string UploadPath = "uploads";

    /// <summary>
    /// Asynchronously uploads a raster file and returns the DTO associated with it.
    /// </summary>
    /// <param name="dto">The data transfer object representing the raster to upload.</param>
    /// <param name="path">The file path of the raster to upload.</param>
    /// <returns>A task representing the asynchronous operation, containing the uploaded raster DTO.</returns>
    protected override Task<TDto> UploadFile(TDto dto, string path) 
        => Task.FromResult(dto);
    
    /// <summary>
    /// Asynchronously inserts a raster DTO into the repository.
    /// </summary>
    /// <param name="dto">The raster DTO to insert.</param>
    /// <returns>A task representing the asynchronous operation, containing the inserted raster model or null if the insertion fails.</returns>
    /// <exception cref="DtoNullException">Thrown when the provided dto is null.</exception>
    public override async Task<TModel?> Insert(TDto dto)
    {
        if (dto is null)
            throw new Exception("dto is null");

        if (dto.PathFileRaster is null) 
            return await base.Insert(dto);

        if (GetRepository() is not GisRasterCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto>
            rasterCoreRepository) return null;
        
        await rasterCoreRepository.InsertRaw(dto, dto.PathFileRaster);
        GisFiles.DeleteFile(dto.PathFileRaster);

        return null;
    }

    private static TDto CreateDtoFromRasterFile(string rasterFile, string subDir) => new()
    {
        Guid = Guid.NewGuid(),
        Key = subDir,
        PathFileRaster = $"/var/lib/postgresql/uploads/{subDir}/{Path.GetFileName(rasterFile)}",
        TimeStamp = DateTime.UtcNow
    };

    public virtual Task DeleteRasterFiles(List<string>? subDirectories)
    {
        var path = GisFiles.GetUploadFolderRaster();
        var subdirectoryEntries = Directory.GetDirectories(path).ToList();
        if (subDirectories is not null || subDirectories?.Count > 0)
            subdirectoryEntries = subdirectoryEntries.Where(subDir 
                => subDirectories.Contains(new DirectoryInfo(subDir).Name)).ToList();
        foreach (var file in from subdirectory 
                     in subdirectoryEntries 
                 let subDir = new DirectoryInfo(subdirectory).Name 
                 select Directory.GetFiles(subdirectory).Where(GisFiles.IsTiff) 
                 into files 
                 from file in files 
                 select file)
            GisFiles.DeleteFile(file);

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual async Task InsertRaster(List<string>? subDirectories)
    {
        var path = GisFiles.CreateUploadsFolderRaster(UploadPath, null);
        
        // leggo tutte le sotto directory in cui sono contenuti i file raster
        var subdirectoryEntries = Directory.GetDirectories(path).ToList();

        if (subDirectories is not null || subDirectories?.Count > 0)
            subdirectoryEntries = subdirectoryEntries.Where(subDir 
                => subDirectories.Contains(new DirectoryInfo(subDir).Name)).ToList();

        var listDto = new List<TDto>();
        foreach (var subdirectory in subdirectoryEntries)
        {
            var subDir = new DirectoryInfo(subdirectory).Name;
            var files = Directory.GetFiles(subdirectory).Where(GisFiles.IsTiff);
            listDto.AddRange(files.Select(file => CreateDtoFromRasterFile(file, subDir)));
        }

        // Avvia un'attività asincrona utilizzando Task.Factory.StartNew
        // https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
        await Task.Factory
            .StartNew(
                () =>
                {
                    // Inizializza un contatore d'indice
                    var index = 0;
                    // Itera attraverso ogni elemento nella lista 'listDto'
                    foreach (var dto in listDto)
                        // Per ogni elemento, avvia un'ulteriore attività asincrona
                        Task.Factory.StartNew(async value 
                                => await Insert(dto), // Chiama il metodo 'Insert' asincrono passando l'elemento corrente 'dto'
                            index++, // Incrementa l'indice per ogni attività avviata
                            TaskCreationOptions.AttachedToParent); 
                    // L'opzione assicura che l'attività figlia sia collegata all'attività genitore
                }).ContinueWith(
                antecedent =>
                    Console.WriteLine($"Executing continuation of Task [InsertRaster] {antecedent.Id}"));
    }

    /// <inheritdoc />
    public virtual async Task<List<TModel>?> Intersects(Geometry geom)
    {
        if (GetRepository() is GisRasterCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto> rasterRepository)
            return await rasterRepository.Intersects(geom);
        return null;
    }

    /// <inheritdoc />
    public virtual async Task<List<TModel>?> Intersects(Point geom, double buffer)
    {
        var repo = GetRepository();
        if (repo is GisRasterCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto> rasterRepository)
            return await rasterRepository.Intersects(geom, buffer);
        return null;
    }

    /// <inheritdoc />
    public override async Task Validate(TDto dto, ECrudActions crudEnum)
    {
        if (dto.Raster is null)
            throw new Exception("[Raster] is required");
        await base.Validate(dto, crudEnum);
    }

    protected override async Task<ICollection<TModel>> ParseQueryParamsGeometry(ICollection<TModel> models, TQuery? queryByParams)
    {
        if (GetRepository() is not GisRasterCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto> rasterRepository)
            return await Task.FromResult(models);
        
        if (queryByParams is null)
            return await Task.FromResult(models);
        
        if (queryByParams.GisGeometry is not null)
            models = models.Where(x => rasterRepository.WhereRaster(x, queryByParams.GisGeometry).Result).ToList();
        
        return await Task.FromResult(models);
    }
}