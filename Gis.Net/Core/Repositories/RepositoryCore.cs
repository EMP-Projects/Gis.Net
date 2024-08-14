using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Repositories;

/// <inheritdoc />
public abstract class RepositoryCore<TModel, TDto, TQuery, TContext> :
    IRepositoryCore<TModel, TDto, TQuery, TContext>
    where TDto : DtoBase
    where TModel : ModelBase
    where TQuery : QueryBase
    where TContext : DbContext
{
    /// <summary>
    /// The DbContext instance representing the session with the database, allowing for CRUD operations.
    /// </summary>
    private readonly TContext _context;

    /// <summary>
    /// The ILogger instance used for logging messages or exceptions that might occur during the data access operations.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// The IMapper instance used to map objects from one type to another, facilitating data transfer between layers.
    /// </summary>
    private readonly IMapper _mapper;

    protected RepositoryCore(ILogger logger, TContext context, IMapper mapper)
    {
        Logger = logger;
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns the database context
    /// </summary>
    /// <returns></returns>
    public TContext GetDbContext() => _context;

    /// <inheritdoc />
    public EntityEntry<TModel> Entry(TModel model) => _context.Entry(model);

    /// <summary>
    /// Salva le modifiche sul database
    /// </summary>
    /// <returns></returns>
    public async Task<int> SaveChanges() => await _context.SaveChangesAsync();

    protected virtual IQueryable<TModel> ParseQueryParams(IQueryable<TModel> query, TQuery? queryByParams)
    {
        if (queryByParams?.Id is not null)
            query = query.Where(x => x.Id == queryByParams.Id);
        
        if (queryByParams?.Key is not null)
            query = query.Where(x => x.Key == queryByParams.Key);
        
        if (queryByParams?.Search is not null)
            query = query.Where(x => x.SearchText != null && x.SearchText.Matches(queryByParams.Search));
        
        return query;
    }
    
    /// <inheritdoc />
    public async Task<ICollection<TDto>> GetRows(ListOptions<TModel, TDto, TQuery> options)
    {
        // se la cache non ha valori memorizzati leggo i records dal database
        var q = ApplyIncludes(_context.Set<TModel>()).AsNoTracking();
        
        // controllo dei parametri obbligatori
        if (!ValidateParameters(options.QueryParams!))
            throw new Exception("Parametri obbligatori mancanti");

        // query parametrizzata
        q = ParseQueryParams(q, options.QueryParams);

        // esegue delegate prima dell'inserimento
        if (options.OnBeforeQuery is not null)
            q = options.OnBeforeQuery.Invoke(q);

        // ordina elementi
        if (options.OnSort is not null)
            q = options.OnSort(q);
        
        // ordina elementi in base ai parametri della query
        // Attenzione! Questo ordinamento sostituisce OnSort
        if (options.OnSortParams is not null)
            q = options.OnSortParams(q, options.QueryParams);
        
        var rows = await q.ToListAsync();
        
        // il metodo delegato esegue un ulteriore query sulla lista dei modelli
        // in questo punto è possibile utilizzare metodi che non sono supportati dal database
        // ma è necessario prima eseguire la query e lavorare sulla lista di oggetti
        // Info: https://learn.microsoft.com/it-it/ef/core/querying/client-eval#explicit-client-evaluation
        if (options.OnAfterQueryParams is not null)
            await options.OnAfterQueryParams.Invoke(rows, options.QueryParams);
        
        return await GetRows(rows, options);
        
    }

    /// <inheritdoc />
    public async Task<ICollection<TDto>> GetRows(IEnumerable<TModel> rows, ListOptions<TModel, TDto, TQuery>? options = null
    )
    {
        List<TDto> result = [];
        foreach (var model in rows)
        {
            var dto = _mapper.Map<TDto>(model);
            options?.OnExtraMapping?.Invoke(model, dto);
            if (options?.OnExtraMappingAsync is not null)
                await options.OnExtraMappingAsync.Invoke(model, dto);
            result.Add(dto);
        }

        return result;
    }

    /// <summary>
    /// Allows you to manage tables linked to the repository
    /// </summary>
    /// <remarks>
    /// This method is invoked in both the Find and List to include tables that are linked to the main one
    /// </remarks>
    /// <param name="table">the main DbSet</param>
    /// <returns>Query con le include</returns>
    protected virtual IQueryable<TModel> ApplyIncludes(DbSet<TModel> table) => table.AsQueryable();

    /// <summary>
    /// Find a single record from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task<TModel> FindAsync(long id)
    {
        var model = await _context.Set<TModel>().FindAsync(id);
        if (model is null) throw new Exception($"{nameof(TModel)} con Id: {id} non trovato");
        return model;
    }

    /// <inheritdoc />
    public virtual Task<TDto> Find(long id) => Find(id, null);

    private async Task<TModel> FindWithIncludes(long id)
    {
        var withIncludes = ApplyIncludes(_context.Set<TModel>());
        var model = await withIncludes.FirstOrDefaultAsync(r => r.Id == id);
        if (model is null) throw new Exception($"{nameof(TModel)} con Id: {id} non trovato");
        return model;
    }

    private async Task<TModel> FindWithExplicitLoading(long id, FindOptions<TModel, TDto>? options)
    {
        var model = await FindAsync(id);

        if (options?.OnExplicitLoading is not null)
            await options.OnExplicitLoading.Invoke(model);
        return model;
    }

    /// <inheritdoc />
    public virtual async Task<TDto> Find(long id, FindOptions<TModel, TDto>? options)
    {
        var model = options?.OnExplicitLoading is not null 
            ? await FindWithExplicitLoading(id, options) 
            : await FindWithIncludes(id);
        var result = _mapper.Map<TDto>(model);
        // invoke delegate function to run other actions
        options?.OnExtraMapping?.Invoke(model, result);
        if (options?.OnExtraMappingAsync is not null)
            await options.OnExtraMappingAsync.Invoke(model, result);
        return result;
    }

    /// <inheritdoc />
    public virtual async Task<TDto?> GetRowByFirst(ListOptions<TModel, TDto, TQuery> options) 
        => (await GetRows(options)).FirstOrDefault();

    /// <inheritdoc />
    public virtual async Task<TDto?> GetRowByLast(ListOptions<TModel, TDto, TQuery> options) 
        => (await GetRows(options)).LastOrDefault();

    /// <summary>
    /// Make action before insert
    /// </summary>
    /// <returns></returns>
    protected virtual async Task StartInsert() => await Task.Run(() => { });

    /// <inheritdoc />
    public async Task<TModel> InsertAsync(TDto dto, InsertOptions<TModel, TDto, TQuery> options)
    {
        await StartInsert();
        var newModel = _mapper.Map<TModel>(dto);

        options.OnExtraMapping?.Invoke(dto, newModel);

        if (options.OnExtraMappingAsync is not null)
            await options.OnExtraMappingAsync.Invoke(dto, newModel);
        
        if (options.OnExtraMappingWithParamsAsync is not null && options.QueryParams is not null)
            await options.OnExtraMappingWithParamsAsync.Invoke(dto, newModel, options.QueryParams);
        
        _context.Set<TModel>().Add(newModel);
        return newModel;
    }

    /// <summary>
    /// Esegue azioni prima di aggiornare un record
    /// </summary>
    protected virtual async Task StartUpdate() => await Task.Run(() => { });

    /// <inheritdoc />
    public async Task<TModel> Update(TDto dto, UpdateOptions<TModel, TDto, TQuery> options)
    {
        await StartUpdate();
        var model = await FindAsync(dto.Id);
        var newModel = _mapper.Map(dto, model);

        // invoke OnExtraMapping 
        options.OnExtraMapping?.Invoke(dto, newModel);

        if (options.OnExtraMappingAsync is not null)
            await options.OnExtraMappingAsync.Invoke(dto, newModel);
        
        if (options.OnExtraMappingWithParamsAsync is not null && options.QueryParams is not null)
            await options.OnExtraMappingWithParamsAsync.Invoke(dto, newModel, options.QueryParams);

        _context.Update(newModel);

        return newModel;
    }

    /// <summary>
    /// make actions before delete
    /// </summary>
    /// <returns></returns>
    protected virtual async Task StartDelete() => await Task.Run(() => { });

    public async Task<TModel> Delete(long id, DeleteOptions<TModel, TDto, TQuery> options)
    {
        await StartDelete();
        var model = await FindAsync(id);
        var result = await Delete(model, options);
        return result;
    }

    /// <inheritdoc />
    public async Task<TModel> Delete(TModel model, DeleteOptions<TModel, TDto, TQuery> options)
    {
        // invoke OnExtraMapping
        if (options.OnBeforeDeleteAsync is not null)
            await options.OnBeforeDeleteAsync.Invoke(model);
        
        if (options.OnExtraMappingWithParamsAsync is not null && options.QueryParams is not null)
            await options.OnExtraMappingWithParamsAsync.Invoke(model, options.QueryParams);
        
        Logger.LogInformation(
            "elimino in modo il record {Id} per la entity {Name}",
            model.Id,
            model.GetType().Name
        );
        _context.Remove(model);
    
        return model;
    }

    #region " === Token del Dto "

    /// <inheritdoc />
    public virtual async Task<string?> CreateToken(long id, string secret)
    {
        var dto = await Find(id);
        return NetCore.CreateJwtToken(dto, secret);
    }

    /// <inheritdoc />
    public virtual TDto ReadToken(string token, string secret)
        => NetCore.DecodeToken<TDto>(token, secret);

    #endregion

    /// <summary>
    /// Validate parameters
    /// </summary>
    /// <param name="queryParameters"></param>
    /// <returns></returns>
    public virtual bool ValidateParameters(TQuery? queryParameters) => true;

    /// <inheritdoc />
    public void ExecuteSqlCommand(string sql)
    {
        Logger.LogInformation("Requested direct SQL Command: {Sql}", sql);
        _context.Database.ExecuteSqlRaw(sql);
    }
}