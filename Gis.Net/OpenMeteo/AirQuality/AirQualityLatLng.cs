namespace Gis.Net.OpenMeteo.AirQuality;

/// <inheritdoc />
public class AirQualityLatLng : IAirQualityLatLng
{
    /// <inheritdoc />
    public string? Name { get; set; }
    
    /// <inheritdoc />
    public double Lat { get; set; }
    
    /// <inheritdoc />
    public double Lng { get; set; }

    public AirQualityLatLng(double lat, double lng)
    {
        Lat = lat;
        Lng = lng;
    }
}