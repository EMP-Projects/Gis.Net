using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Services;

public abstract class ServiceCore<TModel, TDto, TQuery, TRequest, TContext> : 
    IServiceCore<TModel, TDto, TQuery, TRequest, TContext>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase, new()
    where TRequest : RequestBase
    where TContext : DbContext
{
    protected readonly ILogger Logger;
    private readonly IRepositoryCore<TModel, TDto, TQuery, TContext> _repositoryCore;

    protected ServiceCore(ILogger logger, IRepositoryCore<TModel, TDto, TQuery, TContext> repositoryCore)
    {
        Logger = logger;
        _repositoryCore = repositoryCore;
    }

    protected virtual ListOptions<TModel, TDto, TQuery> GetRowsOptions(TQuery q) => new(q);

    /// <inheritdoc />
    public virtual async Task<ICollection<TDto>> List(TQuery? queryParams = null)
    {
        // valido queryParams, che scatena eccezione se c'è qualcosa che non va
        queryParams ??= new TQuery();
        await ValidateQueryParams(queryParams);
        var result = await _repositoryCore.GetRows(GetRowsOptions(queryParams));
        return result;
    }

    protected virtual FindOptions<TModel, TDto> FindOptions() => new();

    public virtual async Task<TDto> Find(long id) => await _repositoryCore.Find(id, FindOptions());

    protected virtual InsertOptions<TModel, TDto, TQuery> InsertOptions() => new();

    protected virtual Task OnAfterInsert(TDto dto, TModel model) => Task.CompletedTask;

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

    protected virtual UpdateOptions<TModel, TDto, TQuery> UpdateOptions() => new();

    protected virtual Task OnAfterUpdate(TDto dto, TModel model) => Task.CompletedTask;

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

    protected virtual Task OnAfterDelete(TModel? model) => Task.CompletedTask;

    public virtual async Task<TModel> Delete(long id)
    {
        var result = await _repositoryCore.Delete(id, DeleteOptions());
        await OnAfterDelete(result);
        return result;
    }

    /// <inheritdoc />
    public virtual async Task<string?> FindToken(long id, string secret) => await _repositoryCore.CreateToken(id, secret);

    protected virtual Task OnAfterSaveContext(TModel model, ECrudActions crudAction) => Task.CompletedTask;

    /// <inheritdoc />
    public async Task<int> SaveContext(TModel model, ECrudActions crudAction)
    {
        var result = await SaveContext();
        await OnAfterSaveContext(model, crudAction);
        return result;
    }
    
    public async Task<int> SaveContext() => await _repositoryCore.SaveChanges();

    /// <inheritdoc />
    public virtual Task ValidateRequest(TRequest request, ECrudActions crudAction) => Task.CompletedTask;

    /// <inheritdoc />
    public virtual Task Validate(TDto dto, ECrudActions crudEnum) => Task.CompletedTask;

    /// <inheritdoc />
    public virtual Task ValidateQueryParams(TQuery queryParams) => Task.CompletedTask;

    /// <inheritdoc />
    public IRepositoryCore<TModel, TDto, TQuery, TContext> GetRepository() => _repositoryCore;
}