namespace Gis.Net.OpenMeteo.AirQuality;

public class AirQualityResponse : IAirQualityResponse
{
    public long Lat { get; set; }
    public long Lng { get; set; }
    public long Elevation { get; set; }
    public AirQualityData[] Current { get; set; }
    public AirQualityData[] Hourly { get; set; }
}