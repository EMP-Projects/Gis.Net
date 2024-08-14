using System.Globalization;
using Gis.Net.Nominatim.Xml;

namespace Gis.Net.Nominatim.Service;

public class NominatimReverse: Nominatim<ReverseGeocodeXml>
{

    protected override List<string> QueryParams()
    {
        List<string> qList = [];
            
        var lat = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", Request?.Lat);
        var lon = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", Request?.Lon);

        qList.Add($"lat={lat}");
        qList.Add($"lon={lon}");

        return qList;
    }
    
    public override void SetResultLimitations(ref List<string> qList)
    {
        if (Limitations is not null)
            qList.Add($"zoom={Limitations.Zoom}");
    }

    public NominatimReverse(HttpClient httpClient) : base(httpClient)
    {
    }
}