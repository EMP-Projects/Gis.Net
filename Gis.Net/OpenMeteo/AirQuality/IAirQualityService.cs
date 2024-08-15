namespace Gis.Net.OpenMeteo.AirQuality;

public interface IAirQualityService
{
    Task<List<AirQualityApiResponse>?> AirQuality(AirQualityOptions options);
    string[] Current { get; set; }
    string[] Hourly { get; set; }
}