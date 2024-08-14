using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

public abstract class GisManyPropertiesModel<TGis, TModel> : 
    ModelBase,
    IGisManyProperties<TGis, TModel> 
    where TGis : GisCoreManyModel<TModel>
    where TModel: ModelBase
{
    [Column("gis_id"), ForeignKey(nameof(Gis))]
    public long GisId { get; set; }
    
    public TGis? Gis { get; set; }
}