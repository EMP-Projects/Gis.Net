using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents the response object returned by the Air Quality API.
/// </summary>
public partial class AirQualityApiResponse
{
    /// <summary>
    /// Represents the elevation information in the AirQualityApiResponse.
    /// </summary>
    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }

    /// The HourlyUnits class represents the units used for the hourly air quality measurements.
    /// /
    [JsonPropertyName("hourly_units")]
    public HourlyUnits? HourlyUnits { get; set; }

    /// <summary>
    /// Gets or sets the generation time in milliseconds of the air quality data.
    /// </summary>
    [JsonPropertyName("generationtime_ms")]
    public double? GenerationtimeMs { get; set; }

    /// <summary>
    /// Represents the current air quality data.
    /// </summary>
    [JsonPropertyName("current")]
    public Current? Current { get; set; }

    /// <summary>
    /// Gets or sets the timezone abbreviation.
    /// </summary>
    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    /// <summary>
    /// Gets or sets the units of measurement for the current air quality data.
    /// </summary>
    [JsonPropertyName("current_units")]
    public CurrentUnits? CurrentUnits { get; set; }

    /// <summary>
    /// Represents the timezone information of an air quality API response.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <summary>
    /// Represents the latitude of the location.
    /// </summary>
    /// <value>The latitude in decimal degrees.</value>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the UTC offset in seconds.
    /// </summary>
    [JsonPropertyName("utc_offset_seconds")]
    public long? UtcOffsetSeconds { get; set; }

    /// <summary>
    /// Represents the hourly air quality data.
    /// </summary>
    [JsonPropertyName("hourly")]
    public Hourly? Hourly { get; set; }

    /// <summary>
    /// Represents the geographical longitude coordinate.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
}