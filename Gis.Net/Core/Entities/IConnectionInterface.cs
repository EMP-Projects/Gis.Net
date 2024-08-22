namespace Gis.Net.Core.Entities;

/// <summary>
/// Represents a connection interface.
/// </summary>
public interface IConnectionInterface
{
    /// <summary>
    /// Represents the host property of a connection.
    /// </summary>
    string? Host { get; set; }
    
    /// <summary>
    /// Represents the port number for a connection.
    /// </summary>
    string? Port { get; set; }
    
    /// <summary>
    /// Represents a connection configuration interface.
    /// </summary>
    string? Name { get; set; }
    
    /// <summary>
    /// Represents a user for a connection.
    /// </summary>
    string? User { get; set; }
    
    /// <summary>
    /// Represents a password property for a connection interface.
    /// </summary>
    string? Password { get; set; }
    
    /// <summary>
    /// Represents a connection configuration for a database.
    /// </summary>
    string? ConnectionContext { get; }
    
    /// <summary>
    /// Represents the read buffer size used in a connection configuration.
    /// </summary>
    /// <remarks>
    /// The read buffer size determines the amount of data that can be read from the connection at once.
    /// A larger buffer size can improve the performance of data retrieval, especially for large datasets.
    /// However, it may also consume more memory.
    /// The default value is 8192 bytes.
    /// </remarks>
    /// <seealso cref="IConnectionInterface"/>
    /// <seealso cref="ConnectionPgSql"/>
    long? ReadBufferSize { get; set; }
    
    /// <summary>
    /// Gets or sets the write buffer size for a PostgreSQL connection.
    /// </summary>
    /// <remarks>
    /// The write buffer size determines the amount of data that can be written to the network at once.
    /// </remarks>
    /// <value>
    /// The write buffer size in bytes.
    /// </value>
    long? WriteBufferSize { get; set; }
    
    /// <summary>
    /// Represents the timeout value in milliseconds for a connection.
    /// </summary>
    /// <remarks>
    /// The timeout value is used to specify the amount of time, in milliseconds, that the connection should wait for a response before considering the operation as timed out.
    /// </remarks>
    int? TimeoutInMs { get; set; }
}