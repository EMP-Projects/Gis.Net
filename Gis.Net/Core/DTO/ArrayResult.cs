using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents a result object containing an array of data.
/// </summary>
/// <typeparam name="T">The type of data contained in the array. This type must implement IDtoBase.</typeparam>
public class ArrayResult<T>: ResultBase where T : IDtoBase
{
    /// <summary>
    /// Gets or initializes the data array.
    /// </summary>
    /// <remarks>
    /// The Data property represents a collection of elements of type T. 
    /// It is serialized to JSON with the name "data" and is ignored when writing null values.
    /// </remarks>
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<T>? Data { get; init; }

    /// <summary>
    /// Gets the total count of elements in the array result.
    /// </summary>
    /// <remarks>
    /// The Count property retrieves the total number of elements in the array result.
    /// It is based on the Data property, which contains the collection of elements.
    /// If the Data property is null, the count will be zero.
    /// </remarks>
    [JsonPropertyName("count")]
    public int Count => Data?.Count() ?? 0;

    /// <summary>
    /// Represents a result object that contains an array of data.
    /// </summary>
    public ArrayResult(string error) => Error = error;

    /// <summary>
    /// Represents a result object that contains an array of data.
    /// </summary>
    public ArrayResult(IEnumerable<T> data) => Data = data;
}