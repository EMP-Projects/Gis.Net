using Gis.Net.Core.DTO;

namespace Gis.Net.Core.Services;

public class EventServiceArgs<TDto> : EventArgs where TDto : DtoBase
{
    public bool? ForceIfVirtual { get; set; } = false;
    public long? Id { get; set; }
    public TDto? Dto { get; set; }
    public ECrudActions? Crud { get; set; } = null;
    public DateTime TimedReached { get; set; } = DateTime.Now;
}