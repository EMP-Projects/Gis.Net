using Gis.Net.Core.Entities;

namespace Gis.Net.Vector.Models;

/// <summary>
/// Modello per la gestione di più modelli
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GisCoreManyModel<T> : VectorModel, IGisCoreManyModels<T> where T : ModelBase
{
    /// <inheritdoc />
    public ICollection<T>? PropertiesCollection { get; set; }
}