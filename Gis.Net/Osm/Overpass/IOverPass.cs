using NetTopologySuite.Features;
using TeamSviluppo.Gis.Overpass;

namespace Gis.Net.Osm.Overpass;

public interface IOverPass
{
    Task<FeatureCollection?> Intersects(OverPassOptions options);
}