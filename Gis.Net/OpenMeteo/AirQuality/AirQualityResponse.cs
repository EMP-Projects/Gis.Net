namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents a response object that contains air quality information.
/// </summary>
/// <remarks>
/// The AirQualityResponse class is used to store air quality data for a specific location.
/// It contains properties for latitude, longitude, elevation, current air quality data, and hourly air quality data.
/// </remarks>
public class AirQualityResponse : IAirQualityResponse
{
    /// <summary>
    /// Represents the latitude of a location in the air quality response.
    /// </summary>
    /// <remarks>
    /// The latitude is a decimal value that specifies the north-south position of a location on Earth.
    /// It is used in the air quality response object to indicate the latitude of the location for which air quality data is provided.
    /// </remarks>
    public long Lat { get; set; }
    
    /// <summary>
    /// Represents the longitude value of a location.
    /// </summary>
    /// <remarks>
    /// The longitude value represents the east-west position of a location on the Earth's surface.
    /// It is typically used in conjunction with the latitude value to specify the exact coordinates of a location.
    /// </remarks>
    public long Lng { get; set; }
    
    /// <summary>
    /// Gets or sets the elevation of the location.
    /// </summary>
    /// <remarks>
    /// The elevation is the height above or below mean sea level.
    /// It is a measure of vertical distance relative to the Earth's surface.
    /// The value is usually expressed in meters or feet.
    /// </remarks>
    public long Elevation { get; set; }
    
    /// <summary>
    /// Gets or sets the current air quality data.
    /// </summary>
    /// <remarks>
    /// This property represents the current air quality data, including information such as time, name, value, unit, text, and hex color.
    /// </remarks>
    public required AirQualityData[] Current { get; set; }

    /// <inheritdoc />
    public required AirQualityData[] Hourly { get; set; }
}