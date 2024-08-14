namespace Gis.Net.Core.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException(string key)
        : base($"Chiave di configurazione mancante o non valida: {key}") { }
}