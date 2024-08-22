using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Represents an abstract class for a GIS model with multiple properties.
/// </summary>
/// <typeparam name="TGis">The GIS model type.</typeparam>
/// <typeparam name="TModel">The base model type.</typeparam>
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