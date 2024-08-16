using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <inheritdoc cref="Gis.Net.Vector.Models.IGisManyProperties<TGis,TModel>" />
public abstract class GisManyPropertiesModel<TGis, TModel> : 
    ModelBase,
    IGisManyProperties<TGis, TModel> 
    where TGis : GisCoreManyModel<TModel>
    where TModel: ModelBase
{
    /// <inheritdoc />
    [Column("gis_id"), ForeignKey(nameof(Gis))]
    public long GisId { get; set; }

    /// <inheritdoc />
    public TGis? Gis { get; set; }
}