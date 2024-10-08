using System.Globalization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <inheritdoc cref="IAirQualityService" />
public class AirQualityService : OpenMeteoService, IAirQualityService
{
    private static readonly string[] CurrentParameters = ["european_aqi", "pm10", "pm2_5", "carbon_monoxide", "nitrogen_dioxide", "sulphur_dioxide", "ozone", "dust"];
    private static readonly string[] HourlyParameters = ["pm10", "pm2_5", "carbon_monoxide", "nitrogen_dioxide", "sulphur_dioxide", "ozone", "european_aqi", "european_aqi_pm2_5", "european_aqi_pm10", "european_aqi_nitrogen_dioxide", "european_aqi_ozone", "european_aqi_sulphur_dioxide"];
    
    /// <inheritdoc />
    public AirQualityService(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public virtual string[] Current { get; set; } = CurrentParameters;
    
    /// <inheritdoc />
    public virtual string[] Hourly { get; set; } = HourlyParameters;

    /// <inheritdoc />
    public async Task<AirQualityApiResponse?> AirQuality(AirQualityOptions options)
    {
        if (options.Point is null)
            throw new ArgumentException("Missing Required Points");
        
        if (options.TimeZone is null)
            throw new ArgumentException("[TimeZone] Required Argument Missing");
        
        var uri = $"{HttpClient.BaseAddress}/air-quality?timezone={options.TimeZone}"
            + $"&hourly={string.Join(",", HourlyParameters)}"
            + $"&forecast_days={options.ForecastDays}"
            + $"&forecast_hours={options.ForecastHours}"
            + $"{UriParameters()}"
            + $"&latitude={options.Point.Lat.ToString(CultureInfo.InvariantCulture)}&longitude={options.Point.Lng.ToString(CultureInfo.InvariantCulture)}";
        
        return await ApiRequest<AirQualityApiResponse>(uri);
    }

    /// <inheritdoc />
    protected override string UriParameters() => $"&current={string.Join(",", Current)}&hourly={string.Join(",", Hourly)}";
}
