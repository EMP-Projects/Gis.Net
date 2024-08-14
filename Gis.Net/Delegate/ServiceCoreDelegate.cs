using Gis.Net.Core.DTO;
using Gis.Net.Core.Services;

namespace Gis.Net.Delegate;

public delegate void BeforeSaveChangesEventHandler<TDto>(object sender, EventServiceArgs<TDto> e) where TDto : DtoBase;
public delegate void PresetDtoFieldsBeforeSaveEventHandler<TDto>(object sender, EventServiceArgs<TDto> e) where TDto : DtoBase;
public delegate void DeleteEventHandler<TDto>(object sender, EventServiceArgs<TDto> e) where TDto : DtoBase;
public delegate void SaveChangesEventHandler<TDto>(object sender, EventServiceArgs<TDto> e) where TDto : DtoBase;