using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

public partial class CurrentUnits
{
    [JsonPropertyName("sulphur_dioxide")]
    public string? SulphurDioxide { get; set; }

    [JsonPropertyName("pm2_5")]
    public string? Pm25 { get; set; }

    [JsonPropertyName("nitrogen_dioxide")]
    public string? NitrogenDioxide { get; set; }

    [JsonPropertyName("pm10")]
    public string? Pm10 { get; set; }

    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("ozone")]
    public string? Ozone { get; set; }

    [JsonPropertyName("european_aqi")]
    public string? EuropeanAqi { get; set; }

    [JsonPropertyName("carbon_monoxide")]
    public string? CarbonMonoxide { get; set; }
}