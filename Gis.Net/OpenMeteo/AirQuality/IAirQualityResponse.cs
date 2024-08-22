namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents a response object that contains air quality information.
/// </summary>
public interface IAirQualityResponse
{
    /// <summary>
    /// Represents the latitude of a location.
    /// </summary>
    /// <remarks>
    /// The latitude is a geographical coordinate that specifies the north-south position of a point on the Earth's surface.
    /// In the context of the AirQualityResponse class, this property stores the latitude value of a specific location
    /// for which air quality data is being retrieved.
    /// </remarks>
    long Lat { get; set; }
    
    /// <summary>
    /// Represents the longitude of a location.
    /// </summary>
    /// <remarks>
    /// The Lng property is used to store the longitude coordinate of a location.
    /// It is typically used in conjunction with the Lat property to specify the geographic coordinates of a point.
    /// </remarks>
    long Lng { get; set; }
    
    /// <summary>
    /// Represents a response object that contains air quality information.
    /// </summary>
    long Elevation { get; set; }
    
    /// <summary>
    /// Represents the current air quality data.
    /// </summary>
    /// <remarks>
    /// This property is used to store the current air quality data for a specific location.
    /// It is part of the <see cref="IAirQualityResponse"/> interface and is typically used in an <see cref="AirQualityResponse"/> object.
    /// </remarks>
    AirQualityData[] Current { get; set; }
    
    /// <summary>
    /// Represents the hourly air quality data.
    /// </summary>
    /// <remarks>
    /// The Hourly property represents the hourly air quality information for a specific location.
    /// It is used in the AirQualityResponse class to store the hourly air quality data.
    /// </remarks>
    /// <seealso cref="AirQualityResponse"/>
    /// <seealso cref="AirQualityData"/>
    /// <seealso cref="IAirQualityResponse"/>
    /// <seealso cref="IAirQualityData"/>
    AirQualityData[] Hourly { get; set; }
}