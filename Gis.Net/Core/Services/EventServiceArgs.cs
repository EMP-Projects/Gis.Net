using Gis.Net.Core.DTO;

namespace Gis.Net.Core.Services;

/// <summary>
/// Represents the arguments for the EventService event.
/// </summary>
public class EventServiceArgs<TDto> : EventArgs where TDto : DtoBase
{
    /// <summary>
    /// Gets or sets a value indicating whether to force the virtual execution of a method if it is marked as virtual.
    /// </summary>
    /// <remarks>
    /// This property is used in the <see cref="EventServiceArgs{TDto}"/> class which is a base class for event arguments used in the GIS.NET Core library.
    /// When this property is set to true, it overrides the virtual behavior of a method marked as virtual.
    /// </remarks>
    public bool? ForceIfVirtual { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public long? Id { get; set; }
    
    /// <summary>
    /// Represents a property of type Dto.
    /// </summary>
    public TDto? Dto { get; set; }
    
    /// <summary>
    /// Provides CRUD operations for managing entities.
    /// </summary>
    public ECrudActions? Crud { get; set; } = null;
    
    /// <summary>
    /// Gets or sets the time at which the event was reached.
    /// This property is only used for events related to the Event Service.
    /// </summary>
    /// <value>
    /// The time at which the event was reached.
    /// </value>
    public DateTime TimedReached { get; set; } = DateTime.Now;
}