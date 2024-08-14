using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Inetrfaccia per il modello delle proprietà del vettore
/// </summary>
/// <typeparam name="TGis">Modello della tabella con geometrie</typeparam>
/// <typeparam name="TModel">Modello base della tabella con le proprietà</typeparam>
public interface IGisManyProperties<TGis, TModel> 
    where TGis : GisCoreManyModel<TModel>
    where TModel: ModelBase
{
    long GisId { get; set; }
    TGis? Gis { get; set; }
}