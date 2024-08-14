using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Delegate;

namespace Gis.Net.Core.Repositories;

public class RepositoryGetRowsOptions<TModel, TDto, TQuery>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
{
    /// <summary>
    /// Operazioni preliminari prima della query
    /// </summary>
    public QueryableDelegate<TModel>? OnBeforeQuery { get; set; }

    /// <summary>
    /// Sorting
    /// </summary>
    public QueryableDelegate<TModel>? OnSort { get; set; }
    
    /// <summary>
    /// Parametri per il sorting.
    /// Attenzione! Se viene usato questo tipo di ordinamento sostituisce il metodo delegato OnSort.
    /// </summary>
    public QueryableParamsDelegate<TModel, TQuery>? OnSortParams { get; set; }

    /// <summary>
    /// Operazioni preliminari dopo la query con i parametri
    /// </summary>
    public QueryableParamsDelegateAsync<TModel, TQuery>? OnAfterQueryParams { get; set; }
    
    /// <summary>
    /// Operazioni extra sul mapping dei dati
    /// </summary>
    public ModelToDtoExtraMapperDelegate<TModel, TDto>? OnExtraMapping { get; set; } = null;

    /// <summary>
    /// Come <see cref="OnExtraMapping"/> ma supporta operazioni asincrone
    /// </summary>
    public ModelToDtoExtraMapperAsyncDelegate<TModel, TDto>? OnExtraMappingAsync { get; set; }

    /// <summary>
    /// Parametri per la ricerca
    /// </summary>
    public TQuery? QueryParams { get; set; }

    public RepositoryGetRowsOptions()
    {
    }
    
    public RepositoryGetRowsOptions(TQuery queryParams) : this() => QueryParams = queryParams;
}