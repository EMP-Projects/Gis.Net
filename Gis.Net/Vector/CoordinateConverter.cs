using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems.Transformations;

namespace Gis.Net.Vector;

public class CoordinateConverter
{
    public CoordinateConverter()
    {
        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
             NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
         new PrecisionModel(1000d),
         4326,
         GeometryOverlay.NG,
         new CoordinateEqualityComparer());
    }

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

    private static Geometry Transform(Geometry geom, MathTransform transform)
    {
        geom = geom.Copy();
        geom.Apply(new Mtf(transform));
        return geom;
    }

    private sealed class Mtf(MathTransform mathTransform) : ICoordinateSequenceFilter
    {
        public bool Done => false;
        public bool GeometryChanged => true;
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