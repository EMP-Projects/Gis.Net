using AutoMapper;
using Gis.Net.OsmPg.Properties;
using Gis.Net.Vector.Services;
using Gis.Net.VectorCore.OsmPg.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace Gis.Net.OsmPg.Vector;

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
    /// Crea la base dati 
    /// </summary>
    /// <param name="bbox"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<int> SeedGeometries(Geometry bbox, string key)
    {
        // leggo le features di Osm
        var featuresOsm = await _osmPgService.GetFeatures(bbox);

        // leggo tutte le features presenti per controllare le feature già presenti nel database
        var featuresDb = await FeatureCollection(new OsmVectorQuery
        {
            SrCode = 3857
        });

        if (featuresDb is null)
            throw new ApplicationException("Non riesco a leggere le features dal database");

        var countInsert = 0;
        foreach (var feature in featuresOsm)
        {
            // controllo se la feature è già presente nel database
            if (featuresDb.Any(f => f.Geometry.EqualsExact(feature.Geometry)))
                continue;

            // aggiungo la feature al database se la feature ha le proprietà di OSM
            var propertiesName = feature.Attributes.GetNames();
            if (propertiesName.Length == 0)
                continue;

            var propertiesFeatures = propertiesName.Contains("OSM") 
                ? feature.Attributes.GetOptionalValue("OSM")
                : null;

            if (propertiesFeatures is null)
                continue;
            
            var properties = _mapper.Map<OsmPropertiesDto>(propertiesFeatures);
            
            var osmVectorModel = new OsmVectorDto
            {
                Guid = Guid.NewGuid(),
                TimeStamp = DateTime.UtcNow,
                Geom = feature.Geometry,
                Properties = properties,
                Key = key
            };

            await Insert(osmVectorModel);
            countInsert++;
        }

        return countInsert > 0 ? await SaveContext() : countInsert;
    }
}