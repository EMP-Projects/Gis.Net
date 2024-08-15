using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

public partial class Hourly
{
    [JsonPropertyName("sulphur_dioxide")]
    public List<double?> SulphurDioxide { get; set; }

    [JsonPropertyName("european_aqi_pm2_5")]
    public List<long?> EuropeanAqiPm25 { get; set; }

    [JsonPropertyName("nitrogen_dioxide")]
    public List<double?> NitrogenDioxide { get; set; }

    [JsonPropertyName("pm10")]
    public List<double?> Pm10 { get; set; }

    [JsonPropertyName("ozone")]
    public List<double?> Ozone { get; set; }

    [JsonPropertyName("european_aqi_ozone")]
    public List<long?> EuropeanAqiOzone { get; set; }

    [JsonPropertyName("european_aqi_nitrogen_dioxide")]
    public List<long?> EuropeanAqiNitrogenDioxide { get; set; }

    [JsonPropertyName("european_aqi_pm10")]
    public List<long?> EuropeanAqiPm10 { get; set; }

    [JsonPropertyName("pm2_5")]
    public List<double?> Pm25 { get; set; }

    [JsonPropertyName("european_aqi_sulphur_dioxide")]
    public List<long?> EuropeanAqiSulphurDioxide { get; set; }

    [JsonPropertyName("time")]
    public List<string?> Time { get; set; }

    [JsonPropertyName("carbon_monoxide")]
    public List<double?> CarbonMonoxide { get; set; }

    [JsonPropertyName("european_aqi")]
    public List<long?> EuropeanAqi { get; set; }
}