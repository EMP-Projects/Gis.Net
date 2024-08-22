using Gis.Net.Nominatim.Dto;
using NetTopologySuite.Features;

namespace Gis.Net.Nominatim.Service;

/// <summary>
/// Represents the interface for communicating with the Nominatim service.
/// </summary>
/// <typeparam name="T">The type of Nominatim response.</typeparam>
public interface INominatim<T> where T : class
{
    /// <summary>
    /// Converts the location information to the GeoJSON format.
    /// </summary>
    /// <param name="limitations">The limitations of the result.</param>
    /// <param name="request">The request for retrieving location information.</param>
    /// <returns>Returns a Task that represents the asynchronous operation. The task result contains the converted location information as a FeatureCollection, or null if the conversion fails.</returns>
    Task<FeatureCollection?> ToGeoJson(IResultLimitations limitations, INominatimRequest request);

    /// <summary>
    /// Converts the Nominatim API response to a JSON array.
    /// </summary>
    /// <param name="limitations">The result limitations.</param>
    /// <param name="request">The Nominatim request.</param>
    /// <returns>The NominatimResponse array as a JSON string.</returns>
    Task<NominatimResponse[]?> ToJson(IResultLimitations limitations, INominatimRequest request);

    /// <summary>
    /// Serializes the data in XML format based on the specified limitations and request.
    /// </summary>
    /// <param name="limitations">The limitations of the result.</param>
    /// <param name="request">The request for retrieving location information.</param>
    /// <returns>The serialized XML data as string.</returns>
    Task<T?> ToXml(IResultLimitations limitations, INominatimRequest request);
}