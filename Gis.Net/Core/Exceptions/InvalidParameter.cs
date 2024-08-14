namespace Gis.Net.Core.Exceptions;

public class InvalidParameter : Exception
{
    public InvalidParameter(string paramName)
        : base($"Parametro {paramName} assente o non valido") { }
}