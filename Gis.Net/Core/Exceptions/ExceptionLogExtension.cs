namespace Gis.Net.Core.Exceptions;

/// <summary>
/// Provides extension methods for logging exceptions.
/// </summary>
public static class ExceptionLogExtension
{
    /// <summary>
    /// Converts the given exception to a log message.
    /// </summary>
    /// <param name="e">The exception to convert.</param>
    /// <returns>The log message representing the exception.</returns>
    /// <example>
    /// <code>
    /// try
    /// {
    /// // Code that may throw an exception
    /// }
    /// catch (Exception ex)
    /// {
    /// string logMessage = ex.toLogMessage();
    /// Console.WriteLine(logMessage);
    /// }
    /// </code>
    /// </example>
    public static string ToLogMessage(this Exception e)
    {
        var m = $"[{e.GetType().Name}] {e.Message}";
        if (e.InnerException != null)
            m = string.Concat(m, " => ", e.InnerException.ToLogMessage());
        return m;
    }
}