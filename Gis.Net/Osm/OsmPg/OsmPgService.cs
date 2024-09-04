using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using Gis.Net.Vector;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.OsmPg;

/// <summary>
/// Represents an abstract base class for OSM PostgreSQL services.
/// </summary>
/// <typeparam name="T">The type of the database context.</typeparam>
public abstract class OsmPgService<T> : IOsmPgService where T : DbContext, IOsm2PgsqlDbContext
{
    private readonly IOsmPg<PlanetOsmLine, T> _lines;
    private readonly IOsmPg<PlanetOsmPolygon, T> _polygons;
    private readonly IOsmPg<PlanetOsmPoint, T> _points;
    private readonly IOsmPg<PlanetOsmRoads, T> _roads;

    /// <summary>
    /// Represents an abstract base class for OSM PostgreSQL services.
    /// </summary>
    /// <typeparam name="T">The type of the database context.</typeparam>
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
    /// Returns the OsmOptions for lines based on the given geometry.
    /// </summary>
    /// <param name="geom">The geometry used for querying lines.</param>
    /// <returns>The OsmOptions for lines based on the given geometry.</returns>
    protected abstract OsmOptions<PlanetOsmLine>? OsmOptionsLines(Geometry geom);

    /// <summary>
    /// Gets the OSM options for polygons based on the provided geometry.
    /// </summary>
    /// <param name="geom">The geometry for which to get the options.</param>
    /// <returns>The OSM options for polygons.</returns>
    protected abstract OsmOptions<PlanetOsmPolygon>? OsmOptionsPolygon(Geometry geom);

    /// <summary>
    /// Represents a method to generate OSM options for the specified point geometry.
    /// </summary>
    /// <param name="geom">The point geometry.</param>
    /// <returns>An instance of <see cref="OsmOptions{T}"/> with the generated OSM options, or null if no options are available.</returns>
    protected abstract OsmOptions<PlanetOsmPoint>? OsmOptionsPoint(Geometry geom);

    /// <summary>
    /// Retrieves the options for querying road features from the OSM PostgreSQL database based on the given geometry.
    /// </summary>
    /// <param name="geom">The geometry representing the area of interest.</param>
    /// <returns>The options for querying road features.</returns>
    protected abstract OsmOptions<PlanetOsmRoads>? OsmOptionsRoads(Geometry geom);

    /// <summary>
    /// Retrieves a collection of features based on a given geometry.
    /// </summary>
    /// <param name="geom">The geometry used as a filter for retrieving features.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a FeatureCollection.</returns>
    public async Task<FeatureCollection> GetFeatures(Geometry geom)
    {
        var features = new List<Feature>();

        var optionsLines = OsmOptionsLines(geom);
        if (optionsLines is not null && optionsLines.Tags?.Length > 0)
        {
            var lines = await _lines.GetFeatures(optionsLines);
            if (lines is not null) features.AddRange(lines);
        }

        var optionsPolygons = OsmOptionsPolygon(geom);
        if (optionsPolygons is not null && optionsPolygons.Tags?.Length > 0)
        {
            var polygons = await _polygons.GetFeatures(optionsPolygons);
            if (polygons is not null) features.AddRange(polygons);
        }

        var optionsPoints = OsmOptionsPoint(geom);
        if (optionsPoints is not null && optionsPoints.Tags?.Length > 0)
        {
            var points = await _points.GetFeatures(optionsPoints);
            if (points is not null) features.AddRange(points);
        }

        var optionsRoads = OsmOptionsRoads(geom);
        if (optionsRoads is not null && optionsRoads.Tags?.Length > 0)
        {
            var roads = await _roads.GetFeatures(optionsRoads);
            if (roads is not null) features.AddRange(roads);
        }

        var featuresCollection = GisUtility.CreateFeatureCollection(features.ToArray());
        featuresCollection.BoundingBox = CalculateBoundingBox(features);
        return featuresCollection;
    }
}