namespace Gis.Net.OpenMeteo.AirQuality;

public interface IAirQualityResponse
{
    long Lat { get; set; }
    long Lng { get; set; }
    long Elevation { get; set; }
    AirQualityData[] Current { get; set; }
    AirQualityData[] Hourly { get; set; }
}