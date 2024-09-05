using Gis.Net.Osm.OsmPg.Properties;
using Gis.Net.Osm.OsmPg.Vector;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Represents a database context for working with OSM (OpenStreetMap) data in a PostgreSQL database.
/// </summary>
public interface IOsmDbContext
{
    /// <summary>
    /// Represents a model that holds the properties of an OpenStreetMap object.
    /// </summary>
    DbSet<OsmPropertiesModel>? OsmProperties { get; set; }
    
    /// <summary>
    /// Represents a vector model for OpenStreetMap objects.
    /// </summary>
    DbSet<OsmVectorModel>? OsmVector { get; set; }
}