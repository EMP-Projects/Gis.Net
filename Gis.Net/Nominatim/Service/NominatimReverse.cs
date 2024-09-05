using System.Globalization;
using Gis.Net.Nominatim.Xml;

namespace Gis.Net.Nominatim.Service;

/// <summary>
/// Represents a class for performing reverse geocoding using Nominatim API.
/// </summary>
public class NominatimReverse: Nominatim<ReverseGeocodeXml>
{

    /// <summary>
    /// Generates a list of query parameters based on the specific implementation of the QueryParams method in the derived class.
    /// </summary>
    /// <returns>A list of query parameters.</returns>
    protected override List<string> QueryParams()
    {
        List<string> qList = [];
            
        var lat = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", Request?.Lat);
        var lon = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", Request?.Lon);

        qList.Add($"lat={lat}");
        qList.Add($"lon={lon}");

        return qList;
    }

    /// <summary>
    /// Sets the result limitations for the Nominatim query.
    /// </summary>
    /// <param name="qList">The list of query parameters.</param>
    /// <remarks>
    /// This method adds the zoom level limitation to the query parameters list.
    /// </remarks>
    public override void SetResultLimitations(ref List<string> qList)
    {
        if (Limitations is not null)
            qList.Add($"zoom={Limitations.Zoom}");
    }

    /// <summary>
    /// Represents a class for performing Nominatim reverse geocoding operations.
    /// </summary>
    public NominatimReverse(HttpClient httpClient) : base(httpClient)
    {
    }
}