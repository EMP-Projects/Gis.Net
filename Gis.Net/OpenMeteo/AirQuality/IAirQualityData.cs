namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents air quality data.
/// </summary>
/// <remarks>
/// This interface defines the properties for air quality data, such as time, name, value, unit, text, and hex color.
/// It is typically used in an air quality response object.
/// </remarks>
public interface IAirQualityData
{
    /// <summary>
    /// Represents the time of the air quality data.
    /// </summary>
    /// <remarks>
    /// The Time property stores the timestamp of the air quality data.
    /// This information indicates the date and time when the data was recorded or measured.
    /// It allows users to track the temporal aspect of the air quality data.
    /// The Time property is nullable to accommodate scenarios where the timestamp is not available or applicable.
    /// </remarks>
    DateTime? Time { get; set; }
    
    /// <summary>
    /// Represents the name property of an air quality data object.
    /// </summary>
    /// <remarks>
    /// The name property represents the name of a specific air quality measurement parameter, such as "PM2.5" or "CO2".
    /// It is typically used in conjunction with other properties, such as time, value, unit, text, and hex color, to describe the air quality data.
    /// </remarks>
    string? Name { get; set; }
    
    /// <summary>
    /// Represents the value of air quality data.
    /// </summary>
    /// <remarks>
    /// This property is used to store the value of air quality data.
    /// It is typically used in an air quality response object.
    /// </remarks>
    double? Value { get; set; }
    
    /// <summary>
    /// Represents a unit of measurement for air quality data.
    /// </summary>
    string? Unit { get; set; }
    
    /// <summary>
    /// Represents the text associated with air quality data.
    /// </summary>
    /// <remarks>
    /// This property stores the text description or information associated with air quality data.
    /// It provides additional context or details about the air quality measurement.
    /// This property is typically used in conjunction with other properties such as Time, Name, Value, Unit, and HexColor.
    /// </remarks>
    string? Text { get; set; }

    /// <summary>
    /// Represents air quality data.
    /// </summary>
    string? HexColor { get; set; }
}