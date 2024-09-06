namespace Gis.Net.OpenMeteo.AirQuality;

/// Represents the options for retrieving air quality data.
/// /
public class AirQualityOptions : IAirQualityOptions
{
    /// <summary>
    /// Represents the configuration options for retrieving air quality data.
    /// </summary>
    public AirQualityOptions(AirQualityLatLng point)
    {
        Point = point;
    }

    /// <inheritdoc />
    public AirQualityLatLng Point { get; set; }

    /// <inheritdoc />
    public string? TimeZone { get; set; } = "Europe/Berlin";

    /// <inheritdoc />
    public int? ForecastDays { get; set; } = 5;

    /// <inheritdoc />
    public int? ForecastHours { get; set; } = 24;
}