using NetTopologySuite.Features;

namespace Gis.Net.Osm.Overpass;

public interface IOverPass
{
    Task<FeatureCollection?> Intersects(OverPassOptions options);
}