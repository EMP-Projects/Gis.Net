using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Services;

public abstract class ServiceCore<TDto, TModel, TQuery, TRequest> : IServiceCore<TDto, TModel, TQuery, TRequest>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase, new()
    where TRequest : RequestBase
{
    private readonly ILogger _logger;
    private readonly IRepositoryCore<TDto, TModel, TQuery> _repositoryCore;

    protected ServiceCore(ILogger logger, IRepositoryCore<TDto, TModel, TQuery> repositoryCore)
    {
        _logger = logger;
        _repositoryCore = repositoryCore;
    }

    /// <summary>
    /// Set the options for a List query
    /// </summary>
    /// <param name="q"></param>
    /// <param name="forSelect"></param>
    /// <returns></returns>
    protected virtual RepositoryGetRowsOptions<TModel, TDto, TQuery> GetRowsOptions(TQuery q) => new(q);

    /// <summary>
    /// Same as <see cref="GetRowsOptions"/> but async
    /// </summary>
    /// <param name="q"></param>
    /// <param name="forSelect"></param>
    /// <returns></returns>
    protected virtual Task<RepositoryGetRowsOptions<TModel, TDto, TQuery>> GetRowsOptionsAsync(TQuery q) => Task.FromResult(GetRowsOptions(q));

    /// <summary>
    /// Restituisce gli elementi del modello, filtrati in base al parametro <paramref name="queryParams"/>
    /// </summary>
    /// <param name="queryParams">oggetto che contiene i filtri da applicare ai risultati</param>
    /// <returns>elenco con i corrispondenti dto del modello</returns>
    public virtual async Task<ICollection<TDto>> List(TQuery? queryParams = null)
    {
        // valido queryParams, che scatena eccezione se c'è qualcosa che non va
        queryParams ??= new TQuery();
        await ValidateQueryParams(queryParams);
        var options = await GetRowsOptionsAsync(queryParams);
        // options.LoggedUser = AuthService.LoggedUser;
        var result = await _repositoryCore.GetRows(options);
        return result;
    }

    protected virtual RepositoryFindOptions<TModel, TDto> FindOptions() => new();

    public virtual async Task<TDto> Find(long id)
    {
        var options = FindOptions();
        return await _repositoryCore.Find(id, options);
    }

    protected virtual InsertOptions<TModel, TDto, TQuery> InsertOptions() => new();

    /// <summary>
    /// Metodo chiamato dopo che un model è stato aggiunto al context
    /// </summary>
    /// <remarks>
    /// <para>L'implementazione predefinita non fa nulla.</para>
    /// <para>Viene restituito un task in modo che l'override possa eseguire operazioni asincrone</para>
    /// </remarks>
    /// <param name="dto"></param>
    /// <param name="model"></param>
    protected virtual Task OnAfterInsert(TDto dto, TModel model) => Task.Run(() => { });

    /// <summary>
    /// Metodo chiamato da Insert e Update dopo aver superato la validazione, e prima di procedere con le rispettive operazioni sul Repository
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="crudEnum"></param>
    /// <returns></returns>
    protected virtual Task OnAfterValidate(TDto dto, ECrudActions crudEnum) => Task.CompletedTask;

    /// <inheritdoc />
    public virtual async Task<TModel?> Insert(TDto dto)
    {
        await Validate(dto, ECrudActions.Insert);
        await OnAfterValidate(dto, ECrudActions.Insert);
        var options = InsertOptions();
        var model = await _repositoryCore.InsertAsync(dto, options);
        await OnAfterInsert(dto, model);
        return model;
    }

    /// <summary>
    /// Opzioni passate al metodo Update della classe Repository
    /// </summary>
    /// <returns></returns>
    protected virtual UpdateOptions<TModel, TDto, TQuery> UpdateOptions() => new();

    /// <summary>
    /// A method that runs immediately after updating the context with model data
    /// </summary>
    /// <remarks>
    /// <para>The default implementation doesn't do anything.</para>
    /// <para>A task is returned so that the override can perform asynchronous operations</para>
    /// </remarks>
    /// <param name="dto"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    protected virtual Task OnAfterUpdate(TDto dto, TModel model) => Task.Run(() => { });

    /// <inheritdoc />
    public virtual async Task<TModel?> Update(TDto dto)
    {
        await Validate(dto, ECrudActions.Update);
        await OnAfterValidate(dto, ECrudActions.Update);
        var options = UpdateOptions();
        var model = await _repositoryCore.Update(dto, options);
        await OnAfterUpdate(dto, model);
        return model;
    }

    /// <summary>
    /// Set options to delete
    /// </summary>
    /// <returns></returns>
    protected virtual DeleteOptions<TModel, TDto, TQuery> DeleteOptions() => new();

    /// <summary>
    /// Metodo eseguito non appena il modello è stato rimosso dal Context
    /// </summary>
    /// <remarks>
    /// <para>L'implementazione predefinita non fa nulla.</para>
    /// <para>Viene restituito un task in modo che l'override possa eseguire operazioni asincrone</para>
    /// </remarks>
    /// <param name="model"></param>
    /// <returns></returns>
    protected virtual Task OnAfterDelete(TModel? model) => Task.Run(() => { });

    public virtual async Task<TModel> Delete(long id)
    {
        var result = await _repositoryCore.Delete(id, DeleteOptions());
        await OnAfterDelete(result);
        return result;
    }

    /// <inheritdoc />
    public virtual async Task<string?> FindToken(long id, string secret) => await _repositoryCore.CreateToken(id, secret);

    protected virtual Task OnAfterSaveContext(TModel model, ECrudActions crudAction) => Task.CompletedTask;

    public async Task<int> SaveContext(TModel model, ECrudActions crudAction)
    {
        var result = await _repositoryCore.SaveChanges();
        await OnAfterSaveContext(model, crudAction);
        return result;
    }

    public abstract Task ValidateRequest(TRequest request, ECrudActions crudAction);

    /// <inheritdoc />
    public abstract Task Validate(TDto dto, ECrudActions crudEnum);

    /// <inheritdoc />
    public virtual async Task ValidateQueryParams(TQuery queryParams) => await Task.Run(() => { });

    public IRepositoryCore<TDto, TModel, TQuery> GetRepository() => _repositoryCore;
}