using Microsoft.Extensions.Configuration;

namespace Gis.Net.Core.Entities;

/// <summary>
/// 
/// </summary>
public class ConnectionPgSql : IConnectionInterface
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Name { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
    public int? TimeoutInMs { get; set; }

    public string? ConnectionContext
    {
        get
        {
            Host ??= "localhost";
            Port ??= "5432";
            ReadBufferSize ??= 8192;
            WriteBufferSize ??= 8192;
            TimeoutInMs ??= 20000;

            const string cnn =
                "Host={0};Database={1};Username={2};Password={3};Port={4};" +
                "CommandTimeout={5};Include Error Detail=true;" +
                "Read Buffer Size={6};Write Buffer Size={7}";

            return string.Format(cnn, Host, Name, User, Password, Port, TimeoutInMs, ReadBufferSize,
                WriteBufferSize);
        }
    }

    public long? ReadBufferSize { get; set; }
    public long? WriteBufferSize { get; set; }

    public ConnectionPgSql(string host, string port, string name, string user, string password)
    {
        Host = host;
        Port = port;
        Name = name;
        User = user;
        Password = password;
    }

    public ConnectionPgSql(IConfiguration configuration) :
        this(configuration["POSTGRES_HOST"]!,
            configuration["POSTGRES_PORT"]!,
            configuration["POSTGRES_DB"]!,
            configuration["POSTGRES_USER"]!,
            configuration["POSTGRES_PASSWORD"]!)
    {
        ReadBufferSize = configuration.GetValue<long?>("POSTGRES_READ_BUFFER_SIZE");
        WriteBufferSize = configuration.GetValue<long?>("POSTGRES_WRITE_BUFFER_SIZE");
        TimeoutInMs = configuration.GetValue<int?>("POSTGRES_TIMEOUT");
        if (int.TryParse(configuration["POSTGRES_TIMEOUT"], out var t)) TimeoutInMs = t;
    }
}