using Gis.Net.OsmPg.Vector;
using Gis.Net.VectorCore.OsmPg.Properties;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.OsmPg;

public interface IOsmDbContext
{
    DbSet<OsmPropertiesModel> OsmProperties { get; set; }
    DbSet<OsmVectorModel> OsmVector { get; set; }
}