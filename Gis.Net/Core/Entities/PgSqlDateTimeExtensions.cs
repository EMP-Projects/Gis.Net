namespace Gis.Net.Core.Entities;

/// <summary>
/// Provides extension methods for handling PgSqlDateTime values.
/// </summary>
public static class PgSqlDateTimeExtensions
{
    /// <summary>
    /// Convert a DateTime object to a UTC Date object.
    /// </summary>
    /// <param name="dateTime">The DateTime object to convert to UTC Date.</param>
    /// <returns>A UTC Date object.</returns>
    public static DateTime UtcDate(this DateTime dateTime) => new DateTimeOffset(dateTime).DateTime;
}