using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

public partial class AirQualityApiResponse
{
    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }

    [JsonPropertyName("hourly_units")]
    public HourlyUnits? HourlyUnits { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double? GenerationtimeMs { get; set; }

    [JsonPropertyName("current")]
    public Current? Current { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    [JsonPropertyName("current_units")]
    public CurrentUnits? CurrentUnits { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public long? UtcOffsetSeconds { get; set; }

    [JsonPropertyName("hourly")]
    public Hourly? Hourly { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
}