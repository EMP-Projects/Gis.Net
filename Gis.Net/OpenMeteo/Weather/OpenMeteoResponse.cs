using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.Weather;

/// <inheritdoc />
public class OpenMeteoResponse : IOpenMeteoResponse
{
    /// <inheritdoc />
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("generationtime_ms")]
    public double? GenerationTimeMs { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("utc_offset_seconds")]
    public int? UtcOffsetSeconds { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("daily_units")]
    public DailyUnits? DailyUnits { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("daily")]
    public Daily? Daily { get; set; }
}