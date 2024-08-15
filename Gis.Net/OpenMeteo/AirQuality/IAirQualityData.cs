namespace Gis.Net.OpenMeteo.AirQuality;

public interface IAirQualityData
{
    DateTime? Time { get; set; }
    string? Name { get; set; }
    double? Value { get; set; }
    string? Unit { get; set; }
    string? Text { get; set; }
    
    string? HexColor { get; set; }
}