namespace Gis.Net.Core.Entities;

public class ConnectionSqlServer : IConnectionInterface
{
    public string? ConnectionContext
    {
        get
        {
            const string cnn =
                "Server={0};Database={1};User Id={2};Password={3};TrustServerCertificate=true";
            return string.Format(cnn, Host, Name, User, Password);
        }
    }

    public long? ReadBufferSize { get; set; }
    public long? WriteBufferSize { get; set; }
    public int? TimeoutInMs { get; set; }

    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Name { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }

    public ConnectionSqlServer(string host, string name, string user, string password)
    {
        this.Host = host;
        this.Name = name;
        this.User = user;
        this.Password = password;
    }
}