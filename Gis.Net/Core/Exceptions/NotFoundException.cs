namespace Gis.Net.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string model, int id)
        : base($"Id {id} non trovato per il tipo {model}") { }

    public NotFoundException(string model, string key)
        : base($"Valore {key} non trovato per il tipo {model}") { }

    public NotFoundException(string message)
        : base(message) { }
}