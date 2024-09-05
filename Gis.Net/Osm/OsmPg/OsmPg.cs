using Gis.Net.Osm.OsmPg.Models;
using Gis.Net.Vector;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;

namespace Gis.Net.Osm.OsmPg;

/// <inheritdoc />
public class OsmPg<TGeom, TContext> : IOsmPg<TGeom, TContext> 
    where TGeom : class, IOsmPgGeometryModel
    where TContext: DbContext, IOsm2PgsqlDbContext
{
    private readonly TContext _context;

    /// <summary>
    /// Represents a class that provides operations for retrieving OpenStreetMap (OSM) entities and generating feature collections using the Osm2Pgsql database context.
    /// </summary>
    public OsmPg(TContext context) => _context = context;

    /// <summary>
    /// Asynchronously retrieves a collection of OSM entities based on provided criteria.
    /// </summary>
    /// <param name="options"></param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of OSM entities.</returns>
    private async Task<IEnumerable<TGeom>?> GetOsmEntities(OsmOptions<TGeom>? options)
    {
        var entities = _context.Set<TGeom>().AsNoTracking();
        if (options?.OnBeforeQuery is not null)
            entities = options.OnBeforeQuery.Invoke(entities);

        if (options?.Geom is not null)
        {
            if (GisGeometries.IsPoint(options.Geom))
                entities = entities.Where(entry => entry.Way != null && entry.Way.IsWithinDistance(options.Geom, options.DistanceMt ?? 100));
            else if (GisGeometries.IsLineString(options.Geom))
                entities = entities.Where(entry => entry.Way != null && entry.Way.Touches(options.Geom));
            else if (GisGeometries.IsPolygon(options.Geom))
                entities = entities.Where(entry => entry.Way != null && entry.Way.Intersects(options.Geom));
            else
            {
                var geom = GisGeometries.NormalizeGeometry(options.Geom);
                entities = entities.Where(entry => entry.Way != null && entry.Way.Intersects(geom));
            }
        }

        var query = await entities.ToListAsync();
        
        if (options?.OnAfterQuery is not null)
            query = options.OnAfterQuery?.Invoke(query, options.Tags ?? []);
        
        return query;
    }
    
    /// <summary>
    /// GetFeatures method retrieves OpenStreetMap (OSM) entities and generates a list of features.
    /// </summary>
    /// <param name="options">Options for querying OSM entities and generating features.</param>
    /// <returns>A list of features that represent the retrieved OSM entities.</returns>
    public async Task<List<Feature>?> GetFeatures(OsmOptions<TGeom>? options)
    {
        if (options?.Error is not null)
            throw new Exception(options.Error);

        var entities = await GetOsmEntities(options);
        
        var features = entities?.Select(entry =>
        {
            var feature = GisUtility.CreateEmptyFeature(3857, entry.Way!);
            feature.Attributes.Add("OSM", new OsmProperties
            {
                Type = options?.Type ?? "",
                Tags = options?.Tags ?? [],
                Name = entry.Name
            });
            return feature;
        }).ToList();
        
        return features;
    }
}