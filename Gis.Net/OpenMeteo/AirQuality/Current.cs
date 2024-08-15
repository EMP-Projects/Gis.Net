using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

public partial class Current
{
    [JsonPropertyName("sulphur_dioxide")]
    public double? SulphurDioxide { get; set; }

    [JsonPropertyName("pm2_5")]
    public double? Pm25 { get; set; }

    [JsonPropertyName("nitrogen_dioxide")]
    public double? NitrogenDioxide { get; set; }

    [JsonPropertyName("pm10")]
    public double? Pm10 { get; set; }

    [JsonPropertyName("interval")]
    public long? Interval { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("ozone")]
    public double? Ozone { get; set; }

    [JsonPropertyName("european_aqi")]
    public long? EuropeanAqi { get; set; }

    [JsonPropertyName("carbon_monoxide")]
    public double? CarbonMonoxide { get; set; }
}