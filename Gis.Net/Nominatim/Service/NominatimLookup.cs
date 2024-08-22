using Gis.Net.Nominatim.Xml;

namespace Gis.Net.Nominatim.Service;

/// <summary>
/// Represents a class for performing Nominatim lookup operations.
/// </summary>
/// <typeparam name="T">The type of result to be returned.</typeparam>
public class NominatimLookup : Nominatim<LookupResultsXml>
{
    /// <summary>
    /// Generates a list of query parameters based on the specific implementation of the QueryParams method in the derived class.
    /// </summary>
    /// <returns>A list of query parameters.</returns>
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

    /// <summary>
    /// Sets the result limitations for the Nominatim query.
    /// </summary>
    /// <param name="qList">The list of query parameters.</param>
    public override void SetResultLimitations(ref List<string> qList) { }

    /// <inheritdoc />
    public NominatimLookup(HttpClient httpClient) : base(httpClient)
    {
    }
}