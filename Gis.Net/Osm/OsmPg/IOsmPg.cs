using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;

namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Interface defining operations for retrieving OpenStreetMap (OSM) entities and generating feature collections.
/// </summary>
/// <typeparam name="TGeom">The type of OSM entity, must implement both IOsmPgGenericModel and IOsmPgGeometryModel.</typeparam>
/// <typeparam name="TContext"></typeparam>
public interface IOsmPg<TGeom, TContext> 
    where TGeom : class, IOsmPgGeometryModel
    where TContext: DbContext, IOsm2PgsqlDbContext
{
    /// <summary>
    /// Asynchronously generates a feature collection based on provided criteria.
    /// </summary>
    /// <param name="options"></param>
    Task<List<Feature>> GetFeatures(OsmOptions<TGeom>? options);
}