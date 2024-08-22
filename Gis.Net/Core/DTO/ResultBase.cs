using System.Text.Json.Serialization;

namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents the base class for result objects.
/// </summary>
public class ResultBase
{
    /// <summary>
    /// Represents an error that occurred during an operation.
    /// </summary>
    [JsonPropertyName("error"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Error { get; set; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    /// <remarks>
    /// This property is used to determine if an operation was executed successfully.
    /// It returns true if there were no errors, and false otherwise.
    /// </remarks>
    [JsonPropertyName("hasSuccess")]
    public bool HasSuccess => Error is null;
}


