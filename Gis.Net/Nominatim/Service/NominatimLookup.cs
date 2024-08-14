using Gis.Net.Nominatim.Xml;

namespace Gis.Net.Nominatim.Service;

public class NominatimLookup : Nominatim<LookupResultsXml>
{
    protected override List<string> QueryParams()
    {
        List<string> qList = new();
        string? qIds = null;

        if (Request?.Node is not null)
            qIds += $"N{Request?.Node}";

        if (Request?.Way is not null)
        {
            if (qIds is not null)
                qIds += ",";
            qIds += $"W{Request?.Way}";
        }

        if (Request?.Relation is not null)
        {
            if (qIds is not null)
                qIds += ",";
            qIds += $"R{Request?.Relation}";
        }

        qList.Add($"osm_ids={qIds}");

        return qList;
    }
    
    public override void SetResultLimitations(ref List<string> qList) { }

    public NominatimLookup(HttpClient httpClient) : base(httpClient)
    {
    }
}