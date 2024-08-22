using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems.Transformations;

namespace Gis.Net.Vector;

/// <summary>
/// Class for converting coordinates between WGS84 (latitude, longitude) and Web Mercator (x, y) coordinate systems.
/// </summary>
public class CoordinateConverter
{
    /// <summary>
    /// Represents a coordinate converter class that can be used to convert coordinates between Wgs84 and WebMercator systems.
    /// </summary>
    public CoordinateConverter()
    {
        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
             NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
         new PrecisionModel(1000d),
         4326,
         GeometryOverlay.NG,
         new CoordinateEqualityComparer());
    }

    /// <summary>
    /// Converts coordinates from WGS84 to Web Mercator projection.
    /// </summary>
    /// <param name="wgs84Longitude">The longitude in WGS84 coordinate system.</param>
    /// <param name="wgs84Latitude">The latitude in WGS84 coordinate system.</param>
    /// <returns>
    /// The converted point in Web Mercator projection.
    /// </returns>
    public static Point ConvertWgs84ToWebMercator(double wgs84Longitude, double wgs84Latitude)
    {
        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var wgs84Point = gf.CreatePoint(new Coordinate(wgs84Longitude, wgs84Latitude));
        var sourceCoordSystem = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
        var targetCoordSystem = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator;
        var wgs84ToWebMercatorTransformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(sourceCoordSystem, targetCoordSystem);
        var webMercatorPoint = Transform(wgs84Point, wgs84ToWebMercatorTransformation.MathTransform);
        return (Point)webMercatorPoint;
    }

    /// <summary>
    /// Convert a point in Web Mercator (EPSG:3857) coordinate system to WGS84 (EPSG:4326) coordinate system.
    /// </summary>
    /// <param name="lng">The longitude value of the point in Web Mercator coordinate system.</param>
    /// <param name="lat">The latitude value of the point in Web Mercator coordinate system.</param>
    /// <returns>A Point object representing the converted point in WGS84 coordinate system.</returns>
    public static Point ConvertWebMercatorToWgs84(double lng, double lat)
    {
        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(3857);
        var webMercatorPoint = gf.CreatePoint(new Coordinate(lng, lat));
        var sourceSystem = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator;
        var targetSystem = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
        var webMercatorToWgs84Transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(sourceSystem, targetSystem);
        var wgs84Point = Transform(webMercatorPoint, webMercatorToWgs84Transformation.MathTransform);
        return (Point)wgs84Point;
    }

    /// <summary>
    /// Transforms a geometry from one coordinate system to another using the given math transform.
    /// </summary>
    /// <param name="geom">The geometry to transform.</param>
    /// <param name="transform">The math transform to use for the transformation.</param>
    /// <returns>The transformed geometry.</returns>
    private static Geometry Transform(Geometry geom, MathTransform transform)
    {
        geom = geom.Copy();
        geom.Apply(new Mtf(transform));
        return geom;
    }

    /// <summary>
    /// The CoordinateConverter class provides methods to convert coordinates between WGS84 (GPS coordinates) and Web Mercator.
    /// </summary>
    private sealed class Mtf(MathTransform mathTransform) : ICoordinateSequenceFilter
    {
        /// <summary>
        /// The CoordinateConverter class provides methods to convert coordinates between WGS84 and Web Mercator coordinate systems.
        /// </summary>
        public bool Done => false;
        /// <summary>
        /// A class that provides utility methods for converting coordinates between WGS84 and Web Mercator coordinate systems.
        /// </summary>
        public bool GeometryChanged => true;

        /// <summary>
        /// Converts the given WGS84 longitude and latitude coordinates to Web Mercator coordinates.
        /// </summary>
        /// <returns>The converted Web Mercator coordinates as a Point.</returns>
        public void Filter(CoordinateSequence seq, int i)
        {
            var x = seq.GetX(i);
            var y = seq.GetY(i);
            var z = seq.GetZ(i);
            mathTransform.Transform(ref x, ref y, ref z);
            seq.SetX(i, x);
            seq.SetY(i, y);
            seq.SetZ(i, z);
        }
    }
}