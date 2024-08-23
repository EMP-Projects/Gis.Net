using System.Text.Json;
using Gis.Net.Vector;
using NetTopologySuite.Geometries;

namespace Gis.Net.GeoJsonImport;

/// <summary>
/// Represents a utility class for working with GeoJSON.
/// </summary>
public static class GeoJson
{
    /// <summary>
    /// Retrieves a feature collection from a GeoJSON file.
    /// </summary>
    /// <param name="geoJsonFileName">The file name of the GeoJSON file.</param>
    /// <param name="pathRoot">The root path where the GeoJSON file is located. If null, the default path "GeoJson" will be used.</param>
    /// <returns>A GeoJsonImport object representing the feature collection in the GeoJSON file. Returns null if the file cannot be found or deserialized.</returns>
    public static GeoJsonImport? GetFeatureCollectionByGeoJson(string geoJsonFileName, string? pathRoot = null)
    {
        var pathGeoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathRoot ?? "GeoJson");
        var fileGeoJson = Path.Combine(pathGeoJson, geoJsonFileName);
        using var streamGeoJson = File.OpenRead(fileGeoJson);
        return JsonSerializer.Deserialize<GeoJsonImport>(streamGeoJson);
    }

    /// <summary>
    /// Creates a polygon geometry based on the given coordinates.
    /// </summary>
    /// <param name="coordinates">A list of coordinate triplets representing the polygon's exterior and interior rings.</param>
    /// <returns>A Polygon object representing the created polygon geometry.</returns>
    public static Polygon? CreatePolygon(List<List<List<double>>> coordinates)
    {
        var coordinateToPoly = new List<Coordinate>();
        foreach (var a in coordinates)
            coordinateToPoly.AddRange(a.Select(b => new Coordinate(b[1], b[0])));

        var ring = new LinearRing(coordinateToPoly.ToArray());
        var geom = GisUtility.CreateGeometryFactory(3857).CreatePolygon(ring);
        return geom;
    }
}