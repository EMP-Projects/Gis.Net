namespace Gis.Net.OpenMeteo.AirQuality;

public class AirQualityOptions : IAirQualityOptions
{
    public AirQualityOptions(AirQualityLatLng[] points)
    {
        Points = points;
    }
    
    public AirQualityLatLng[] Points { get; set; }
    
    public string? TimeZone { get; set; } = "Europe/Berlin";
    
    public int? ForecastDays { get; set; } = 7;
    
    public int? ForecastHours { get; set; } = 24;
}