using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

public partial class HourlyUnits
{
    [JsonPropertyName("sulphur_dioxide")]
    public string? SulphurDioxide { get; set; }

    [JsonPropertyName("european_aqi_pm2_5")]
    public string? EuropeanAqiPm25 { get; set; }

    [JsonPropertyName("nitrogen_dioxide")]
    public string? NitrogenDioxide { get; set; }

    [JsonPropertyName("pm10")]
    public string? Pm10 { get; set; }

    [JsonPropertyName("ozone")]
    public string? Ozone { get; set; }

    [JsonPropertyName("european_aqi_ozone")]
    public string? EuropeanAqiOzone { get; set; }

    [JsonPropertyName("european_aqi_nitrogen_dioxide")]
    public string? EuropeanAqiNitrogenDioxide { get; set; }

    [JsonPropertyName("european_aqi_pm10")]
    public string? EuropeanAqiPm10 { get; set; }

    [JsonPropertyName("pm2_5")]
    public string? Pm25 { get; set; }

    [JsonPropertyName("european_aqi_sulphur_dioxide")]
    public string? EuropeanAqiSulphurDioxide { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("carbon_monoxide")]
    public string? CarbonMonoxide { get; set; }

    [JsonPropertyName("european_aqi")]
    public string? EuropeanAqi { get; set; }
}