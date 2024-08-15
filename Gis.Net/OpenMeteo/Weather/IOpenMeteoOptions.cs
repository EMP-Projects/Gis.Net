namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// Defines the options for accessing OpenMeteo data.
/// </summary>
public interface IOpenMeteoOptions : ICoordinatesOptions
{
    /// <summary>
    /// Gets or sets the unique identifier used for OpenMeteo queries.
    /// </summary>
    /// <value>
    /// The identifier as a long integer, or null if not applicable.
    /// </value>
    long? Id { get; set; }
    
    /// <summary>
    /// Gets or sets the limit for API calls to OpenMeteo.
    /// </summary>
    /// <value>
    /// The maximum number of API calls allowed, or null if there is no limit.
    /// </value>
    int? LimitApiCalls { get; set; }
    
    /// <summary>
    /// Gets or sets the time zone for the OpenMeteo data.
    /// </summary>
    /// <value>
    /// The time zone as a string, or null if not specified.
    /// </value>
    string? TimeZone { get; set; }
    
    /// <summary>
    /// Gets or sets the number of forecast days for OpenMeteo data retrieval.
    /// </summary>
    /// <value>
    /// The number of days to forecast, or null if not set.
    /// </value>
    int? ForecastDays { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether only values greater than zero should be considered.
    /// </summary>
    /// <value>
    /// A boolean value that indicates whether to filter for values greater than zero, or null if the filter should not be applied.
    /// </value>
    bool? ValuesGreaterThanZero { get; set; }
}