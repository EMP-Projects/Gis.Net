using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Core.Services;

/// <inheritdoc />
public abstract class ServiceCore<TModel, TDto, TQuery, TRequest, TContext> : 
    IServiceCore<TModel, TDto, TQuery, TRequest, TContext>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase, new()
    where TRequest : RequestBase
    where TContext : DbContext
{
    /// The Logger variable is an instance of the ILogger interface provided by the Microsoft.Extensions.Logging namespace.
    /// It is used for logging information, warnings, and errors during the execution of the ServiceCore class and its derived classes.
    /// @type {ILogger}
    protected readonly ILogger Logger;

    /// The _repositoryCore variable is an instance of a class that implements the IRepositoryCore interface.
    /// It is used as a core repository for accessing and manipulating data related to a specific model.
    private readonly IRepositoryCore<TModel, TDto, TQuery, TContext> _repositoryCore;

    /// <summary>
    /// The logger instance used for logging.
    /// </summary>
    protected ServiceCore(ILogger logger, IRepositoryCore<TModel, TDto, TQuery, TContext> repositoryCore)
    {
        Logger = logger;
        _repositoryCore = repositoryCore;
    }

    /// <summary>
    /// Represents the options for retrieving a list of rows from the database.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO (Data Transfer Object).</typeparam>
    /// <typeparam name="TQuery">The type of the query parameters.</typeparam>
    protected virtual ListOptions<TModel, TDto, TQuery> GetRowsOptions(TQuery q) => new(q);

    /// <inheritdoc />
    public virtual async Task<ICollection<TDto>> List(TQuery? queryParams = null)
    {
        queryParams ??= new TQuery();
        await ValidateQueryParams(queryParams);
        var result = await _repositoryCore.GetRows(GetRowsOptions(queryParams));
        return result;
    }

    /// <summary>
    /// Represents the options for the Find method in the ServiceCore class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    protected virtual FindOptions<TModel, TDto> FindOptions() => new();

    /// <inheritdoc />
    public virtual async Task<TDto> Find(long id) => await _repositoryCore.Find(id, FindOptions());

    /// <summary>
    /// Represents the options for inserting a new record into the database.
    /// </summary>
    /// <typeparam name="TModel">The type of the model class.</typeparam>
    /// <typeparam name="TDto">The type of the DTO class.</typeparam>
    /// <typeparam name="TQuery">The type of the query class.</typeparam>
    protected virtual InsertOptions<TModel, TDto, TQuery> InsertOptions() => new();

    /// <summary>
    /// Executes after a new record is inserted into the database.
    /// </summary>
    /// <param name="dto">The DTO representing the inserted record.</param>
    /// <param name="model">The model representing the inserted record.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected virtual Task OnAfterInsert(TDto dto, TModel model) => Task.CompletedTask;

    /// <summary>
    /// Method called after the validation of the input DTO in a specific CRUD action.
    /// </summary>
    /// <param name="dto">The data transfer object (DTO) being validated.</param>
    /// <param name="crudEnum">The CRUD action being performed (Insert, Update, or Delete).</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
    /// Represents the options for updating a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>An instance of UpdateOptions.</returns>
    protected virtual UpdateOptions<TModel, TDto, TQuery> UpdateOptions() => new();

    /// <summary>
    /// Method called after updating a record.
    /// </summary>
    /// <param name="dto">The DTO containing the updated data.</param>
    /// <param name="model">The updated model object.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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
    /// Represents the options for the Delete operation.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    protected virtual DeleteOptions<TModel, TDto, TQuery> DeleteOptions() => new();

    /// <summary>
    /// Method that is called after a record is deleted.
    /// </summary>
    /// <param name="model">The deleted model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnAfterDelete(TModel? model) => Task.CompletedTask;

    /// <inheritdoc />
    public virtual async Task<TModel> Delete(long id)
    {
        var result = await _repositoryCore.Delete(id, DeleteOptions());
        await OnAfterDelete(result);
        return result;
    }

    /// <inheritdoc />
    public virtual async Task<string?> FindToken(long id, string secret) => await _repositoryCore.CreateToken(id, secret);

    /// <summary>
    /// Performs additional actions after saving the context.
    /// </summary>
    /// <param name="model">The model that was saved.</param>
    /// <param name="crudAction">The CRUD action performed (Insert, Update, or Delete).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnAfterSaveContext(TModel model, ECrudActions crudAction) => Task.CompletedTask;

    /// <inheritdoc />
    public async Task<int> SaveContext(TModel model, ECrudActions crudAction)
    {
        var result = await SaveContext();
        await OnAfterSaveContext(model, crudAction);
        return result;
    }

    /// <inheritdoc />
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