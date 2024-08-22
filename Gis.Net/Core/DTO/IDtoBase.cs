namespace Gis.Net.Core.DTO;

/// <summary>
/// Represents the base interface for Data Transfer Objects (DTOs).
/// </summary>
public interface IDtoBase
{
    /// <summary>
    /// The unique identifier for the data transfer object (DTO).
    /// </summary>
    /// <remarks>
    /// This property is inherited from the <see cref="DtoBase"/> class and is of type <see cref="long"/>.
    /// </remarks>
    /// <seealso cref="DtoBase"/>
    long Id { get; set; }
    
    /// <summary>
    /// Represents a property key in a Data Transfer Object (DTO).
    /// </summary>
    string EntityKey { get; set; }
    
    /// <summary>
    /// Represents the timestamp property of a data transfer object (DTO) that implements the <see cref="IDtoBase"/> interface.
    /// </summary>
    /// <remarks>
    /// The timestamp property represents the date and time when the DTO object was last updated or modified.
    /// </remarks>
    DateTime TimeStamp { get; set; }
}