namespace Gis.Net.Core.Exceptions;

public static class ExceptionLogExtension
{
    public static string toLogMessage(this Exception e)
    {
        var m = $"[{e.GetType().Name}] {e.Message}";
        if (e.InnerException != null)
            m = string.Concat(m, " => ", e.InnerException.toLogMessage());
        return m;
    }
}