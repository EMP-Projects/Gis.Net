namespace Gis.Net.Core.Entities;

public interface IConnectionInterface
{
    string? Host { get; set; }
    string? Port { get; set; }
    string? Name { get; set; }
    string? User { get; set; }
    string? Password { get; set; }
    string? ConnectionContext { get; }
    long? ReadBufferSize { get; set; }
    long? WriteBufferSize { get; set; }
    int? TimeoutInMs { get; set; }
}