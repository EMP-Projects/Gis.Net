namespace Gis.Net.OpenMeteo.AirQuality;

public interface IAirQualityOptions
{
    AirQualityLatLng[] Points { get; set; }
    string? TimeZone { get; set; }
    
    int? ForecastDays { get; set; }
    int? ForecastHours { get; set; }
}