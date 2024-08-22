namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents the base interface for request objects.
/// </summary>
public interface IRequestBase
{
    /// <summary>
    /// Gets or sets the Id of the request.
    /// </summary>
    /// <remarks>
    /// This property represents the unique identifier of the request.
    /// </remarks>
    int Id { get; set; }
}