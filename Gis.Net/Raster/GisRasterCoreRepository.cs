using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Vector;
using Gis.Net.Vector.DTO;
using Gis.Net.Vector.Models;
using Gis.Net.Vector.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.Raster;

/// <inheritdoc />
public abstract class GisRasterCoreRepository<TModel, TDto, TQuery, TContext, TPropertiesModel, TPropertiesDto> : 
    GisCoreRepository<TModel, TDto, TQuery, TContext>,
    IGisRasterCoreRepository<TModel, TDto, TPropertiesModel, TPropertiesDto>
    where TDto : GisVectorDto<TPropertiesDto>, new()
    where TModel : GisCoreModel<TPropertiesModel>, new()
    where TQuery : GisVectorQuery, new()
    where TPropertiesModel: ModelBase
    where TPropertiesDto: DtoBase
    where TContext : DbContext
{
    /// <inheritdoc />
    protected GisRasterCoreRepository(ILogger logger, TContext context, IMapper mapper) : 
        base(logger, context, mapper)
    {
        
    }

    /// <inheritdoc />
    public async Task<string> UploadRaster(IRasterUploadDto options)
    {
        var filePath = await GisFiles.UploadTiffFileAndSave(options.Url, options.Path, options.Replace ?? true, options.Body);
        if (filePath is null)
            throw new Exception("Non sono riuscito a leggere il file raster");

        return filePath;
    }

    /// <summary>
    /// Asynchronously inserts raw data into the database using the specified DTO and file path.
    /// </summary>
    /// <param name="dto">The data transfer object containing the data to be inserted.</param>
    /// <param name="path">The file path to the binary file containing the raw data.</param>
    /// <returns>
    /// A task that represents the asynchronous insert operation. The task result contains the inserted model object if successful, or null if not.
    /// </returns>
    public virtual async Task<int> InsertRaw(TDto dto, string path)
    {
        try
        {
            var ts = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm");
            // Constructs the SQL insert statement using parameters from the DTO and the given file path.
            var sql = $"INSERT INTO {Schema}.{Table} (guid, \"key\", \"timestamp\", rast) " +
                           $"VALUES ('{dto.Guid}', '{dto.EntityKey}', '{ts}', " +
                           $"ST_FromGDALRaster(pg_read_binary_file('{path}')))";

            return await GetDbContext().Database.ExecuteSqlRawAsync(sql);
        }
        catch (Exception ex)
        {
            // Logs the exception message using the Logger.
            Logger.LogError(ex.Message);
            Console.WriteLine(ex.Message);
            return await Task.FromResult(-1);
        }
    }
    
    private string GetSqlIntersects(Geometry geom, double buffer)
    {
        var sql = $"SELECT id, key, guid, timestamp, " +
                  $"ST_Polygon(ST_Union(ST_Clip(ST_SetSRID(r.rast, {(int)ESrCode.WebMercator}), ST_SetSRID(g.geom, {(int)ESrCode.WebMercator}))))::geometry as geom " +
                  $"FROM {Schema}.{Table} as r " +
                  $"INNER JOIN ST_Buffer('{geom.AsText()}', {buffer}) as g(geom) " +
                  $"ON ST_Intersects(ST_SetSRID(r.rast, {(int)ESrCode.WebMercator}), ST_SetSRID(g.geom, {(int)ESrCode.WebMercator}))";
        return sql;
    }
    
    /// <summary>
    /// Finds all entities of type T that intersect with the specified point and buffer.
    /// </summary>
    /// <param name="geom">The point geometry to test for intersection.</param>
    /// <param name="buffer">The buffer distance around the point geometry.</param>
    /// <returns>A list of entities of type T that intersect with the specified geometry.</returns>
    public async Task<List<TModel>> Intersects(Point geom, double buffer)
    {
        var sql = GetSqlIntersects(geom, buffer);
        return await GetDbContext().Set<TModel>()
            .FromSqlRaw(sql)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Finds all entities of type T that intersect with the specified geometry.
    /// </summary>
    /// <param name="geom">The geometry to test for intersection.</param>
    /// <returns>A list of entities of type T that intersect with the specified geometry, or null if none are found.</returns>
    public virtual async Task<List<TModel>?> Intersects(Geometry geom) 
    {
        try
        {
            var sql =
                $"SELECT id, key, guid, timestamp, ST_Polygon(ST_Union(ST_Clip(ST_SetSRID(r.rast, {(int)ESrCode.WebMercator}), ST_SetSRID(g.geom, {(int)ESrCode.WebMercator}))))::geometry as geom " +
                $"FROM {Schema}.{Table} as r " +
                $"INNER JOIN '{geom.AsText()}' as g(geom) " +
                $"ON ST_Intersects(ST_SetSRID(r.rast, {(int)ESrCode.WebMercator}), ST_SetSRID(g.geom, {(int)ESrCode.WebMercator})) " +
                $"GROUP BY r.id";
            
            return await GetDbContext().Set<TModel>()
                .FromSqlRaw(sql)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            // Logs the exception message using the Logger.
            Logger.LogError(ex.Message);
            Console.WriteLine(ex.Message);
            return await Task.FromResult<List<TModel>?>(null);
        }
    }
    
    /// <summary>
    /// Finds all entities of type T that intersect with the specified point and buffer, filtered by a timestamp.
    /// </summary>
    /// <param name="geom">The point geometry to test for intersection.</param>
    /// <param name="buffer">The buffer distance around the point geometry.</param>
    /// <param name="timestamp">The timestamp to filter the results.</param>
    /// <returns>A list of entities of type T that intersect with the specified geometry and match the timestamp filter.</returns>
    public virtual async Task<List<TModel>?> Intersects(Point geom, double buffer, DateTime timestamp)
    {
        try
        {
            var sql = GetSqlIntersects(geom, buffer);
            var startTs = timestamp.Date.ToString("yyyy-MM-dd HH:mm");
            var endTs = timestamp.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm");
            sql += $" WHERE timestamp BETWEEN '{startTs}' and '{endTs}' GROUP BY r.id";
            return await GetDbContext().Set<TModel>().FromSqlRaw(sql).AsNoTracking().ToListAsync()!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Logs the exception message using the Logger.
            Logger.LogError(ex.Message);
            return await Task.FromResult<List<TModel>?>(null);
        }
    }

    /// <summary>
    /// Abstract class representing a repository for managing raster data in a GIS system.
    /// </summary>
    protected abstract string Table { get; set; }

    /// <summary>
    /// Represents a base repository for GIS raster data.
    /// </summary>
    protected abstract string Schema { get; set; }
    
    private async Task<TModel?> ConvertRasterToPolygon(long id) 
    {
        var sql = $"SELECT ST_Union(gv.geom) AS geom" +
                  $"FROM {Schema}.{Table} as r, " +
                  $"ST_DumpAsPolygons(raster, 2) AS gv " +
                  $"WHERE id = {id}";
        
        return await GetDbContext().Set<TModel>().FromSqlRaw(sql).AsNoTracking().FirstOrDefaultAsync();
    }

    /// <summary>
    /// Selects raster data based on a model and a geometry for comparison.
    /// </summary>
    /// <param name="model">The model representing the raster data.</param>
    /// <param name="geomByParams">The geometry to compare with the raster data.</param>
    /// <returns>Returns a boolean value representing whether there is a match or not.</returns>
    public async Task<bool> WhereRaster(TModel model, GisGeometry geomByParams)
    {
        if (geomByParams.Geom is null)
            throw new Exception("Geometria per il confronto con il raster NULL");
        
        var sql = $"SELECT id, key, guid, timestamp, ST_Polygon(ST_Union(ST_Clip(r.raster, g.geom))) as geom " +
                  $"FROM {Schema}.{Table} AS r " +
                  $"WHERE id = {model.Id} " +
                  $"INNER JOIN {geomByParams.Geom.AsText()} AS g(geom) " +
                  $"ON ${GisUtility.SqlFunctionSpatialByGeometry(geomByParams.Geom)}(r.raster, g.geom)";
        
        var m = await GetDbContext().Set<TModel>().FromSqlRaw(sql).AsNoTracking().FirstOrDefaultAsync();
        if (m?.Geom is not null) return geomByParams & new GisGeometry(geomByParams.SrCode, m.Geom);
        
        return false;
    }

    /// <summary>
    /// Creates a <see cref="Feature"/> object with geometry from the provided <paramref name="dto"/> and <paramref name="options"/>.
    /// </summary>
    /// <param name="dto">The DTO object containing the required information.</param>
    /// <param name="options">The options for creating the feature.</param>
    /// <returns>A <see cref="Feature"/> object with the created geometry.</returns>
    protected override async Task<Feature> CreateGeometry(TDto dto, GisOptionsGetRows<TModel, TDto, TQuery> options) 
    {
        // If the geometry is null, throw an exception.
        var geom = (await ConvertRasterToPolygon(dto.Id))?.Geom;
        
        // If the geometry is null, throw an exception.
        if (geom is null)
            throw new Exception("I was unable to convert the raster to polygon");
        
        return await CreateFeatureFromGeometry(geom, options);
    }
}