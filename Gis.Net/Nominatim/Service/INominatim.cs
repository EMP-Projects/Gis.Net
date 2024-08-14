using Gis.Net.Nominatim.Dto;
using NetTopologySuite.Features;

namespace Gis.Net.Nominatim.Service;

public interface INominatim<T> where T : class
{
    Task<FeatureCollection?> ToGeoJson(IResultLimitations limitations, INominatimRequest request);
    Task<NominatimResponse[]?> ToJson(IResultLimitations limitations, INominatimRequest request);
    Task<T?> ToXml(IResultLimitations limitations, INominatimRequest request);
}