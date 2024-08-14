﻿using System.Globalization;
using Gis.Net.Nominatim.Xml;

namespace Gis.Net.Nominatim.Service;

public class NominatimSearch : Nominatim<SearchResultsXml>
{
    public override void SetResultLimitations(ref List<string> qList)
    {
        if (Limitations is null) return;
        qList.Add($"limit={Limitations.Limit}");
        qList.Add($"bounded={Limitations.Bounded}");

        if (Limitations.ViewBox is not null && Limitations.ViewBox.Count > 0)
        {
            var viewBox = "";
            for (var i = 0; i < Limitations.ViewBox.Count; i++)
            {
                viewBox += Limitations.ViewBox[i].ToString(CultureInfo.InvariantCulture);
                if (i < Limitations.ViewBox.Count - 1)
                    viewBox += ",";
            }
            qList.Add($"viewbox={viewBox}");
        }

        if (Limitations.ExcludePlaceIds is not null && Limitations.ExcludePlaceIds.Count > 0)
        {
            var excludeIds = string.Empty;
            for (var i = 0; i < Limitations.ExcludePlaceIds.Count; i++)
            {
                excludeIds += Limitations.ExcludePlaceIds[i];
                if (i < Limitations.ExcludePlaceIds.Count - 1)
                    excludeIds += ",";
            }

            qList.Add($"exclude_place_ids={excludeIds}");
        }

        if (Limitations.CountryCodes is not null && Limitations.CountryCodes.Count > 0)
        {
            var countryCodes = string.Empty;
            for (var i = 0; i < Limitations.CountryCodes.Count; i++)
            {
                countryCodes += Limitations.CountryCodes[i].ToLower();
                if (i < Limitations.CountryCodes.Count - 1)
                    countryCodes += ",";
            };
            qList.Add($"countrycodes={countryCodes}");
        }

        if (Limitations.Email is not null)
            qList.Add($"email={Limitations.Email}");
    }

    protected override List<string> QueryParams()
    {
        List<string> qList = new() { $"city={Request?.City}" };

        if (Request?.Street is not null)
            qList.Add($"street={Request?.Street}");

        if (Request?.County is not null)
            qList.Add($"county={Request?.County}");

        if (Request?.Country is not null)
            qList.Add($"country={Request?.Country}");

        if (Request?.Postalcode is not null)
            qList.Add($"postalcode={Request?.Postalcode}");

        return qList;
    }

    public NominatimSearch(HttpClient httpClient) : base(httpClient)
    {
    }
}