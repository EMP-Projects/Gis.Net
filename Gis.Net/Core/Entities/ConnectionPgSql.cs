using Microsoft.Extensions.Configuration;

namespace Gis.Net.Core.Entities;

/// <summary>
/// Represents a PostgreSQL connection configuration.
/// </summary>
public class ConnectionPgSql : IConnectionInterface
{
    /// <summary>
    /// Represents the host property of a PostgreSQL connection.
    /// </summary>
    public string? Host { get; set; }
    
    /// <summary>
    /// Represents the port property of a connection.
    /// </summary>
    public string? Port { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the connection.
    /// </summary>
    /// <value>
    /// The name of the connection.
    /// </value>
    public string? Name { get; set; }
    
    /// <summary>
    /// Represents a user.
    /// </summary>
    public string? User { get; set; }
    
    /// <summary>
    /// Represents the password used for authentication.
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Gets or sets the timeout value in milliseconds for establishing a connection.
    /// </summary>
    /// <value>
    /// The timeout value in milliseconds for establishing a connection.
    /// </value>
    public int? TimeoutInMs { get; set; }

    /// <summary>
    /// Represents the connection context for a PostgreSQL connection.
    /// </summary>
    public string ConnectionContext
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

    /// <summary>
    /// Gets or sets the read buffer size for the PostgreSQL connection.
    /// </summary>
    /// <remarks>
    /// The read buffer size specifies the size of the buffer used for reading data from the PostgreSQL server.
    /// The default value is 8192 bytes.
    /// </remarks>
    public long? ReadBufferSize { get; set; }
    
    /// <summary>
    /// Gets or sets the write buffer size for the connection.
    /// </summary>
    /// <remarks>
    /// The write buffer size determines the size of the buffer used for writing data to the connection.
    /// It can be used to optimize the performance of write operations by reducing the number of network round-trips.
    /// Setting a larger buffer size can improve performance, especially for bulk write operations or high-latency connections.
    /// The default value is 8192 bytes.
    /// </remarks>
    public long? WriteBufferSize { get; set; }

    public ConnectionPgSql(string host, string port, string name, string user, string password)
    {
        Host = host;
        Port = port;
        Name = name;
        User = user;
        Password = password;
    }

    /// <summary>
    /// Represents a PostgreSQL connection configuration.
    /// </summary>
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