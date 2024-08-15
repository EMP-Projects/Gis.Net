using Gis.Net.Osm.OsmPg.Properties;
using Gis.Net.Osm.OsmPg.Vector;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

public interface IOsmDbContext
{
    DbSet<OsmPropertiesModel> OsmProperties { get; set; }
    DbSet<OsmVectorModel> OsmVector { get; set; }
}