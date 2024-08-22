namespace Gis.Net.OpenMeteo.Weather;

/// <inheritdoc cref="Gis.Net.OpenMeteo.Weather.IOpenMeteoOptions" />
public class OpenMeteoOptions : CoordinatesOptions, IOpenMeteoOptions
{
    /// <inheritdoc />
    public long? Id { get; set; }
    
    /// <inheritdoc />
    public int? LimitApiCalls { get; set; } = 500;

    /// <inheritdoc />
    public string? TimeZone { get; set; } = "Europe/Berlin";

    /// <inheritdoc />
    public int? ForecastDays { get; set; } = 5;

    /// <inheritdoc />
    public bool? ValuesGreaterThanZero { get; set; } = true;
}