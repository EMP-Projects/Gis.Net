using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents a single result operation containing either data or an error.
/// </summary>
/// <typeparam name="T">The type of data contained in the result. This type must implement IDtoBase.</typeparam>
public class SingleResult<T> : ResultBase where T : IDtoBase
{
    /// <summary>
    /// Gets or sets the data of type T.
    /// </summary>
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    /// <summary>
    /// Initializes a new instance of the SingleResult class with an error message.
    /// </summary>
    /// <param name="error">The error message associated with the result.</param>
    public SingleResult(string error) => Error = error;

    /// <summary>
    /// Initializes a new instance of the SingleResult class with data.
    /// </summary>
    /// <param name="data">The data to store in the result.</param>
    public SingleResult(T? data) => Data = data;
}