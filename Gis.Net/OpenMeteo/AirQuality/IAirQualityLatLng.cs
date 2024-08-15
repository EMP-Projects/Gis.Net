namespace Gis.Net.OpenMeteo.AirQuality;

public interface IAirQualityLatLng
{
    string? Name { get; set; }
    double Lat { get; set; }
    double Lng { get; set; }
}