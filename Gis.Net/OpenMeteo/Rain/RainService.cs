using System.Globalization;
using Gis.Net.OpenMeteo.Weather;

namespace Gis.Net.OpenMeteo.Rain;

/// <inheritdoc cref="IRainService" />
public class RainService : OpenMeteoService, IRainService
{
    /// <inheritdoc />
    public RainService(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    protected override string UriParameters() => "daily=weather_code,rain_sum";

    private static IEnumerable<OpenMeteoData> Daily(IOpenMeteoResponse? response)
    {
        var result = new List<OpenMeteoData>();

        for (var i = 0; i < response?.Daily?.Time?.Count; i++)
        {
            if (response.Daily?.RainSum is null) continue;
            if (response.Daily?.Time is null) continue;
            if (response.Daily?.WeatherCode is null) continue;
            if (response.DailyUnits is null) continue;
            
            var value = (double)response.Daily.RainSum[i]!;

            var od = new OpenMeteoData
            {
                Lat = response.Latitude,
                Lng = response.Longitude,
                Elevation = response.Elevation,
                Timezone = response.Timezone,
                Date = DateTime.Parse(response.Daily.Time[i]).ToUniversalTime(),
                Value = value,
                Description =
                    $"{WeatherCodes.GetWmo((int)response.Daily.WeatherCode[i]!)} " +
                    $"- Rain {value.ToString(CultureInfo.InvariantCulture)} {response.DailyUnits.RainSum}"
            };

            result.Add(od);
        }

        return result;
    }
    
    /// <inheritdoc />
    public virtual async Task<OpenMeteoData?> Forecast(OpenMeteoOptions options)
    {
        if (options.Lat is null || options.Lng is null)
            throw new ArgumentException("Missing Required Geographic Coordinates");
        
        if (options.TimeZone is null)
            throw new ArgumentException("[TimeZone] Required Argument Missing");

        var uri = $"{HttpClient.BaseAddress}/forecast?latitude={options.Lat?.ToString(CultureInfo.InvariantCulture)}" +
                  $"&longitude={options.Lng?.ToString(CultureInfo.InvariantCulture)}" +
                  $"&timezone={options.TimeZone}";
        uri += $"&{UriParameters()}";

        return await ApiRequest<OpenMeteoData>(uri);
    }
    
}