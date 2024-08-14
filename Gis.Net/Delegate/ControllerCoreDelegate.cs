using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Delegate;

public delegate void AfterSaveChanges(IDtoBase dtoBase, IModelBase modelBase);