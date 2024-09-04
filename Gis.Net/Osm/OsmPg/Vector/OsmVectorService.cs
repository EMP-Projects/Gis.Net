using AutoMapper;
using Gis.Net.Osm.OsmPg.Properties;
using Gis.Net.Vector.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.OsmPg.Vector;

/// <inheritdoc />
public class OsmVectorService<T> : 
    GisVectorCoreService<OsmVectorModel, OsmVectorDto, OsmVectorQuery, OsmVectorRequest, T, OsmPropertiesModel, OsmPropertiesDto>
where T : DbContext
{
    private readonly IOsmPgService _osmPgService;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public OsmVectorService(
        ILogger<OsmVectorService<T>> logger, 
        OsmVectorRepository<T> netCoreRepository,
        IOsmPgService osmPgService, 
        IMapper mapper) : base(logger, netCoreRepository)
    {
        _osmPgService = osmPgService;
        _mapper = mapper;
    }


    /// <summary>
    /// Seeds geometries in the OsmVectorModel table based on the given bounding box and key.
    /// </summary>
    /// <param name="bbox">The bounding box representing the area to seed geometries.</param>
    /// <param name="key">The key to associate with the seeded geometries.</param>
    /// <param name="distance"></param>
    /// <returns>The number of geometries inserted or 0 if no geometries were inserted.</returns>
    public async Task<int> SeedGeometries(Geometry bbox, string key)
    {
        // Awaiting for the GetFeatures method from the _osmPgService with the given Geometry object (bbox) as an argument.
        var featuresOsm = await _osmPgService.GetFeatures(bbox);

        // Awaiting for the FeatureCollection method with a new OsmVectorQuery created as an argument.
        // The SrCode property of OsmVectorQuery is set to 3857.
        var featuresDb = await FeatureCollection(new OsmVectorQuery { SrCode = 3857 });

        // An exception is thrown when the featuresDb is null.
        if (featuresDb is null)
            throw new ApplicationException("I can't read features from the database.");

        // Initialization of the countInsert variable to 0. It's used to count the number of successful inserts.
        var countInsert = 0;

        // For every feature in featuresOsm, do the following
        foreach (var feature in featuresOsm)
        {
            // If there is any feature in featuresDb with exactly the same Geometry as the current feature, skip to the next feature.
            if (featuresDb.Any(f => f.Geometry.EqualsExact(feature.Geometry)))
                continue;

            // Get all attribute names of the feature.
            var propertiesName = feature.Attributes.GetNames();
            // If the feature doesn't have any attributes, skip to the next feature.
            if (propertiesName.Length == 0)
                continue;

            // Get the value of the "OSM" attribute from the feature if it exists.
            // If it doesn't exist, set the propertiesFeatures variable to null.
            var propertiesFeatures = propertiesName.Contains("OSM") ? (OsmProperties)feature.Attributes.GetOptionalValue("OSM") : null;

            // If propertiesFeatures is null, skip to the next feature.
            if (propertiesFeatures is null)
                continue;

            // Map the propertiesFeatures to an OsmPropertiesDto object using the _mapper.
            var properties = _mapper.Map<OsmPropertiesDto>(propertiesFeatures);
            
            // Create a new OsmVectorDto object and set its properties.
            var osmVectorModel = new OsmVectorDto
            {
                Guid = Guid.NewGuid(),
                TimeStamp = DateTime.UtcNow,
                Geom = feature.Geometry,
                Properties = properties,
                EntityKey = key
            };

            // Await on the Insert method with the new OsmVectorDto object as an argument.
            await Insert(osmVectorModel);
            // Increment the countInsert variable, indicating a successful insertion.
            countInsert++;
        }

        // If the countInsert variable is more than 0, await on the SaveContext method and return its result.
        // Else, return the countInsert variable as is.
        return countInsert > 0 ? await SaveContext() : countInsert;
    }
}