namespace Gis.Net.Istat;

/// <summary>
/// Represents options for querying ISTAT limits.
/// </summary>
/// <typeparam name="T">The type of the model that implements <see cref="ILimitsModelBase"/>.</typeparam>
public class IstatLimitOptions<T> where T : class, ILimitsModelBase
{
    /// <summary>
    /// Gets or sets a delegate that is invoked before the query is executed.
    /// </summary>
    public QueryDelegate<T>? OnBeforeQuery { get; set; }
}