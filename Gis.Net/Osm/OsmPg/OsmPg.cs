using Gis.Net.Osm.OsmPg;
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

    public OsmPg(TContext context) => _context = context;

    /// <summary>
    /// Asynchronously retrieves a collection of OSM entities based on provided criteria.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="key"></param>
    /// <param name="values"></param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of OSM entities.</returns>
    private async Task<IEnumerable<TGeom>> GetOsmEntities(OsmOptions<TGeom>? options)
    {
        var entities = _context.Set<TGeom>().AsNoTracking();
        entities = options?.OnBeforeQuery?.Invoke(entities);

        if (entities == null)
            return [];

        // query spaziale sulla geometria
        if (options?.Geom is not null)
            entities = entities.Where(entry => entry.Way != null && entry.Way.Intersects(options.Geom));
        
        var query = await entities.ToListAsync();
        
        return options?.OnAfterQuery?.Invoke(query, options.Tags) ?? query;
    }

    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public async Task<List<Feature>> GetFeatures(OsmOptions<TGeom>? options)
    {
        if (options?.Error is not null)
            throw new Exception(options.Error);

        var entities = await GetOsmEntities(options);
        
        var features = entities.Select(entry =>
        {
            // options.SrCode qui ha sempre un valore
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