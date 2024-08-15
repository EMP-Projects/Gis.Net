using Gis.Net.Osm.OsmPg.Models;

namespace Gis.Net.Osm.OsmPg;

public delegate IQueryable<T> QueryDelegate<T>(IQueryable<T> query) 
    where T : class, IOsmPgGeometryModel;
    
public delegate IEnumerable<T> EnumerableDelegate<T>(IEnumerable<T> query, string[] tags) 
    where T : class, IOsmPgGeometryModel;