using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// The base class for GIS core models.
/// </summary>
/// <typeparam name="T">The type of properties associated with the model.</typeparam>
public abstract class GisCoreModel<T> : VectorModel, IGisCoreProperties<T> where T : ModelBase
{
    /// <inheritdoc />
    [Column("id_properties"), ForeignKey(nameof(Properties))] 
    public long IdProperties { get; set; }

    /// <inheritdoc />
    public required T Properties { get; set; }
}