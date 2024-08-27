namespace Gis.Net.Istat;

/// <summary>
/// Represents a delegate that modifies an <see cref="IQueryable{T}"/> query.
/// </summary>
/// <typeparam name="T">The type of the model that implements <see cref="ILimitsModelBase"/>.</typeparam>
/// <param name="query">The query to be modified.</param>
/// <returns>The modified query.</returns>
public delegate IQueryable<T> QueryDelegate<T>(IQueryable<T> query) where T : class, ILimitsModelBase;