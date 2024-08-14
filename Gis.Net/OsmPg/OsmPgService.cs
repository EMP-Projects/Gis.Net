using Gis.Net.OsmPg.Models;
using Gis.Net.Vector;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.OsmPg;

/// <inheritdoc />
public abstract class OsmPgService<T> : IOsmPgService where T : DbContext, IOsm2PgsqlDbContext
{
    private readonly IOsmPg<PlanetOsmLine, T> _lines;
    private readonly IOsmPg<PlanetOsmPolygon, T> _polygons;
    private readonly IOsmPg<PlanetOsmPoint, T> _points;
    private readonly IOsmPg<PlanetOsmRoads, T> _roads;

    protected OsmPgService(
        IOsmPg<PlanetOsmLine, T> lines, 
        IOsmPg<PlanetOsmPolygon, T> polygons, 
        IOsmPg<PlanetOsmPoint, T> points, 
        IOsmPg<PlanetOsmRoads, T> roads)
    {
        _lines = lines;
        _polygons = polygons;
        _points = points;
        _roads = roads;
    }

    private static Envelope CalculateBoundingBox(List<Feature> features)
    {
        var envelope = new Envelope();
        foreach (var feature in features)
            envelope.ExpandToInclude(feature.Geometry.EnvelopeInternal);
        return envelope;
    }

    /// <summary>
    /// Configurazione delle opzioni per la ricerca di features geometriche di tipo linea
    /// </summary>
    /// <returns></returns>
    protected abstract OsmOptions<PlanetOsmLine>? OsmOptionsLines(Geometry geom);
    
    /// <summary>
    /// Configurazione delle opzioni per la ricerca di features geometriche di tipo poligono
    /// </summary>
    /// <returns></returns>
    protected abstract OsmOptions<PlanetOsmPolygon>? OsmOptionsPolygon(Geometry geom);

    /// <summary>
    /// Configurazione delle opzioni per la ricerca di features geometriche di tipo punto
    /// </summary>
    /// <returns></returns>
    protected abstract OsmOptions<PlanetOsmPoint>? OsmOptionsPoint(Geometry geom);

    /// <summary>
    /// Configurazione delle opzioni per la ricerca di features geometriche di tipo strade
    /// </summary>
    /// <returns></returns>
    protected abstract OsmOptions<PlanetOsmRoads>? OsmOptionsRoads(Geometry geom);

    /// <param name="geom"></param>
    /// <inheritdoc />
    public async Task<FeatureCollection> GetFeatures(Geometry geom)
    {
        var features = new List<Feature>();

        var optionsLines = OsmOptionsLines(geom);
        if (optionsLines is not null)
            features.AddRange(await _lines.GetFeatures(OsmOptionsLines(geom)));

        var optionsPolygons = OsmOptionsPolygon(geom);
        if (optionsPolygons is not null)
            features.AddRange(await _polygons.GetFeatures(OsmOptionsPolygon(geom)));
        
        var optionsPoints = OsmOptionsPoint(geom);
        if (optionsPoints is not null)
            features.AddRange(await _points.GetFeatures(OsmOptionsPoint(geom)));
        
        var optionsRoads = OsmOptionsRoads(geom);
        if (optionsRoads is not null)
            features.AddRange(await _roads.GetFeatures(OsmOptionsRoads(geom)));
        
        var featuresCollection = GisUtility.CreateFeatureCollection(features.ToArray());
        featuresCollection.BoundingBox = CalculateBoundingBox(features);
        return featuresCollection;
    }
}